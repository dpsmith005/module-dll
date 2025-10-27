dotnet new install Microsoft.PowerShell.Standard.Module.Template
dotnet new psmodule -n MyModuleName

# Add assembly reference
dotnet add package HtmlAgilityPack
#dotnet add package Newtonsoft.Json
dotnet add package Selenium.WebDriver
dotnet add package Selenium.Support
dotnet add package Selenium.WebDriver.ChromeDriver # or other browser driver
dotnet add package Selenium.WebDriver.EdgeDriver
# dotnet add package Microsoft.Edge.SeleniumTools - obsolete use selenium 4