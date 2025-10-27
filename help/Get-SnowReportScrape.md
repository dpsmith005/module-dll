---
external help file: moduleDll.dll-Help.xml
Module Name: moduleDll
online version:
schema: 2.0.0
---

# Get-SnowReportScrape

## SYNOPSIS

{{ Scrape SNOW report for data }}

## SYNTAX

```powershell
Get-SnowReportScrape [-Type] <String> [-Url] <String> [<CommonParameters>]
```

## DESCRIPTION

{{ Gather data from a SNOW report that contains state counts by support group }}

## EXAMPLES

### Example 1

```powershell
PS C:\> {{Get-SnoReportScrape.ps1 -Type "incidents" -URl 'https://wellspan.service-now.com/$pa_dashboard.do?sysparm_dashboard=175bcd319359da50e10ab86efaba102b&sysparm_tab=631c45b59359da50e10ab86efaba1043&sysparm_cancelable=true&sysparm_editable=false&sysparm_active_panel=false'}}
```

{{ Scrape the Incidents report and return the incident state counts by support group }}

### Example 2

```powershell
PS C:\> {{Get-SnoReportScrape.ps1  -Type "changeRequests" -Url 'https://wellspan.service-now.com/$pa_dashboard.do?sysparm_dashboard=175bcd319359da50e10ab86efaba102b&sysparm_tab=e02f9ca897a76ed0e17277971153af4a&sysparm_cancelable=true&sysparm_editable=false&sysparm_active_panel=false'
        }}
```

{{ Scrape the Change Requests report and return the incident state counts by support group }}

### Example 3

```powershell
PS C:\> {{Get-SnoReportScrape.ps1  -Type "catalogTasks" -Url 'https://wellspan.service-now.com/$pa_dashboard.do?sysparm_dashboard=175bcd319359da50e10ab86efaba102b%26sysparm_tab=a1ab05f19359da50e10ab86efaba1059&sysparm_cancelable=true&sysparm_editable=undefined=sysparm_active_panel=false'
        }}
```

{{ Scrape the Catalog Tasks report and return the incident state counts by support group }}

### Example 4

### Example 5

```powershell
$data1 = Get-SnowReportScrape -Type "incidents" -URl 'https://wellspan.service-now.com/$pa_dashboard.do?sysparm_dashboard=175bcd319359da50e10ab86efaba102b&sysparm_tab=631c45b59359da50e10ab86efaba1043&sysparm_cancelable=true&sysparm_editable=false&sysparm_active_panel=false'
$data2 = Get-SnowReportScrape -Type "changeRequests" -Url 'https://wellspan.service-now.com/$pa_dashboard.do?sysparm_dashboard=175bcd319359da50e10ab86efaba102b&sysparm_tab=e02f9ca897a76ed0e17277971153af4a&sysparm_cancelable=true&sysparm_editable=false&sysparm_active_panel=false'
$data3 = Get-SnowReportScrape -Type "catalogTasks" -Url 'https://wellspan.service-now.com/$pa_dashboard.do?sysparm_dashboard=175bcd319359da50e10ab86efaba102b&sysparm_tab=a1ab05f19359da50e10ab86efaba1059&sysparm_cancelable=true&sysparm_editable=undefined&sysparm_active_panel=false'
$data = $data1 + $data2 + $data3
$data | Export-Csv .\output.csv
```

{{ Gather the SNOW data and store in a CSV.  The data scraped from the SNOW report is state counts by assignment group for Incidents, Change Requests, and Catalog Tasks. }}

## PARAMETERS

### -Type

{{ Fill Type Description }}

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: 1
Default value: None
Accept pipeline input: True (ByPropertyName, ByValue)
Accept wildcard characters: False
```

### -Url

{{ Fill Url Description }}

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: 2
Default value: None
Accept pipeline input: True (ByPropertyName, ByValue)
Accept wildcard characters: False
```

### CommonParameters

This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.String

## OUTPUTS

### System.Object

## NOTES

## RELATED LINKS
