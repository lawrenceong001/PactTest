param ($hostNameWithPort, $consumerName)


if ([string]::IsNullOrEmpty($hostNameWithPort)){
	$hostNameWithPort = "http://localhost:9292"
}

if ([string]::IsNullOrEmpty($consumerName)){
	$consumerName = "Consumer1"
}

write-host "host Name With Port: " $hostNameWithPort
write-host "Consumer Name: " $consumerName

$Uri = "$hostNameWithPort/pacticipants/$consumerName"
write-host "Uri: " $Uri 
invoke-restmethod -Method Delete -uri $Uri 

