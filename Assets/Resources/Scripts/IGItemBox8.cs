using UnityEngine;
using System.Collections;

public class IGItemBox8 : MonoBehaviour {
	float _z = 4.0f;
	int i =0 ;
	// Use this for initialization
	void Start () {
		transform.localPosition = new Vector3 (0.0f, 0.0f, _z);
	}
	
	// Update is called once per frame
	void Update () {
	
		_z = _z - 0.1f;
		transform.localPosition = new Vector3 (0.0f, 0.0f, _z );

	}

	void OnTriggerEnter(Collider col)
	{
		if (col.collider.tag != "Bottom") {
			DestoryGameObject _do =new DestoryGameObject();
			_do.DestoryObject=col.collider.gameObject;
			_do.Position = col.collider.transform.position ;
			_do.RemainTime = 10 + i++;
			IGManager.gm.AddDestoryGameObject(_do) ;
						//Destroy (col.collider.gameObject);
				}
		print (  "col.OnTriggerEnter.name  = " +  col.collider.name ) ;
	}

	void OnCollisionEnter (Collision col ) 
	{
		print (  "col.collider.name  = " +  col.collider.name ) ;
	}
}
