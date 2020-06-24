<?php

	include 'db_connect.php';
	$connection = mysqli_connect($dbhost, $dbuser, $dbpasswd, $dbname);

	//check that the connection happened
	if (mysqli_connect_errno()) {
		echo json_encode(array("code"=>1, "details"=>"DB connection failed")) ;
		exit ;
	}

	//secure charset
	mysqli_query($connection, 'SET NAMES utf8;');

	if ( ($_POST == null) || ($_POST["json"] == null) ) {
		echo json_encode(array("code"=>5, "details"=>"No data sent")) ;
		exit ;
	} else {
		$username = mysqli_real_escape_string($connection, json_decode($_POST["json"])->username) ;
		$password = mysqli_real_escape_string($connection, json_decode($_POST["json"])->password) ;

		//check if name exists
		$nameCheckQuery = "SELECT username FROM players WHERE username='" . $username . "';" ;

		$nameCheck = mysqli_query($connection, $nameCheckQuery) or die(json_encode(array("code"=>2, "details"=>"name check query failed"))) ;

		if (mysqli_num_rows($nameCheck) > 0) {
			echo json_encode(array("code"=>3, "details"=>"Username already exists")) ;
			exit ;
		}
		
		//convert password to salt and hash -- SHA256 encryption method
		$salt = "\$5\$rounds=5000\$" . "prettybirdies" . $username . "\$";
		$hash = crypt($password, $salt);

		//add user to the table
		$insertUserQuery = "INSERT INTO players(username, pw_hash, pw_salt) VALUES ('" . $username . "', '" . $hash . "', '" . $salt . "');" ;

		mysqli_query($connection, $insertUserQuery) or die(json_encode(array("code"=>4, "details"=>"insert player query failed"))) ;

		echo json_encode(array("code"=>0, "details"=>"successfully registered")) ;
	}
?>
