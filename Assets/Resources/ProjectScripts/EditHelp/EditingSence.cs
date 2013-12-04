using UnityEngine;
using System.Collections;

public class EditingSence : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
		IGState.IsEditing = true ;
		
		
	
	}
	

	
	// Update is called once per frame
	void Update () {
	
		

	}
	
	
	void OnSelectionChange (string val)
	{
		int levelindex = System.Convert.ToInt32(val) ;
		IGState.LevelsIndex = levelindex ;
		Debug.Log( " LevelManager OnSelectionChange GameState.LevelCurrentIndex = "+ levelindex +"IsEditing="+IGState.IsEditing );
	}
}
