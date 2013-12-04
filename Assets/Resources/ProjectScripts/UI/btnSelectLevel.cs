using UnityEngine;
using System.Collections;

public class btnSelectLevel : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnSelectionChange (string val)
	{
		int levelindex = System.Convert.ToInt32( val) ;
		IGState.LevelsIndex = levelindex ;
		
	}
}
