using UnityEngine;
using System.Collections;

public class StarRotate : MonoBehaviour {
	//float spinSpeed = 4.0f;
	// Use this for initialization
	void Start () {
	//print("ObjectsRotate start");
	}
	
	// Update is called once per frame
	void Update () {
		
		//////////////////////
		transform.Rotate(0,0,-100*Time.deltaTime);
		
		//print ("roooo");
		//transform.RotateAround(Vector3.forward, spinSpeed * Time.deltaTime);
	
	}
}
