write-host "**************************" -foreground "Cyan"
write-host "*   Packaging to nuget   *" -foreground "Cyan"
write-host "**************************" -foreground "Cyan"

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

write-host "Generate nuget packages" -foreground "Green"
.\NuGet.exe pack MvvX.Plugins.CouchBaseLite.nuspec
.\NuGet.exe pack MvvX.Plugins.CouchBaseLite.ForestDB.nuspec
.\NuGet.exe pack MvvX.Plugins.CouchBaseLite.SQLCipher.nuspec

$apiKey = $env:NuGetApiKey
	
write-host "Publish nuget packages" -foreground "Green"	
#.\NuGet push MvvX.Plugins.CouchBaseLite.$ProductVersion.nupkg -Source https://www.nuget.org/api/v2/package -ApiKey $apiKey
#.\NuGet push MvvX.Plugins.CouchBaseLite.ForestDB.$ProductVersion.nupkg -Source https://www.nuget.org/api/v2/package -ApiKey $apiKey
#.\NuGet push MvvX.Plugins.CouchBaseLite.SQLCipher.$ProductVersion.nupkg -Source https://www.nuget.org/api/v2/package -ApiKey $apiKey

$oauth_consumer_key = $env:oauth_consumer_key
$oauth_consumer_secret = $env:oauth_consumer_secret
$oauth_token = $env:oauth_token
$oauth_token_secret = $env:oauth_token_secret
$messageToPublish = "New #mvvmcross plugin version for #CouchBaseLite. Version : $ProductVersion. https://www.nuget.org/packages/MvvX.Plugins.CouchBaseLite/"

write-host "Send notification for new version" -foreground "Green"	
write-host $messageToPublish -foreground "DarkGray"	
.\Send-Tweet.ps1 -message $messageToPublish -oauth_consumer_key $oauth_consumer_key -oauth_consumer_secret $oauth_consumer_secret -oauth_token $oauth_token -oauth_token_secret $oauth_token_secret
