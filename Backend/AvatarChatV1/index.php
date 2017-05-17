<?php
header('Access-Control-Allow-Origin: *');
header("Access-Control-Allow-Credentials: true");
header('Access-Control-Allow-Methods: GET, PUT, POST, DELETE, OPTIONS');
header('Access-Control-Max-Age: 1000');
header('Access-Control-Allow-Headers: Origin, Content-Type, X-Auth-Token , Authorization');
header('Content-Type: application/json');

ini_set('display_errors', 'On');
error_reporting(E_ALL);


require_once 'setup.php';
require_once 'database.php';
$db = new DataBase();
$db->DB_Connect();

require_once 'tools.php';

$f = Safe("f");

if ( $f == "register") {
    $email = Safe("email");
    $pass = Safe("password");
    $screenname = Safe("screenname");
    $result = Register($email, $pass, $screenname);
}
else 
if ( $f == "gettoken") {
    $email = Safe("email");
    $pass = Safe("password");
    $screenname = Safe("screenname");
    $result = GetToken($email, $pass, $screenname);
}
else {
    $result = "0";
    }


echo $result;


?>