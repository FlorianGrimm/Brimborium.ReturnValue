dotnet tool install -g dotnet-reportgenerator-globaltool

dotnet test --collect:"XPlat Code Coverage"

dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura

reportgenerator -reports:
"D:\github.com\FlorianGrimm\Brimborium.ReturnValue\Brimborium.ReturnValue.Test\TestResults\a690485b-ccee-425c-9001-c46f6060cbc0\coverage.cobertura.xml"
-targetdir:"D:\github.com\FlorianGrimm\Brimborium.ReturnValue\Brimborium.ReturnValue.Test\TestResults" -reporttypes:Html