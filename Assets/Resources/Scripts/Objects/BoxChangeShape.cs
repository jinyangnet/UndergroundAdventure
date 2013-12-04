using UnityEngine;
using System.Collections;

public class BoxChangeShape : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	void OnDestroy()
	{
		print( " BoxChangeShape OnDestroy() " ) ;
		return ;
	}
	
	public void MoveChildBody()
	{
		// boxObject
		
		/*
		ttf.eventReceiver  = boxObject ;
		ttf.callWhenFinished="showStarEffect";
		ttf.from = _transfromPanel_from ;
		ttf.to   = star.transform ;
		ttf.duration = 0.50f ;
		*/
		Transform[] allChildren = transform.GetComponentsInChildren<Transform>();
		foreach (Transform child in allChildren)
		{
		     //
		
		}
		
	}
	
	
	void showStarEffect()
	{
		//print( "---- showStarEffect222 ----" );
	}
	
}
