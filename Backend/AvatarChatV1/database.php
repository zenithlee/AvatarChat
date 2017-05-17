<?php

class DataBase
{
	public $exists;
	
	/*
	public $db_domain = "127.0.0.1";
	public $db_database = "scorecats1";
	//public $db_name = "test";
	public $db_user = "root";
	public $db_pass = "mysql";
	*/	
	
	public $db_domain;
	public $db_database;
	//public $db_name = "";
	public $db_user;
	public $db_pass;
	
	
	public $mysqli ;
	
	
	public $cmd;
	
	public function Init( )
	{
		$this->db_domain = $_SESSION["db_domain"];
		$this->db_database = $_SESSION["db_database"];
		//$this->db_name = $_SESSION["db_name"];
		$this->db_user = $_SESSION["db_user"];
		$this->db_pass = $_SESSION["db_pass"];
	}
	
	public function DB_Connect( )
	{
		$this->Init( );		
		$this->mysqli = mysqli_connect
		( $this->db_domain, $this->db_user, $this->db_pass, $this->db_database	 );
		if (mysqli_connect_errno())
	   {
		    $err = "Failed to connect to MySQL: " . mysqli_connect_error();;
   	   		echo $err;
			return $err;
	   }
	   return "No Error";
	}
	
	public function Check( )
	{
		echo "<br><br>";
		echo "<div class='spacer'>";
		echo "Connecting to Database...<br>";
		$err = $this->DB_Connect();
		echo $err;
		echo "<br><br>";
		echo "Querying Master Table...<br>";
		$q = "SELECT * FROM dealers";
		$err = $this->Query( $q );
		
		$i = 0;
		while ( $row = mysqli_fetch_assoc( $err ) )
		{
			$i++;
		}
		echo "Found $i Results<br>";
		echo "<br><br>";
		echo "Closing Database";
		echo "</div>";
		//$this->close();
		
	}
	
	public function ShowStats( )
	{
		
	}
	
	public function close( )
	{
		mysqli_close( $this->mysqli );
	}
	
	public function debug ($m )
	{
		echo $m . "<br>";
	}
	
	public function CreateDB( )
	{
		//load the sql file
		$textfile = "touchcms.sql";
		$file = fopen( $textfile, "r" );
		$sql = fread( $file, filesize( $textfile ));
		fclose( $file );
		//do it
		$this->mysqli = mysqli_connect( $this->db_domain, $this->db_user, $this->db_pass);
		if (mysqli_connect_errno())
	   {
	   echo "Failed to connect to MySQL: " . mysqli_connect_error();
	   }
	   if (mysqli_query($this->mysqli,$sql))
		  {
		  	$this->debug("Commands executed successfully");
		  }
			else
		  {
		  	$this->debug("Error running commands: " . mysqli_error($this->mysqli));
		  }
		
	}
	
	public function CreateTable( )
	{
		$this->mysqli = mysqli_connect( $this->db_domain, $this->db_user, $this->db_pass, $this->db_database);
		if (mysqli_connect_errno())
	   {
	   echo "Failed to connect to MySQL: " . mysqli_connect_error();
	   }
		
		$sql="CREATE DATABASE IF NOT EXISTS " . $this->db_database;
		 if (mysqli_query($this->mysqli,$sql))
		  {
		  $this->debug("Database my_db created successfully<br>");
		  }
		else
		  {
		  echo "Error creating database: " . mysqli_error($this->mysqli);
		  }
		  
		  $sql = "CREATE TABLE IF NOT EXISTS `dealers` 
			 (
			 PID INT NOT NULL AUTO_INCREMENT, 
			 PRIMARY KEY(PID),
			 `group` INT(11) NOT NULL,
			 `dealerid` INT(11) NOT NULL,			 
			 name CHAR(15)
			 )";
		  
		  $this->mysqli=mysqli_connect($this->db_domain,$this->db_user,$this->db_pass,$this->db_database);
		 // Execute query
		 if (mysqli_query($this->mysqli,$sql))
		  {
		  	$this->debug("Table dealers created successfully");
		  }
			else
		  {
		  	$this->debug("Error creating table: " . mysqli_error($this->mysqli));
		  }
		  
		  //columns
		  $sql = "ALTER TABLE dealers ADD COLUMN dealerid INT(11) NOT NULL";
		  if (mysqli_query($this->mysqli,$sql))
		  { 	$this->debug("Column Added");  }	
		  else  {	$this->debug("Error adding column: " . mysqli_error($this->mysqli)); 		  }
		  
		  $sql = "ALTER TABLE dealers ADD COLUMN groupid INT(11) NOT NULL";
		  if (mysqli_query($this->mysqli,$sql))
		  { 	$this->debug("Column Added");  }	
		  else  {	$this->debug("Error adding column: " . mysqli_error($this->mysqli)); 		  }

		  
		  
	}
	
	/**
		Check if the database exists and is setup
		if not show a dialog to ask for login details
	
	*/
	public function CheckExists(  )
	{
		//open connection. if fail then there is an access problem
		$this->mysqli = mysqli_connect( $this->db_domain, $this->db_user, $this->db_pass, $this->db_database	 );
		if (mysqli_connect_errno()) 
	    { 
	   	 echo "Failed to connect to MySQL: " . mysqli_connect_error();
		 $this->ShowSetupForm( );
		 return false;
	    }
	    else
	    {	   
		   $result = $this->Query( "SELECT * from ORM" );
		   if ( $result==false )
		   {
			   echo "//Failed to connect to MySQL: " . mysqli_connect_error();
			   $this->ShowSetupForm( );
			   return false;
		   }
	    }
		return true;		
	}
	
	//show a form to setup the database
	//the setup is saved as setup.txt
	public function ShowSetupForm( )
	{
		echo "<form name='dbform' action='index.php?c=dbsetup' method='post'>";
		echo "database domain url:";
		echo "<input type='text' value='" . $this->db_domain . "'><br>";
		echo "database name:";		
		echo "<input type='text' value='" . $this->db_database . "'/><br> ";		
		echo "database user:";
		echo "<input type='text' value='" . $this->db_user. "'/><br> ";		
		echo "databse password:";
		echo "<input type='text' value='" . $this->db_pass . "'/><br> ";		
		echo "<input type='submit' value = 'SUBMIT'/>";
		echo "</form>";
	}
	
	/**
		Runs a MySQL query
	*/
	public function Query( $sql )
	{
		$this->cmd = $this->Safe($sql);
		//echo $sql . "<br>";
		if ($result = mysqli_query($this->mysqli,$sql))
		  {
		  	//$this->debug("<div class='message'>Query successful</div>");
		  }
			else
		  {
			
		  	$this->debug("<br><br>Error in query: " . mysqli_error($this->mysqli));
		  	$this->debug("<br>" . $sql );			
			//echo "Query:" . $sql ;
			return false;
		  }
		  return $result;
	}
	
	public function Safe( $s )
	{
		return mysqli_real_escape_string( $this->mysqli, $s );
	}
	
}


//$db = new DB


?>