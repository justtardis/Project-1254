<?php
$servername = "casesim.ru";
$username = "justtardis";
$password = "138976Al";
$dbName = "justtardis";

//Variable from the user
$user_name = $_POST["user_name"];//"Nastya";
$device_id =  $_POST["device_id"];//"53cgfdd12chg4sd7657";

$conn = new mysqli($servername, $username, $password, $dbName);
if (!$conn){
	die("Connection Failed. ". mysqli_connect_error());
}


$sql = "UPDATE users SET user_name = '".$user_name."' WHERE device_id = '". $device_id."'"; 
    //$sql = "UPDATE Users SET cash='".$cash."' WHERE email='".$email."'";
	$result = mysqli_query($conn, $sql);
	if (!$result) echo "error";
	else echo "ok";


?>