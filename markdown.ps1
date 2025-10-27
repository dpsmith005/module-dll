write-host psscriptRoot : $PSScriptRoot
import-module "$PSScriptRoot\bin\Debug\netstandard2.0\moduleDll.dll"

New-MarkdownHelp -module moduleDll -outputFolder "$PSScriptRoot\help"
#Update-MarkdownHelp .\help
#Update-MarkdownHelpModule .\help
New-MarkdownAboutHelp -OutputFolder "$PSScriptRoot\help" -AboutName "about_moduleDll"

New-ExternalHelp -path ".\help" -OutputPath ".\bin\debug\netstandard2.0\publish"

# This uses the module platyPS to create the markdown files
