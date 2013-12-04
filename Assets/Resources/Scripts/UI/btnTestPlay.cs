using UnityEngine;
using System.Collections;

public class btnTestPlay : MonoBehaviour {
	
	public GameObject _cam ;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnClick()
	{
		// print ("play void OnClick() ") ;
		//XML _xml = _cam.AddComponent("XML") as  XML ; // <TweenTransform> ; //
		//_xml.XMLStart();
		
		//XML _xml = Camera.main.GetComponent<XML> as  XML ; // <TweenTransform> ; //
		//_xml.XMLStart();
		
		//XML _xml = Camera.main.GetComponent("XML") as  XML ; // <TweenTransform> ; //
		//if( _xml!=null)
		//	_xml.XMLStart();
		
		
		IGState.Replay();
	
		
	}
	
}
