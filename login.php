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
		$nameCheckQuery = "SELECT pw_salt, pw_hash, pb_score FROM players WHERE username='" . $username . "';" ;

		$nameCheck = mysqli_query($connection, $nameCheckQuery) or die(json_encode(array("code"=>2, "details"=>"name check query failed"))) ;

		if (mysqli_num_rows($nameCheck) == 0) {
			echo json_encode(array("code"=>6, "details"=>"username not found")) ;
			exit ;
		} else if (mysqli_num_rows($nameCheck) > 1) {
			echo json_encode(array("code"=>7, "details"=>"duplicate username found")) ;
			exit ;
		}
	
		$userInfo = mysqli_fetch_assoc($nameCheck) ;
	
		//check password via salt and hash -- SHA256 encryption method
		if ($userInfo["pw_hash"] != crypt($password, $userInfo["pw_salt"]) ) {
			echo json_encode(array("code"=>8, "details"=>"incorrect password")) ;
			exit ;
		}

		echo json_encode(array("code"=>0, "details"=>"successfully registered", "player"=>array("username"=>$username,"score"=>$userInfo["pb_score"]))) ;
	}
?>
