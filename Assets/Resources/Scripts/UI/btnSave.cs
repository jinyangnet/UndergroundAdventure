using UnityEngine;
using System.Collections;

public class btnSave : MonoBehaviour {
	
	public GameObject mainCamera ;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnClick()
	{
		
	//GameState.isGameOver =false ;
	//Application.LoadLevel( Application.loadedLevel )  ; // .LoadLevel("LevelSence") ;//(1); //.loadedLevel();
	//Object.FindObjectOfType(
	//Object.FindObjectsOfType
	//GameObject.FindGameObjectsWithTag("objects");
		
	// SQLManager.DeleteGameObjectDetailedBylevelID( GameState.LevelsIndex ) ; // .delete();
	//return ;
	
	Object[] objs = GameObject.FindGameObjectsWithTag("objects") ; //  FindObjectsOfType(typeof(GameObject));
	foreach (GameObject go in objs)
	{  
	   //float area = ComputCircleArea(go.collider.bounds.size.x);
	    //print("objname: "+go.name + "position=" + go.transform.position + "localPosition=" + go.transform.localPosition   ) ;//+"- size = "+go.collider.bounds.size  ) ;
		
			//SQLManager.SaveLevelObjects( go,GameState.GameLevelsIndex );
	    
			//Destroy(go);
    }
		
	//Object[] objs = GameObject.FindGameObjectsWithTag("objects") ; //  FindObjectsOfType(typeof(GameObject));
	//foreach (GameObject go in objs)
		
	XmlHelper.ExportXML();
		
	//mainCamera.SendMessage("init","",SendMessageOptions.DontRequireReceiver );
		
	
	}
}
