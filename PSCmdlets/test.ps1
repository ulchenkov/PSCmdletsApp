<#
 dotnet publish -o ref -r win-x64
 dotnet publish -o ref -r linux-x64
 #>

Import-Module .\ref\PSCmdlets.dll

$dt = Get-RandomTable -RowQuantity 10000