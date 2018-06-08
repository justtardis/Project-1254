<?php
$servername = "casesim.ru";
$username = "justtardis";
$password = "138976Al";
$dbName = "justtardis";

//Variable from the user
$user_name = $_POST["user_name"];//"Nastya";
$device_id = $_POST["device_id"];//"53cgfdd12chg4sd7657";

$conn = new mysqli($servername, $username, $password, $dbName);
if (!$conn){
	die("Connection Failed. ". mysqli_connect_error());
}
$sql = "SELECT device_id, user_name, case_num FROM `users` WHERE `device_id` = '".$device_id."'";
$result = mysqli_query($conn, $sql);

if(mysqli_num_rows($result) > 0){
	while($row = mysqli_fetch_assoc($result))
	{
		if($device_id == $row['device_id'])
			//echo "true";
			echo $row['user_name']."—".$row['case_num'];
		else
			echo "false";
	}
}
else{
	$sql = "INSERT INTO users (device_id, user_name) 
	VALUES ('".$device_id."','".$user_name."')";
	$result = mysqli_query($conn, $sql);
	if(!$result)
		echo "error";
	else 
		$sql_data = "SELECT device_id, user_name, case_num FROM `users` WHERE `device_id` = '".$device_id."'";
		$res = mysqli_query($conn, $sql_data);
		if(mysqli_num_rows($res) > 0){
		while($row_data = mysqli_fetch_assoc($res))
		{
		if($device_id == $row_data['device_id'])
			//echo "true";
			echo $row_data['user_name']."—".$row_data['case_num'];
		else
			echo "false";
		}
}
}
?>