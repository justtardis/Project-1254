<?php
	$servername = "casesim.ru";
	$username = "justtardis";
	$password = "138976Al";
	$dbName = "justtardis";
	
	$conn = new mysqli($servername, $username, $password, $dbName);
	if (!$conn){
		die("Connection Failed. ". mysqli_connect_error());
	}
	$sql = "SELECT COUNT(*) FROM users"; 
	$result = mysqli_query($conn, $sql);
	$row = mysqli_fetch_array($result);
	$data = $row[0];
	$sql = "SELECT SUM(case_num) FROM users"; 
	$result = mysqli_query($conn, $sql);
	$row = mysqli_fetch_array($result);
	$data = $data . "|" . $row[0];
	echo ($data);
?>