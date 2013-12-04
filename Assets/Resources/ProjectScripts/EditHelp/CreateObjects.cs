using UnityEngine;
using System.Collections;

public class CreateObjects : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnClick () {
		
		Vector3 pos = new Vector3(0f,0f,2.0f);
		Vector3 rot = Vector3.zero;
		Vector3 sca = Vector3.zero;
		string	name =transform.name ;
		
		/*
		if( name.Contains("box1"))
		{
			string asset = "Prefab/box1ta" ;
			GameObject ob = (GameObject)Instantiate(Resources.Load(asset)) ;
			
			asset = "Prefab/box1tb" ;
		    ob = (GameObject)Instantiate(Resources.Load(asset)) ;
		}
		else
		*/
			
		{
			string asset = "Prefab/" + name ;
			print(asset ) ;
			GameObject ob = (GameObject)Instantiate(Resources.Load(asset)) ;   // ,pos,Quaternion.Euler(rot));
		}
		
		/*
		switch(name)
		{
			case "box1":
			    string asset = "Prefab/" + name ;
				GameObject ob = (GameObject)Instantiate(Resources.Load(asset),pos,Quaternion.Euler(rot));
				break ;
			
			case "box2":
				 
				break ;
		}
		*/
		
	
	}
}
