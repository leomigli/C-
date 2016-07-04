<?php
   $latitude   = $_GET['lt'];
   $longitude  = $_GET['ln'];
   $device     = $_GET['d'];
   $annotation = $_GET['n'];
?>

<html>
  <head>
    <title>GeoLog</title>
    <meta name="viewport" content="initial-scale=1.0, user-scalable=no">
    <meta charset="utf-8">
    <style>
      html, body, #map {
        height: 100%;
        margin: 0px;
        padding: 0px;
		width:100%;
      }
    </style>
    <script src="https://maps.googleapis.com/maps/api/js?v=3.exp&signed_in=true&language=it"></script>
    <script>
	   var initialize = function(){
           var latlng = new google.maps.LatLng(<?php echo $latitude, ",", $longitude;?>);
           var options = { zoom: 18,
                           center: latlng,
                           mapTypeId: google.maps.MapTypeId.ROADMAP
                         };
           var map = new google.maps.Map(document.getElementById('map'), options);
		   
		   var marker = new google.maps.Marker({ position: latlng,
                                                 map: map, 
                                                 title: '<?php echo $device;?>' });
	   }
	   
	   window.onload = initialize;
    </script>
  </head>
  <body>
     <div id="map"></div>
  </body>
</html>