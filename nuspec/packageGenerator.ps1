
$location  = $env:APPVEYOR_BUILD_FOLDER

$locationNuspec = $location + "\nuspec"
$locationNuspec
	
Set-Location -Path $locationNuspec

write-host "Update the nuget.exe file" -foreground "DarkGray"
.\NuGet update -self

"Packaging to nuget..."
"Build folder : " + $location

$strPath = $location + '\MvvX.Plugins.CouchBaseLite\bin\Release\MvvX.Plugins.CouchBaseLite.dll'

$VersionInfos = [System.Diagnostics.FileVersionInfo]::GetVersionInfo($strPath)
$ProductVersion = $VersionInfos.ProductVersion + "-nightly"
"Product version : " + $ProductVersion

"Update nuspec versions ..."
	
$nuSpecFile =  $locationNuspec + '\MvvX.Plugins.CouchBaseLite.nuspec'
(Get-Content $nuSpecFile) | 
Foreach-Object {$_ -replace "{BuildNumberVersion}", "$ProductVersion" } | 
Set-Content $nuSpecFile

$nuSpecFile =  $locationNuspec + '\MvvX.Plugins.CouchBaseLite.ForestDB.nuspec'
(Get-Content $nuSpecFile) | 
Foreach-Object {$_ -replace "{BuildNumberVersion}", "$ProductVersion" } | 
Set-Content $nuSpecFile

$nuSpecFile =  $locationNuspec + '\MvvX.Plugins.CouchBaseLite.SQLCipher.nuspec'
(Get-Content $nuSpecFile) | 
Foreach-Object {$_ -replace "{BuildNumberVersion}", "$ProductVersion" } | 
Set-Content $nuSpecFile

"Generate nuget packages ..."
.\NuGet.exe pack MvvX.Plugins.CouchBaseLite.nuspec
.\NuGet.exe pack MvvX.Plugins.CouchBaseLite.ForestDB.nuspec
.\NuGet.exe pack MvvX.Plugins.CouchBaseLite.SQLCipher.nuspec

$apiKey = $env:NuGetApiKey
	
"Publish packages ..."	
.\NuGet push MvvX.Plugins.CouchBaseLite.$ProductVersion.nupkg -Source https://www.nuget.org/api/v2/package -ApiKey $apiKey
.\NuGet push MvvX.Plugins.CouchBaseLite.ForestDB.$ProductVersion.nupkg -Source https://www.nuget.org/api/v2/package -ApiKey $apiKey
.\NuGet push MvvX.Plugins.CouchBaseLite.SQLCipher.$ProductVersion.nupkg -Source https://www.nuget.org/api/v2/package -ApiKey $apiKey