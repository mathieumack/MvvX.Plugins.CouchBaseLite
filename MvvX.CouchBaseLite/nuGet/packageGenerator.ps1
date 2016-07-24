$location  = Get-Location

$location.Path

$strPath = $location.Path + '\..\MvvX.Plugins.CouchBaseLite\bin\Release\MvvX.Plugins.CouchBaseLite.dll'

$strPath

$Assembly = [Reflection.Assembly]::Loadfile($strPath)
$AssemblyName = $Assembly.GetName()
$Assemblyversion =  $AssemblyName.version
$Assemblyversion

$nuSpecFile =  $location.Path + '\MvvX.Plugins.CouchBaseLite.nuspec'

(Get-Content $nuSpecFile) | 
Foreach-Object {$_ -replace "(<version>([0-9.]+)<\/version>)", "<version>$Assemblyversion</version>" } | 
Set-Content $nuSpecFile

.\NuGet pack MvvX.Plugins.CouchBaseLite.nuspec


$nuSpecFile =  $location.Path + '\MvvX.Plugins.CouchBaseLite.ForestDB.nuspec'

(Get-Content $nuSpecFile) | 
Foreach-Object {$_ -replace "(<version>([0-9.]+)<\/version>)", "<version>$Assemblyversion</version>" } | 
Set-Content $nuSpecFile

.\NuGet pack MvvX.Plugins.CouchBaseLite.ForestDB.nuspec

$nuSpecFile =  $location.Path + '\MvvX.Plugins.CouchBaseLite.SQLCipher.nuspec'

(Get-Content $nuSpecFile) | 
Foreach-Object {$_ -replace "(<version>([0-9.]+)<\/version>)", "<version>$Assemblyversion</version>" } | 
Set-Content $nuSpecFile

.\NuGet pack MvvX.Plugins.CouchBaseLite.SQLCipher.nuspec

$apiKey = Read-Host 'Please set apiKey to publish to nuGet :'
	
.\NuGet push MvvX.Plugins.CouchBaseLite.$Assemblyversion.nupkg -Source https://www.nuget.org/api/v2/package -ApiKey $apiKey
.\NuGet push MvvX.Plugins.CouchBaseLite.ForestDB.$Assemblyversion.nupkg -Source https://www.nuget.org/api/v2/package -ApiKey $apiKey
.\NuGet push MvvX.Plugins.CouchBaseLite.SQLCipher.$Assemblyversion.nupkg -Source https://www.nuget.org/api/v2/package -ApiKey $apiKey
