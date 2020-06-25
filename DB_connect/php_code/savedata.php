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
		$score = mysqli_real_escape_string($connection, json_decode($_POST["json"])->score) ;

		//check if name exists
		$updatePlayerQuery = "UPDATE players SET pb_score = " . $score . " WHERE username='" . $username . "';" ;

		mysqli_query($connection, $updatePlayerQuery) or die(json_encode(array("code"=>9, "details"=>"player update failed!"))) ;

		if (mysqli_affected_rows($connection) == 0) {
			echo json_encode(array("code"=>6, "details"=>"username not found")) ;
			exit ;
		} else if (mysqli_affected_rows($connection) > 1) {
			echo json_encode(array("code"=>7, "details"=>"duplicate username found")) ;
			exit ;
		}

		echo json_encode(array("code"=>0, "details"=>"successfully updated score"));
	}
?>
