/*

using UnityEngine;
using System;
using System.Collections;
using Mono.Data.Sqlite;

using System.Data;
//using Mono.Data.Sqlite;

public class SQLHelper
{
    //private    _sqliteDataReader;
	
	string _filename ="data source=" + Application.streamingAssetsPath + "/data5.sqlite3";
	
	   public SQLHelper()
        {
        }


    public SqliteDataReader executeReader (string sqlQuery)
    {
		 SqliteDataReader _sqliteDataReader ;
		try
   		 {
			SqliteConnection _sqliteConnection = new SqliteConnection (_filename);
       	 	_sqliteConnection.Open ();
			
			using( SqliteCommand _sqliteCommand = _sqliteConnection.CreateCommand () )
			{
		        _sqliteCommand.CommandText = sqlQuery;
			    _sqliteDataReader = _sqliteCommand.ExecuteReader ();
		        return _sqliteDataReader ;
			}
		 }
    	catch(Exception e)
    	{
       		string ex = e.ToString();
       		Debug.Log(ex);
    	}
		
		return null ;
    }
	
	public  int executeNonQuery (string sqlQuery)
    {
		try
   		 {
			SqliteConnection _sqliteConnection = new SqliteConnection (_filename);
       	 	_sqliteConnection.Open ();
			using( SqliteCommand _sqliteCommand = _sqliteConnection.CreateCommand () )
			{
		        _sqliteCommand.CommandText = sqlQuery;
			    int  i = _sqliteCommand.ExecuteNonQuery ();
	        	return i ;
			}
		 }
    	catch(Exception e)
    	{
       		string ex = e.ToString();
       		Debug.Log(ex);
    	}
		
		
		return 0 ;
		
    }
}


*/