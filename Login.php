<?php
$servername = "localhost";
$username = "id5689441_root";
$password = "rootroot";
$dbName = "id5689441_case_sim";

//Variable from the user
$user_name = "Kartoshka";
$google_id = "jdaksjkjr324jklsdjfds";

$conn = new mysqli($servername, $username, $password, $dbName);
if (!$conn){
	die("Connection Failed. ". mysqli_connect_error());
}
$sql = "SELECT `Google_ID` FROM `users` WHERE Google_ID = '".$google_id."'";
$result = mysqli_query($conn, $sql);

if(mysqli_num_rows($result) > 0){
	while($row = mysqli_fetch_assoc($result))
	{
		if($row['Google_ID'] == $google_id)
			echo "true";//$row['Google_ID'];
		else
			echo "false";
	}
}
else{
	$sql = "INSERT INTO users (google_id, user_name) 
	VALUES ('".$user_name."', '".$google_id."')";
	$result = mysqli_query($conn, $sql);
	if(!$result)
		echo "error";
	else 
		echo "OK";	
}

?>

