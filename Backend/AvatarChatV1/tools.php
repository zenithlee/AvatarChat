<?php
require_once 'database.php';

function GetToken( $email, $pass, $screenname){
    global $db;
    $query = "SELECT * FROM at_users WHERE `email`='$email';";
    $result = $db->query($query);    
    $temparray = array();
    while($row =mysqli_fetch_assoc($result))
    {
        $temparray[] = $row['token'];
    }
    
    if ( empty( $temparray )) {
        http_response_code(401);
        die ("ERROR");
    }
    
    return $temparray[0];
}

function Register( $email, $pass, $screenname ) {
    global $db;
    $uid = uniqid();
    $query = "INSERT INTO at_users (`email`,`password`,`screenname`, `token`) VALUES('$email','$pass', '$screenname', '$uid')";
    $result = $db->query($query);
    if ( $result ) {
          return `{"result":"ok"}`;
      }
      return `{"result":"FAILED"}`;
}

//get a list of all stocks form the exchange
function GetInstruments() {
    global $db;
    $query = "SELECT * FROM instruments ORDER BY symbol;";
    $result = $db->query($query);
    $temparray = array();
    while($row =mysqli_fetch_assoc($result))
    {
        $temparray[] = $row;
    }
    return json_encode($temparray, true);
}

function GetInfo($exchange,$symbol) {
     global $db;
    $query = "SELECT * FROM sharenames where jsecode='$symbol';";
    $result = $db->query($query);
    $temparray = array();
    while($row =mysqli_fetch_assoc($result))
    {
        $temparray[] = $row;
    }
    return json_encode($temparray, true);

}

function InsertData($table, $column, $data) {
    global $db;
      $query = "INSERT INTO $table ($column) VALUES ('$data');";
      $result = $db->query($query);
      if ( $result ) {
          return `{"result":"ok"}`;
      }
}

function GetData($table, $column ) {
     global $db;
      $query = "SELECT * FROM $table;";
      $result = $db->query($query);
        if ($result->num_rows > 0) {
            // output data of each row
            $temparray = array();
            while($row =mysqli_fetch_assoc($result))
            {
                $temparray[] = $row;
            }
            return json_encode($temparray, true);
        }
        else
        {
            return `{"result":"failed"}`;
        }
}

function InsertQuote($symbol,$exchange,$date,$high,$low,$open,$close,$volume,$rating){
    global $db;

    $query = "SELECT * FROM quotes WHERE symbol='$symbol' AND date='$date';";
    $result = $db->query($query);
    $row =mysqli_fetch_assoc($result);

    if  ($row == null ) {
        $query = "INSERT INTO quotes (symbol, exchange,date,high,low,open,close,volume) "
            . "VALUES ('$symbol', '$exchange', '$date', '$high', '$low', '$open', '$close', '$volume');";
        $db->query($query);
    }
    else {
       // print_r($row);
        $id=$row['id'];
        //print("UPDATING ID ".$id."\n");
        $query = "REPLACE INTO quotes (id, symbol, exchange,date,high,low,open,close,volume) "
            . "VALUES ('$id', '$symbol', '$exchange', '$date', '$high', '$low', '$open', '$close', '$volume');";
        $db->query($query);
    }

}

function InsertSimple($symbol,$exchange,$datetime,$price){
    global $db;
    $query = "REPLACE INTO quotes (symbol,exchange,date,price) VALUES ('$symbol','$exchange','$datetime', '$price');";
    $db->query($query);
}

