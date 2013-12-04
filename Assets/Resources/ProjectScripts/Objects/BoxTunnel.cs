using UnityEngine;
using System.Collections;

public class BoxTunnel : MonoBehaviour {
	
	public GameObject box_Effect ;
	private GameObject _box_Effect ;
	// Use this for initialization
	void Start () {
		_box_Effect = (GameObject)Instantiate(box_Effect);
		_box_Effect.transform.position = new Vector3(-100f,-1000f,1f) ;
	
	}
	
	// Update is called once per frame
	void Update () {
		
		_box_Effect.transform.position = transform.position ;
	}
	
	void OnDestroy()
	{
		//print( "StarEffect OnDestroy()" ) ;
		Destroy( _box_Effect );
	}
}
