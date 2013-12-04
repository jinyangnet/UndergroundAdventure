using UnityEngine;
using System.Collections;

public class StarEffect : MonoBehaviour {
	
	public GameObject effectPrefab ;
	private GameObject starParticles;
	
	private System.Random _random =new System.Random() ;
	private float _rotateSpeed; // =100 ;
	
	// Use this for initialization
	
	void Start ()
	{
		starParticles = (GameObject)Instantiate(effectPrefab);
		starParticles.transform.position =  gameObject.transform.position ;
		_rotateSpeed = _random.Next(-100,100) ;
	}
	
	// Update is called once per frame
	void Update () {
		int i = _random.Next(0,20);
		if(i>15)
		{
			transform.Rotate(0,0,_rotateSpeed*Time.deltaTime);
		}
	}
	
	void OnDestroy()
	{
		print( "StarEffect OnDestroy()" ) ;
		Destroy( starParticles );
	}
	
	
}
