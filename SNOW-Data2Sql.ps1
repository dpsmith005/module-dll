set-ExecutionPolicy Bypass -scope currentuser   # change execution policy to be able to source VRAcommon.ps1

# Source the VRA common script
#. "\\wellspan.org\techservices\Scripts\Global\VRAcommon.ps1"
#. "\\wellspan.org\techservices\Scripts\Global\CyberarkCommon.ps1"

import-module "E:\OneDrive - WellSpan Health\SourceCode\module-dll\bin\Debug\netstandard2.0\publish\moduleDll.dll"

function Connect-SqlDB {
    [CmdletBinding()]
    Param ( [Parameter(Mandatory = $true)][string]$server,
        [Parameter(Mandatory = $true)][string]$database,
        [Parameter(Mandatory = $true)][System.Management.Automation.PSCredential]$creds
    )
	
    # SQL Connection
    $ConnectionTimeout = 30
    $conn = new-object System.Data.SqlClient.SQLConnection
    $conn.ConnectionString = "Server={0};Database={1};Integrated Security=True;Connect Timeout={2};User ID = {3};Password = {4}" -f $server, $database, $ConnectionTimeout, $creds.UserName, $creds.GetNetworkCredential().password
    $conn.Open()
	
    return $conn
}function Get-CyberARKAccountAdminVra {
    $AppID = "vRealizeAutomation"
    $Safe = "APP-vRA-API"
    $Object = "Operating System-WSH_Unmanaged_Generic-wellspan.org-admin_vra"
    $Reason = "Automated credential retrieval"
    $CCP = 'https://vault.wellspan.org/AIMWebService/api/Accounts?'
    $uri = "$($CCP)AppID=$($AppID)&Safe=$($Safe)&Object=$($Object)&Reason$($Reason)"
    $cred = Invoke-RestMethod -Uri $uri -Method GET -UseDefaultCredentials
    return $cred
}    # END - getCyberARKAccountAdminVra



# Gather the data from the snow report and store in one variable $data
$data1 = Get-SnowReportScrape -Type "incidents" -URl 'https://wellspan.service-now.com/$pa_dashboard.do?sysparm_dashboard=175bcd319359da50e10ab86efaba102b&sysparm_tab=631c45b59359da50e10ab86efaba1043&sysparm_cancelable=true&sysparm_editable=false&sysparm_active_panel=false'
$data2 = Get-SnowReportScrape -Type "changeRequests" -Url 'https://wellspan.service-now.com/$pa_dashboard.do?sysparm_dashboard=175bcd319359da50e10ab86efaba102b&sysparm_tab=e02f9ca897a76ed0e17277971153af4a&sysparm_cancelable=true&sysparm_editable=false&sysparm_active_panel=false'
$data3 = Get-SnowReportScrape -Type "catalogTasks" -Url 'https://wellspan.service-now.com/$pa_dashboard.do?sysparm_dashboard=175bcd319359da50e10ab86efaba102b&sysparm_tab=a1ab05f19359da50e10ab86efaba1059&sysparm_cancelable=true&sysparm_editable=undefined&sysparm_active_panel=false'
$data = $data1 + $data2 + $data3

# Export the data to a CSV
$data | Export-Csv .\output.csv
#$data = (import-Csv .\output.csv) |  Where-Object { $_.AssignmentGroup -notmatch "empty" }

class snowStats {
    [string]$DateStamp
    [string]$Type
    [string]$AssignmentGroup
    [int]$Authorize
    [int]$Canceled
    [int]$Closed
    [int]$Closed_Complete
    [int]$Closed_Incomplete
    [int]$Closed_Skipped
    [int]$In_Progress
    [int]$New
    [int]$On_Hold
    [int]$Open
    [int]$Pending
    [int]$Resolved
    [int]$Scheduled
    [int]$Work_in_Progress
}
$arrData = @()
$prevAss = "";
$statsData = new-object snowStats;
foreach ($d in ($data | Sort-Object DateStamp, Type, AssignmentGroup, State)) {
    if ($prevAss -eq $d.AssignmentGroup) {
        $statsData.DateStamp = $d.DateStamp
        $statsData.Type = $d.Type
        $statsData.AssignmentGroup = $d.AssignmentGroup
        $state = $d.State -replace " ", "_"
        if ($statsData.psobject.Properties.Name -contains $state) {
            $statsData.$($state) = $d.Count
        }
        else {
            "The state '$state' is not included in the stats"
        }
    }
    else {
        if ($prevAss -ne "") {
            $statsData = new-object snowStats;
            $state = $d.State -replace " ", "_"
            if ($statsData.psobject.Properties.Name -contains $state) {
                $statsData.$($state) = $d.Count
            }
            else {
                "The state '$state' not included in the stats"
            }
            $arrData += $statsData
        }
        $prevAss = $d.AssignmentGroup; 
    } 
}

