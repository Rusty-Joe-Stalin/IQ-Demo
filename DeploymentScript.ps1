[CmdletBinding()]
param
(
 [Parameter(Mandatory = $true)]
 [String]
 $username,
 [Parameter(Mandatory = $true)]
 [String]
 $password 
)

#Restore build packages 
dotnet restore 

#Test Solution 
dotnet test

#Publish the package
dotnet publish .\DemoProject\DemoProject.csproj -c Release -f netcoreapp3.0

#Create Zip file
Compress-Archive -Path ".\DemoProject\bin\release\netcoreapp3.0\publish\*" -DestinationPath ".\DemoProject" -Force

#Prep Kudu zip deploy
$apiUrl = "https://kyles-iq-demo.scm.azurewebsites.net/api/zipdeploy"
$base64AuthInfo = [Convert]::ToBase64String([Text.Encoding]::ASCII.GetBytes(("{0}:{1}" -f $username, $password)))
$userAgent = "powershell/1.0"

#Deploy packkge to azure
Invoke-RestMethod -Uri $apiUrl -Headers @{Authorization=("Basic {0}" -f $base64AuthInfo)} -UserAgent $userAgent -Method POST -InFile "./*.zip" -ContentType "multipart/form-data"

#Open browser to ping endpoint
Start-Process "https://kyles-iq-demo.azurewebsites.net/ping"
