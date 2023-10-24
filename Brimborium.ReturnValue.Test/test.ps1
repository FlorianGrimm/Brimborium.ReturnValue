$ProjectTest = $PSScriptRoot

Write-Host "ProjectTest $ProjectTest"
cd $ProjectTest
dotnet test
if (-not $?) {
	Write-Host "dotnet test ERROR"
} else {
	$TestResults = "$ProjectTest\TestResults"
	if (Test-Path -LiteralPath $TestResults) {
		Get-Item -LiteralPath $TestResults  -ErrorAction Continue | Remove-Item -Recurse
	}
	dotnet test --collect:"XPlat Code Coverage"
	if (-not $?) {
		Write-Host "dotnet test --collect:"XPlat Code Coverage" ERROR"
	} else {
		Write-Host "dotnet test --collect:"XPlat Code Coverage" OK"
		$CurrentTest = @(Get-ChildItem -LiteralPath $TestResults | Sort-Object LastWriteTime -Descending | Select-Object -First 1)[0].FullName 
		$CurrentTestFile = $CurrentTest + "\coverage.cobertura.xml"
		reportgenerator -reports:$CurrentTestFile -targetdir:$CurrentTest -reporttypes:Html
		if (-not $?) {
			Write-Host "reportgenerator ERROR"
			dotnet tool install -g dotnet-reportgenerator-globaltool
		}
		$CurrentTestIndex = $CurrentTest + "\Index.html"
		Start-Process $CurrentTestIndex
	}
}