function GetQuote($symbol, $startDate, $endDate) {
    global $db;

    $start = strtotime($startDate);
    $end = strtotime($endDate);
    $days = ceil(abs($end - $start) / 86400);
    //if ( $days <=0 ) $days = 30;
   // if ( $days > 999 ) $days = 40;


    //$query = "SELECT * FROM quotes WHERE symbol='$symbol' GROUP BY date ASC LIMIT 50;";
    $query = "SELECT * from (SELECT * FROM quotes where symbol='$symbol' group by date DESC Order by date DESC LIMIT $days) as tbl order by date ASC;";
   // echo $query . "\n";
    $result = $db->query($query);
    if ($result->num_rows > 0) {
    // output data of each row
    $temparray = array();
    while($row =mysqli_fetch_assoc($result))
    {
        $row['days'] = $days;
        $temparray[] = $row;
    }
    return json_encode($temparray, true);

    /*while($row = $result->fetch_assoc()) {
        echo "id:" . $row["id"]. ",symbol:" . $row["symbol"]. ",datetime:".$row["datetime"]. ",price:" . $row["price"]
            .",high:".$row["high"]. ",low:".$row["low"].",open:".$row["open"].",close:".$row["close"].",rating:".$row["rating"].";<br>";
    }*/
} else {    return '{result:"failed"}';
}
}

function Safe($var)
{
    if ( isset( $_REQUEST[$var] ))
    {
        return $_REQUEST[$var];
    }
    return "";
}

function DoCurlAll() {
     global $db;
    $query = "SELECT * from instruments;";
$result = "";
     $results = $db->query($query);
    if ($results->num_rows > 0) {
        // output data of each row
        while($row =mysqli_fetch_assoc($results))
        {
            $symbol = $row['symbol'];
            $exchange = $row['exchange'];
            print("Preparing to update:" . $exchange . ":" . $symbol . "\n" );
            $result .= "Updating:" . $exchange . ":" . $symbol . "\n";
            DoCurl($symbol, $exchange);
            Import();
        }
    }
    return $result;
}

function DoCurl($symbol, $exchange) {

    $stsymbolock = strtoupper($symbol);
    $exchange = strtoupper($exchange);

    $xml = "http://www.google.com/finance/historical?q=$exchange%3A$symbol&ei=xKFVWMiXGsbAUOH0v_gK&output=csv";

    //return  $xml ;

    //$datei = "http://www.beispiel.de/meine_seite.php";
    if (function_exists('curl_version'))
    {
        $curl = curl_init();
        curl_setopt($curl, CURLOPT_URL, $xml);
        curl_setopt($curl, CURLOPT_RETURNTRANSFER, 1);
        $content = curl_exec($curl);
        curl_close($curl);
    }
    else if (file_get_contents(__FILE__) && ini_get('allow_url_fopen'))
    {
        $content = file_get_contents($xml);
    }
    else
    {
        $content = 'CURL not supported';
    }

    file_put_contents( "./data/fresh/".$exchange."/".$symbol.".csv", $content);

    return $content;
}

//http://www.google.com/finance/historical?q=GOOG&histperiod=daily&startdate=Jan+1+2015&enddate=Dec+15+2016&output=csv
//http://www.google.com/finance/historical?q=JSE%3ASNH&ei=xKFVWMiXGsbAUOH0v_gK&output=csv
function Import() {
    //foreach file in fresh
    $folder = "./data/";
    $files = glob($folder."fresh/JSE/*.csv");
    //print_r( $files );

    foreach( $files as $file ){
       // print ($file);
        $stock = strtoupper(basename($file,".csv"));

        $lines = file( $file );
        //print_r($lines);

$counter = 0;
        foreach( $lines as $line){
            $items = explode(",",$line);

             $sdate = $items[0];
            if ($counter==0 )
            {
                $counter++;
                continue;
            }

            $datetime = DateTime::createFromFormat('j-M-y', $sdate);
            $datetime = $datetime->format("Y-m-d");
            $open = $items[1];
            $high = $items[2];
            $low = $items[3];
            $close = $items[4];
            $volume = $items[5];
            InsertQuote($stock, "JSE", $datetime, $high, $low,$open, $close, $volume, "");
            //echo "insert:" . $stock . ": JSE - " . $datetime . ",".$close."\n";
        }

        rename($file, $folder . "loaded/JSE/".$stock.".csv");

    }
    //import csv
    //load to db
    //move file to loaded
}