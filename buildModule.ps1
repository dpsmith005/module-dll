$module = "moduleDll"
#$buildModule = ".\bin\Debug\netstandard2.0\moduleDll.dll"
$publishModule = ".\bin\Debug\netstandard2.0\publish\moduleDll.dll"
#$publishModule = "E:\OneDrive - WellSpan Health\SourceCode\module-dll\bin\Debug\netstandard2.0\publish\moduleDll.dll"
#$testUrl = "https://wellspan.service-now.com/now/nav/ui/classic/params/target/%24pa_dashboard.do%3Fsysparm_dashboard%3D175bcd319359da50e10ab86efaba102b%26sysparm_tab%3D631c45b59359da50e10ab86efaba1043%26sysparm_cancelable%3Dtrue%26sysparm_editable%3Dfalse%26sysparm_active_panel%3Dfalse"

# Add type to use specified dll
Add-Type -Path "E:\OneDrive - WellSpan Health\SourceCode\module-dll\bin\Debug\netstandard2.0\HtmlAgilityPack.dll"
Add-Type -Path "E:\OneDrive - WellSpan Health\SourceCode\module-dll\bin\Debug\netstandard2.0\WebDriver.dll"
Add-Type -Path "E:\OneDrive - WellSpan Health\SourceCode\module-dll\bin\Debug\netstandard2.0\WebDriver.Support.dll"

# Copy Selenium Manager  to powershell 7 folder
#Copy-Item "C:\Users\dsmith14admin\.nuget\packages\selenium.webdriver\4.35.0\runtimes\win\native\selenium-manager.exe" "C:\Program Files\PowerShell\7\selenium-manager.exe"  

# list added assemblies
$asm = [AppDomain]::CurrentDomain.GetAssemblies()
$asm | Where-Object { $_.fullname -match "html" }

# unload the module before building or publishing
if (get-module $module) { remove-module $module }
#dotnet build
#Import-Module $buildModule
dotnet publish
Import-Module $publishModule
New-ExternalHelp -path ".\help" -OutputPath ".\bin\debug\netstandard2.0\publish"

# Run sample cmdlet
Test-SampleCmdlet 7 "Cat"

# Run another sample cmdlet
Get-MyStuff 61 "http://dpstest202.wellspan.org"

# use platyPS to build help file <module>.dll-Help.xml
# this file uses markdown for the syntax
# import-module platyPS

# When including other assemblies the .csproj needs to be modified to include them
# in the <PropertyGroup> section add: <CopyLocalLockFileAssemblies>true</CopyLocalLockFileAssemblies>

# Info for the initial build from https://dev.to/wozzo/creating-a-powershell-binary-module-4e5o