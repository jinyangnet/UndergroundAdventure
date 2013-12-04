/*

using UnityEngine;
using System.Collections;
using Mono.Data.Sqlite;
using System.Collections.Generic;

public class SQLManager : MonoBehaviour 
{
	
	private static Dictionary<int, GameObjectDetailedMode> _GameObjectsDataList = new Dictionary<int, GameObjectDetailedMode>();
	public static Dictionary<int, GameObjectDetailedMode> GameObjectsDataList
    {
       get { return _GameObjectsDataList; }
    }
	
	
	public static int getStarNumber(int id)
	{
		if(GameState.LevelDataList.Count==0)
		{
			InitLevelData() ;
		}
		int i = -1;
		if(GameState.LevelDataList.ContainsKey(id))
		{
			i= GameState.LevelDataList[id].starCount ;
		}
		return i ;
	}
	
	public static void UpdateLevelStarNumber( int id,int starNumber )
	{
		id = id ;
		if(GameState.LevelDataList.ContainsKey(id))
		{
		    GameState.LevelDataList[id].starCount =starNumber ;
			string sql =  "UPDATE GameLevels SET starCount = "+ starNumber +" WHERE [ID]=" + GameState.LevelsIndex;
			
			Debug.Log ( sql ) ;
			
			SQLHelper sh = new SQLHelper () ;
			sh.executeNonQuery(sql);
		}
	}
	
	static void InitLevelData()
	{
		string selectlevelsql = " select id, score,starCount,isLocked from [GameLevels] " ;
			SQLHelper sh = new SQLHelper () ;
			//sh.executeNonQuery(sql);
		
		using (SqliteDataReader reader =sh.executeReader (  selectlevelsql ) )
		{
			GameState.LevelDataList.Clear();
			while ( reader.Read() )
	    	{
				LevelMode lm=new LevelMode ();
				lm.ID = reader.GetInt32 (0);
	            lm.score = reader.GetInt32 (1);
				lm.starCount = reader.GetInt32 (2);
				lm.isLocked = reader.GetBoolean (3);
				
				GameState.LevelDataList.Add(lm.ID,lm);
				
				XMLManager.insert(lm);
				
	     		//Debug.Log( string.Format("id={0},ispass={1}, pic={2},score={3},sence={4},getstar={5}",id,ispass,pic,score,sence,getstar ) ); 
	    	}
		}
	}
	
	///\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
	

	public static void InitGameDataDetailed (int itemIndex) 
	{
		Object[] objs = GameObject.FindGameObjectsWithTag("objects") ; //  FindObjectsOfType(typeof(GameObject));
		foreach (GameObject go in objs)
		{
		   //float area = ComputCircleArea(go.collider.bounds.size.x);
		   //print("objname: "+go.name + "position=" + go.transform.position + "localPosition=" + go.transform.localPosition   ) ;//+"- size = "+go.collider.bounds.size  ) ;
		   //SQLManager.SaveLevelObjects(go,1);
		   Destroy(go);	
	    }
		
		// if( GameObjectsDataList.Count > 0 )
		//	return ;
		
		_GameObjectsDataList.Clear();
		string selectsql = "select [id],[prefabName],[transformX],[transformY],[transformZ],[rotateX],[rotateY],[rotateZ],[rotateW],[scaleX],[scaleY],[scaleZ] from [GameObjectDetailed] where [levelID] = " + itemIndex + " order by id desc " ; 
		
		SQLHelper sh = new SQLHelper () ;
		//sh.executeNonQuery(sql);
		
		using ( SqliteDataReader reader = sh.executeReader(selectsql) )
		{
			GameObjectsDataList.Clear();
			int j = 0;
			while ( reader.Read() )
	    	{
				GameObjectDetailedMode gb = new GameObjectDetailedMode() ;
	            
				gb.ID = reader.GetInt32 (0);
				string prefabName = reader.GetString (1);
				
				gb.transformX = reader.GetFloat (2);
				gb.transformY = reader.GetFloat (3);
				gb.transformZ = reader.GetFloat (4);
				
				gb.rotateX    = reader.GetFloat (5); 
				gb.rotateY    = reader.GetFloat (6); 
				gb.rotateZ    = reader.GetFloat (7); 
				gb.rotateW    = reader.GetFloat (8); 
				
				gb.scaleX    = reader.GetFloat (9); 
				gb.scaleY    = reader.GetFloat (10); 
				gb.scaleZ    = reader.GetFloat (11); 
				
				gb.prefabName = prefabName ;
				gb.levelID    = itemIndex ;
				
				GameObjectsDataList.Add(j++,gb) ;
				//Debug.Log( string.Format("id={0},texturename={1}, gamelevelid={2} , bodytype={3} ,width={4}",id,texturename, gamelevelid , bodytype ,width ) ); 
	    	}
			
		}
	}
	

	
	
	public static void DeleteGameObjectDetailedBylevelID( int levelsIndex )
	{
		string sql = " DELETE FROM  [GameObjectDetailed] where levelID = " +  levelsIndex ; //GameState.GameLevelsIndex ;
		
		print( sql ) ;
		
		SQLHelper sh = new SQLHelper () ;
		sh.executeNonQuery(sql);
		//SQLHelper.executeNonQuery(sql);
	}
	
	
	
	
	/// <summary>
	/// Saves the level datiled.
	/// </summary>
	/// CREATE TABLE "GameObjectDetailed" ("id" INTEGER PRIMARY KEY ,"prefabName" TEXT,"levelID" INT,"bodyType" INT,"transformZ" 
	/// FLOAT,"transformX" FLOAT,"transformY" FLOAT,"rotateX" FLOAT,"rotateY" FLOAT,"rotateZ" 
	/// FLOAT,"scaleX" FLOAT,"scaleY" FLOAT,"scaleZ" FLOAT,"createDate" DateTime)
	public static void SaveLevelObjects( GameObject gb,int levelID )
	{
		
		//string selectlevelsql   = " select id, score,starCount,isLocked from GameLevels " ; 
		//SqliteDataReader reader = SQLHelper.selectData(  selectlevelsql ) ; 
		//LevelDataList.Clear();
		
		Vector3 newPosion = GameState.ResetPosion(gb.transform.position ) ; 
	
		GameObjectDetailedMode gbdm = new GameObjectDetailedMode ();
		//gbdm.ID = id ;
		gbdm.levelID =  levelID ;
		gbdm.prefabName = gb.name ;
		
		gbdm.transformX = newPosion.x ; // gb.transform.localPosition.x ;
		gbdm.transformY = newPosion.y ; // gb.transform.localPosition.y ;
		gbdm.transformZ = newPosion.z ; // gb.transform.localPosition.z ;
		
		gbdm.rotateX = gb.transform.rotation.x ;
		gbdm.rotateY = gb.transform.rotation.y ;
		gbdm.rotateZ = gb.transform.rotation.z ;
		gbdm.rotateW = gb.transform.rotation.w ;
		
		gbdm.scaleX = gb.transform.localScale.x ;
		gbdm.scaleY = gb.transform.localScale.y ;
		gbdm.scaleZ = gb.transform.localScale.z ;
		
		// INSERT INTO
		string sql =string.Format( " INSERT INTO [GameObjectDetailed] ([levelID],[prefabName],[transformX],[transformY],[transformZ],[rotateX],[rotateY],[rotateZ],[rotateW],[scaleX],[scaleY],[scaleZ])"+
			"VALUES ({0},'{1}', {2},{3},{4}, {5},{6},{7}, {8},{9},{10},{11} )",
			gbdm.levelID,gbdm.prefabName,
			gbdm.transformX,gbdm.transformY,gbdm.transformZ,
			gbdm.rotateX,gbdm.rotateY,gbdm.rotateZ ,gbdm.rotateW,
			gbdm.scaleX,gbdm.scaleY,gbdm.scaleZ
			) ; 
		
		Debug.Log(sql);
		
		
		SQLHelper sh = new SQLHelper () ;
		sh.executeNonQuery(sql);
		
		//SQLHelper.executeNonQuery(sql);

		
	}
	
	
	
	

}



*/

/*
 * 
 * 
 
 	//System.Random r =new System.Random ();
		for( int i = 1; i<60 ; i++ )
		{
			//SQLHelper.insert("GAMELEVEL", new string[]{ "'"+i+"'",  "'0'"   ,"'0'"  , "'0'","0","'1'" ,"'2012-12-12'"  });
			//string sql =  "UPDATE GAMELEVEL SET starCount =3 WHERE [ID]=" + i ;
			//SQLHelper.executeNonQuery(sql);
			//CREATE TABLE "GAMELEVEL" ("ID" INTEGER PRIMARY KEY  DEFAULT (0) ,"pic" TEXT,
			//"score" INT,"sence" INT,"starCount" INT,"isLocked" BOOL,"createDate" DATETIME DEFAULT (0) )
		}
		
		
 * 
 */


		
	