#################################
# Write the info to the database #
##################################
$error.Clear()
# establish connection to database
try {
    # use admin_vra credentials
    [securestring]$secStringPassword = ConvertTo-SecureString (Get-CyberARKAccountAdminVra).content -AsPlainText -Force
    [pscredential]$creds = New-Object System.Management.Automation.PSCredential ("admin_vra", $secStringPassword)
	
    $connection = Connect-SqlDb -server "TSSQL01" -database "TechServices" -creds $creds
}
catch {
    Write-error "Error connecting to database : $error.Exception"
    return 1
}

# Loop through the data and write to a SQl table
foreach ($d in $arrData) {
    #$intCount = [int64]$d.count
    #$decPercent = [Decimal]($d.Percent)
    $queryInsert = @"
        INSERT INTO [dbo].[SNOW_Inc_CR_SR]
        ([DateStamp], [Type], [AssignmentGroup], [Authorize], [Canceled], [Closed], [Closed_Complete], [Closed_Incomplete]
        , [Closed_Skipped], [In_Progress], [New], [On_Hold], [Open], [Pending], [Resolved], [Scheduled], [Work_in_Progress])
        VALUES ('$($d.DateStamp)', '$($d.Type)', '$($d.AssignmentGroup)', '$($d.Authorize)', '$($d.Canceled)', '$($d.Closed)', '$($d.Closed_Complete)', '$($d.Closed_Incomplete)',
            '$($d.Closed_Skipped)', '$($d.In_Progress)', '$($d.New)', '$($d.On_Hold)', '$($d.Open)', '$($d.Pending)', '$($d.Resolved)', '$($d.Scheduled)', '$($d.Work_in_Progress)' )
"@
    $queryUpdate = @"
        UPDATE [dbo].[SNOW_Inc_CR_SR]
        SET [DateStamp] = '$($d.DateStamp)'
        , [Type] = '$($d.Type)'
        , [AssignmentGroup] = '$($d.AssignmentGroup)'
        , [Authorize] = $($d.Authorize)
        , [Canceled] = $($d.Canceled)
        , [Closed] = $($d.Closed)
        , [Closed_Complete] = $($d.Closed_Complete)
        , [Closed_Incomplete] = $($d.Closed_Incomplete)
        , [Closed_Skipped] = $($d.Closed_Skipped)
        , [In_Progress] = $($d.In_Progress)
        , [New] = $($d.New)
        , [On_Hold] = $($d.On_Hold)
        , [Open] = $($d.Open)
        , [Pending] = $($d.Pending)
        , [Resolved] = $($d.Resolved)
        , [Scheduled] = $($d.Scheduled)
        , [Work_in_Progress] = $($d.Work_in_Progress)
        WHERE Type = '$($d.Type)' and DateStamp = '$($d.DateStamp)' and AssignmentGroup = '$($d.AssignmentGroup)'
"@
    # Create full query.  Create check for record to perform update or insert
    $Query = @"
        $($QueryUpdate)
        IF @@ROWCOUNT=0
        $($QueryInsert)
"@
    try {
        $QueryTimeout = 120
        $cmd = new-object system.Data.SqlClient.SqlCommand($Query, $conn)
        $cmd.CommandTimeout = $QueryTimeout
        $cmd.ExecuteScalar() ### Command to execute the query
        $query
    }
    catch {
        Write-error "Error updating SNOW_Inc_CR_SR table : $error.Exception"
        return $cmd
    }
}

$connection.close()
$connection.Dispose()
Remove-Variable connection

