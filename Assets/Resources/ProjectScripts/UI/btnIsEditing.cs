using UnityEngine;
using System.Collections;

public class btnIsEditing : MonoBehaviour {
	
	public GameObject _object;
	// Use this for initialization
	void Start () {
	
	}
	
	void OnActivate(bool isActive) 
	{
		//Application.LoadLevel("GameSence");
		
		return ;
		//print (isActive);
		IGState.IsEditing = isActive ;
		
		print ("btnIsEditing="+  IGState.IsEditing ) ;
		if(isActive )
		{
			_object.SendMessage("init","",SendMessageOptions.DontRequireReceiver );
		}
		else
		{
			Application.LoadLevel( Application.loadedLevel )  ;
		}
		
		//Application.LoadLevel( Application.loadedLevel )  ;
		
		
	}
}
