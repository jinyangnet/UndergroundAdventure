using UnityEngine;
using System.Collections;

public class BoxTest2 : MonoBehaviour {
	
	float movedistince ;
	bool _isStop = false ;
	// Use this for initialization
	void Start () {
		
		movedistince = 0.166667f/15.0f ;
		StartCoroutine( initPosition() ) ;
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if( _isStop )
		{
		}
		else
		{
			transform.position = new Vector3(transform.position.x,  transform.position.y- movedistince,transform.position.z);
		}
	}
	
	void getPosition()
	{
		
		/*
		Vector3 screenPoint =  Camera.main.WorldToScreenPoint(  transform.position ) ;
		int x = System.Convert.ToInt32( System.Convert.ToString( screenPoint.x /GameState._boxWidth ).Split('.')[0] ) ;
		int y = System.Convert.ToInt32( System.Convert.ToString( (GameState._screenHeigh - screenPoint.y)  / GameState._boxWidth ).Split('.')[0] ) ;
		transform.position = GameState._gameMaps[x][y].position ;
		*/
		// return ;
		int x =0;
		int y =0 ;
		
		for(int i = 0 ;i <21 ; i++ )
		{
			for(int j = 0 ; j < 100 ; j++ )
			{
				if ( IGState.Maps[i,j].rect.Contains(transform.position) )
				{
					
					//++++++++++++++++++++++++++++++++++
					//print( "++++++++++++++++++++++++++++++++++" ) ;
					//print( string.Format( "transform1 i={0},j={1} .....x={2},y={3}" ,i,j,x,y ) ) ;
					
					//print ( transform.position + "  --- reset " + GameState._gameMaps[i][j].position  ) ;
					//print ( GameState._gameMaps[i][j].rect ) ;
					//print( "---------------------------------" ) ;
					//---------------------------------
					
					transform.position = IGState.Maps[i,j-1].position ;
					
					break ;
				}
			}
		}
	}
	
	
	void OnTriggerEnter(Collider objectTrigger ) 
	// void OnCollisionEnter (Collision col ) // OnCollisionEnter
	{
		
		_isStop = true ;
		getPosition();
		
		//Rigidbody body  = transform.GetComponent ("Rigidbody") as Rigidbody ;
		//body.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY  |  RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionX |RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ ;
		
		
		//print( string.Format( "OnCollisionEnter ,x={0},y ={1} " ,x,y )  ) ;
		
		// print ( "_spritestate.IsActivation = " + _spritestate.IsActivation) ;
		// GameOver() ;
		
		return ;
		
	}
	
	
	IEnumerator initPosition()
	{
		bool running = true;
		while (running) 
		{
			
			yield return new WaitForSeconds(1.0f);
			
			//rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY  |  RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ ;
			//print( " ClickenCollision initPosition () " + transform.name + " x= " + transform.position.x  + " y= "  +  transform.position.y ) ;  
			//rigidbody.isKinematic = false ;
			//rigidbody.useGravity = true  ;
			
			_isStop =false ;
			
		}
	}
	
}
