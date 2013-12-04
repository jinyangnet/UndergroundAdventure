using UnityEngine;
using System.Collections;

public class btnReplay : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnClick()
	{
		IGState.Replay() ;
		//Application.LoadLevel( Application.loadedLevel )  ; // .LoadLevel("LevelSence") ;//(1); //.loadedLevel();
	}
}
