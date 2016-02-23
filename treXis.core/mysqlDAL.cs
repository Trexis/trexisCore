/*
 * Created by SharpDevelop.
 * User: hendrik.tredoux
 * Date: 4/29/2013
 * Time: 6:16 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Data;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Tracing;

using MySql.Data.MySqlClient;

namespace treXis.core
{
	/// <summary>
	/// Description of dbDAL.
	/// </summary>
	public class DAL
	{
        static TraceSource ts = new TraceSource("dbDAL");

	    private MySqlConnection connection;
	    private string server;
	    private string database;
	    private string uid;
	    private string password;        
        
		protected internal DAL(string Server, string Database, string UserId, string Password)
		{
			ts.TraceEvent(TraceEventType.Information, 1, "dbDAL");
			
			this.server = Server;
			this.database = Database;
			this.uid = UserId;
			this.password = Password;
			string connectionString = "SERVER=" + this.server + ";" + "DATABASE=" + this.database + ";" + "UID=" + this.uid + ";" + "PASSWORD=" + this.password + ";";
			connection = new MySqlConnection(connectionString);
		}
		
	
		
		public void ExecuteQuery(string Query){
			
			ts.TraceEvent(TraceEventType.Information, 1, "ExecuteNonQuery");
			ts.TraceEvent(TraceEventType.Verbose, 2, Query);

			try{
			   //open connection
			    if (this.OpenConnection() == true)
			    {
			        //create command and assign the query and connection from the constructor
			        MySqlCommand cmd = new MySqlCommand(Query, connection);
			        
			        //Execute command
			        cmd.ExecuteNonQuery();
			
			        //close connection
			        this.CloseConnection();
			    }			
			} catch (Exception ex) {
				this.CloseConnection();
				throw new Exception("Unable to Execute Query", ex);
			}
		}

		public Dictionary<string, string> ExecuteReader(string Query){
			
			ts.TraceEvent(TraceEventType.Information, 1, "ExecuteReader");
			ts.TraceEvent(TraceEventType.Verbose, 2, Query);
		
		    //Create a list to store the result
		    Dictionary<string, string> dictionary = new Dictionary<string, string>();
		
			try{
			    //Open connection
			    if (this.OpenConnection() == true)
			    {
			        //Create Command
			        MySqlCommand cmd = new MySqlCommand(Query, connection);
			        //Create a data reader and Execute the command
			        MySqlDataReader dataReader = cmd.ExecuteReader();
			        
			        //Read the data and store them in the list
			        for(int i = 0; i < dataReader.FieldCount; i++){
			        	dictionary.Add(dataReader.GetName(i), Convert.ToString(dataReader.GetValue(i)));
			        }
			
			        //close Data Reader
			        dataReader.Close();
			
			        //close Connection
			        this.CloseConnection();
			
			        //return list to be displayed
			        return dictionary;
			    }
			    else
			    {
			        return dictionary;
			    }

		    } catch (Exception ex) {
				this.CloseConnection();
				throw new Exception("Unable to Execute Query", ex);
			}
		}
		

		public int ExecuteCount(string Query)
		{
		    int Count = -1;
		
			try{
			    //Open Connection
			    if (this.OpenConnection() == true)
			    {
			        //Create Mysql Command
			        MySqlCommand cmd = new MySqlCommand(Query, connection);
			
			        //ExecuteScalar will return one value
			        Count = int.Parse(cmd.ExecuteScalar()+"");
			        
			        //close Connection
			        this.CloseConnection();
			
			        return Count;
			    }
			    else
			    {
			        return Count;
			    }
		    } catch (Exception ex) {
				this.CloseConnection();
				throw new Exception("Unable to Execute Query", ex);
			}
		}
				
		//open connection to database
		private bool OpenConnection()
		{
			ts.TraceEvent(TraceEventType.Information, 1, "OpenConnection");

			try
		    {
		        connection.Open();
		        return true;
		    }
		    catch (MySqlException ex)
		    {
		        //When handling errors, you can your application's response based 
		        //on the error number.
		        //The two most common error numbers when connecting are as follows:
		        //0: Cannot connect to server.
		        //1045: Invalid user name and/or password.
		        switch (ex.Number)
		        {
		            case 0:
		        		ts.TraceEvent(TraceEventType.Error, 1, "Cannot connect to server.  Contact administrator");
		        		throw new Exception("Unable to open connection to database. Cannot connect to server", ex);
		
		            case 1045:
		                ts.TraceEvent(TraceEventType.Error, 1, "Invalid username/password, please try again");
		        		throw new Exception("Unable to open connection to database. Invalid username/password, please try again", ex);
		        }
		        return false;
		    }
		}
		
		//Close connection
		private bool CloseConnection()
		{
			ts.TraceEvent(TraceEventType.Information, 1, "CloseConnection");

			try
		    {
		        connection.Close();
		        return true;
		    }
		    catch (MySqlException ex)
		    {
		        ts.TraceEvent(TraceEventType.Error, 1, ex.Message);
		        return false;
		    }
		}

		
	}
}
