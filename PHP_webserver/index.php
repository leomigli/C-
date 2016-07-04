<?php
   $mysqli = new mysqli('localhost', '<USER>', '<PASSWORD>', 'geolog');
   if ($mysqli->connect_errno) {
      echo "Failed to connect to MySQL: (" . $mysqli->connect_errno . ") " . $mysqli->connect_error;
      exit();
   }

   if (!($stmt = $mysqli->prepare("INSERT INTO entries(Latitude, Longitude, Device, Annotation) VALUES (?, ?, ?, ?)"))) {
       echo "Prepare failed: (" . $mysqli->errno . ") " . $mysqli->error;
   }
   
   $latitude   = $_GET['lt'];
   $longitude  = $_GET['ln'];
   $device     = $_GET['d'];
   $annotation = $_GET['n'];
   
   if (!$stmt->bind_param("ddss", $latitude, $longitude, $device, $annotation)) {
      echo "Binding parameters failed: (" . $stmt->errno . ") " . $stmt->error;
   }

   if (!$stmt->execute()) {
       echo "Execute failed: (" . $stmt->errno . ") " . $stmt->error;
   }   

   $stmt->close();
   mysqli_close($mysqli);
?>