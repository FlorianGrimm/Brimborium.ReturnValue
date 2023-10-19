$ProjectTest = $PSScriptRoot

Write-Host "ProjectTest $ProjectTest"
cd $ProjectTest
$TestResults = "$ProjectTest\TestResults"
if (Test-Path -LiteralPath $TestResults) {
	Get-Item -LiteralPath $TestResults  -ErrorAction Continue | Remove-Item -Recurse
}
dotnet test --collect:"XPlat Code Coverage"
$CurrentTest = @(Get-ChildItem -LiteralPath $TestResults | Sort-Object LastWriteTime -Descending | Select-Object -First 1)[0].FullName 
$CurrentTestFile = $CurrentTest + "\coverage.cobertura.xml"
reportgenerator -reports:$CurrentTestFile -targetdir:$CurrentTest -reporttypes:Html
$CurrentTestIndex = $CurrentTest + "\Index.html"
Start-Process $CurrentTestIndex

