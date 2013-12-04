using UnityEngine;
using System.Collections;

public class ObjectsRotate : MonoBehaviour {
	//float spinSpeed = 4.0f;
	// Use this for initialization
	public bool _isZ =false ;
	void Start () {
	//print("ObjectsRotate start");
	}
	
	// Update is called once per frame
	void Update () {
		
		//////////////////////
		if ( _isZ ) transform.Rotate(0,0,-20*Time.deltaTime); 
		else transform.Rotate(0,-20*Time.deltaTime,0);
		
		//print ("roooo");
		//transform.RotateAround(Vector3.forward, spinSpeed * Time.deltaTime);
	
	}
}