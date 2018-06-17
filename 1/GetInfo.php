<?php
	$servername = "casesim.ru";
	$username = "justtardis";
	$password = "138976Al";
	$dbName = "justtardis";
	
	$conn = new mysqli($servername, $username, $password, $dbName);
	if (!$conn){
		die("Connection Failed. ". mysqli_connect_error());
	}
	$sql = "SELECT header, description, date_final, link FROM news";
    $result = mysqli_query($conn, $sql);
    if(mysqli_num_rows($result) > 0){
while($row = mysqli_fetch_assoc($result))
{
	echo $row['header'] . "—" . $row['description'] . "—" . $row['date_final'] . "—" . $row['link']."<br/>";
}
}

   
?>