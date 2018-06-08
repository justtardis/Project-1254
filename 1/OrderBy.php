<?php
$servername = "casesim.ru";
$username = "justtardis";
$password = "138976Al";
$dbName = "justtardis";

$user_name = $_POST["user_name"];
$pos = 1;

$conn = new mysqli($servername, $username, $password, $dbName);
if (!$conn){
	die("Connection Failed. ". mysqli_connect_error());
}

$sql = "SELECT * FROM users ORDER BY case_num DESC LIMIT 0, 10";
$result = mysqli_query($conn, $sql);
if(mysqli_num_rows($result) > 0){
while($row = mysqli_fetch_assoc($result))
{
	echo $row['user_name'].":".$row['case_num']."<br/>";	
}
}

$sql_current = "SELECT * FROM users ORDER BY case_num DESC";
$res = mysqli_query($conn, $sql_current);
if(mysqli_num_rows($res) > 0){
	while($rows = mysqli_fetch_assoc($res) and $rows['user_name']!=$user_name){
	//echo $pos." ".$rows['user_name'].":".$rows['case_num']."<br/>";	
	$pos+=1;
	}
}
echo $pos;

?>