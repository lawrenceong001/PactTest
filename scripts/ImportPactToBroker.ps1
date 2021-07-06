param ($version, $pactFile, $hostNameWithPort, $providerName, $consumerName)

if ([string]::IsNullOrEmpty($version)){
	$version = "1.0.0"
}

if ([string]::IsNullOrEmpty($pactFile)){
	$pactFile = "pacts\consumer1-testapi1.json"
}

if ([string]::IsNullOrEmpty($hostNameWithPort)){
	$hostNameWithPort = "http://localhost:9292"
}

if ([string]::IsNullOrEmpty($providerName)){
	$providerName = "TestApi1"
}

if ([string]::IsNullOrEmpty($consumerName)){
	$consumerName = "Consumer1"
}

write-host "Version: " $version
write-host "Pact File: " $pactFile
write-host "host Name With Port: " $hostNameWithPort
write-host "Consumer Name: " $consumerName
write-host " Provider Name: " $providerName

$textContent = get-content "$pactFile"
$Uri = "$hostNameWithPort/pacts/provider/$providerName/consumer/$consumerName/version/$version"
write-host "Uri: " $Uri 
invoke-restmethod -Method Put -uri $Uri -contentType "application/json" -body "$textContent"