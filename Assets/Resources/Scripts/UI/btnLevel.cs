using UnityEngine;
using System.Collections;

public class btnLevel : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnClick()
	{
		IGState.IsGameOver  =false ;
		IGState.LaunchGameLevelSence() ;
		
		
	}
	
}
