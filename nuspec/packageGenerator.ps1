function PublishTweets {
    param (
        [string]$message,
        [string]$oauth_consumer_key,
        [string]$oauth_consumer_secret,
        [string]$oauth_token,
        [string]$oauth_token_secret
    )
    
    [Reflection.Assembly]::LoadWithPartialName("System.Security")  
    [Reflection.Assembly]::LoadWithPartialName("System.Net")  

    $status = [System.Uri]::EscapeDataString($message);

    $oauth_nonce = [System.Convert]::ToBase64String([System.Text.Encoding]::ASCII.GetBytes([System.DateTime]::Now.Ticks.ToString()));  
    $ts = [System.DateTime]::UtcNow - [System.DateTime]::ParseExact("01/01/1970", "dd/MM/yyyy", $null).ToUniversalTime();  
    $oauth_timestamp = [System.Convert]::ToInt64($ts.TotalSeconds).ToString();  

    $signature = "POST&";  
    $signature += [System.Uri]::EscapeDataString("https://api.twitter.com/1.1/statuses/update.json") + "&";  
    $signature += [System.Uri]::EscapeDataString("oauth_consumer_key=" + $oauth_consumer_key + "&");  
    $signature += [System.Uri]::EscapeDataString("oauth_nonce=" + $oauth_nonce + "&");   
    $signature += [System.Uri]::EscapeDataString("oauth_signature_method=HMAC-SHA1&");  
    $signature += [System.Uri]::EscapeDataString("oauth_timestamp=" + $oauth_timestamp + "&");  
    $signature += [System.Uri]::EscapeDataString("oauth_token=" + $oauth_token + "&");  
    $signature += [System.Uri]::EscapeDataString("oauth_version=1.0&");  
    $signature += [System.Uri]::EscapeDataString("status=" + $status);  

    $signature_key = [System.Uri]::EscapeDataString($oauth_consumer_secret) + "&" + [System.Uri]::EscapeDataString($oauth_token_secret);  

    $hmacsha1 = new-object System.Security.Cryptography.HMACSHA1;  
    $hmacsha1.Key = [System.Text.Encoding]::ASCII.GetBytes($signature_key);  
    $oauth_signature = [System.Convert]::ToBase64String($hmacsha1.ComputeHash([System.Text.Encoding]::ASCII.GetBytes($signature)));  

    $oauth_authorization = 'OAuth ';  
    $oauth_authorization += 'oauth_consumer_key="' + [System.Uri]::EscapeDataString($oauth_consumer_key) + '",';  
    $oauth_authorization += 'oauth_nonce="' + [System.Uri]::EscapeDataString($oauth_nonce) + '",';  
    $oauth_authorization += 'oauth_signature="' + [System.Uri]::EscapeDataString($oauth_signature) + '",';  
    $oauth_authorization += 'oauth_signature_method="HMAC-SHA1",'  
    $oauth_authorization += 'oauth_timestamp="' + [System.Uri]::EscapeDataString($oauth_timestamp) + '",'  
    $oauth_authorization += 'oauth_token="' + [System.Uri]::EscapeDataString($oauth_token) + '",';  
    $oauth_authorization += 'oauth_version="1.0"';  

    $post_body = [System.Text.Encoding]::ASCII.GetBytes("status=" + $status);   
    [System.Net.HttpWebRequest] $request = [System.Net.WebRequest]::Create("https://api.twitter.com/1.1/statuses/update.json");  
    $request.Method = "POST";  
    $request.Headers.Add("Authorization", $oauth_authorization);  
    $request.ContentType = "application/x-www-form-urlencoded";  
    $body = $request.GetRequestStream();  
    $body.write($post_body, 0, $post_body.length);  
    $body.flush();  
    $body.close();  
    $response = $request.GetResponse();
}

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
PublishTweets -message $messageToPublish -oauth_consumer_key $oauth_consumer_key -oauth_consumer_secret $oauth_consumer_secret -oauth_token $oauth_token -oauth_token_secret $oauth_token_secret