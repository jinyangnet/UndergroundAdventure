using UnityEngine;
using System.Collections;

public class btnNextLevel : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	void OnClick()
	{
		
		
		IGState.LaunchNextLevel() ;
		//Application.LoadLevel( Application.loadedLevel )  ; // .LoadLevel("LevelSence") ;//(1); //.loadedLevel();
	}
}
