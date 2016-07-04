<html>
   <head>
      <title>GeoLog</title>
      
      <style>
         *{
            font: normal 12px 'Segoe UI';
         }
         
         table{
            border: 1px solid lightgray;
            border-spacing: 0px;
         }
         
         th{
            font-weight: bold;
            background-color:#DDD;
            padding: 0 10px 0 10px;
         }
         
         td{
            border: 1px solid lightgray;
            padding: 6px;
         }
      </style>
   </head>


   <body>

      <table>
         <tr>
            <th>Id</th>
            <th>Date</th>
            <th>Lat.</th>
            <th>Long.</th>
            <th>Device</th>
            <th>Annotations</th>
            <th>Map</th>
         </tr>
         
         <?php

          $mysqli = new mysqli('localhost', '<USER>', '<PASSWORD>', 'geolog');
          if ($mysqli->connect_errno) {
             echo "Failed to connect to MySQL: (" . $mysqli->connect_errno . ") " . $mysqli->connect_error;
             exit();
          }

          $query = "SELECT * FROM entries ORDER BY IdEntry";
          $result = $mysqli->query($query);
          
          while($row = $result->fetch_array()){ ?>
          
             <tr>
                <td><?php echo $row['IdEntry'];?></td>
                <td><?php echo $row['TimeStamp'];?></td>
                <td><?php echo $row['Latitude'];?></td>
                <td><?php echo $row['Longitude'];?></td>
                <td><?php echo $row['Device'];?></td>
                <td><?php echo $row['Annotation'];?></td>
                <td>[<a href="map.php?lt=<?php echo $row['Latitude'];?>&ln=<?php echo $row['Longitude'];?>&d=<?php echo $row['Device'];?>&n=<?php echo $row['Annotation'];?>">Link</a>]</td>
             </tr>
          <?php }

          $result->close();
          $mysqli->close();
         ?>
   
   </body>
</html>