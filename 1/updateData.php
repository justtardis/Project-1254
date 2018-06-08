<?php
	$servername = "casesim.ru";
	$username = "justtardis";
	$password = "138976Al";
	$dbName = "justtardis";
	
	$device_id = $_POST["device_id"];
	$cases = $_POST["cases"];
	//echo $cases;
	$conn = new mysqli($servername, $username, $password, $dbName);
	if (!$conn){
		die("Connection Failed. ". mysqli_connect_error());
	}
	$sql = "UPDATE users SET case_num = '".$cases."' WHERE device_id = '". $device_id."'"; 
    //$sql = "UPDATE Users SET cash='".$cash."' WHERE email='".$email."'";
	$result = mysqli_query($conn, $sql);
	if (!$result) echo "error";
	else echo "ok";
?>