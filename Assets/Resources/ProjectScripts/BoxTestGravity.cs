using UnityEngine;
using System.Collections;
using System.Collections.Generic ;

public class BoxTestGravity : MonoBehaviour {
	
	int x ;
	int y ;
	public Transform newTransform ;
	IList <Vector3> _listDirection =new List <Vector3>() ;
	
	// Use this for initialization
	void Start () {
		
		//newTransform = new Transform ();
		// StartCoroutine( initPosition() ) ;
		Vector3 screenPoint =  Camera.main.WorldToScreenPoint( transform.position ) ;
		
		if ( IGState.TransformQueue.Count > 0 )
			newTransform = IGState.TransformQueue.Dequeue() ;
		
		/*
		 * 
		for(int i = 0 ;i <21 ; i++ )
		{
			for(int j = 0 ; j < 16 ; j++ )
			{
				Vector3 mapposition =GameState._gameMaps[i][j].ObjectPosition ;
				float distince = Util.GetDistance(screenPoint,mapposition) ;
				
				print(distince);
				
				if( distince < 48 )
				{
					x  = i ;
					y  = j ;
					break ;
				}
			}
		}
		
		*/
		
		Vector3 _down  = transform.TransformDirection( Vector3.down ) ;
		Vector3 _up    = transform.TransformDirection( Vector3.up ) ;
		Vector3 _left  = transform.TransformDirection( Vector3.left ) ;
		Vector3 _right = transform.TransformDirection( Vector3.right ) ;
		
		_listDirection.Add(_down) ;
		_listDirection.Add(_up) ;
		_listDirection.Add(_left) ;
		_listDirection.Add(_right) ;
		
		float dis = 48.0f ;
		x = System.Convert.ToInt32( System.Convert.ToString( screenPoint.x /dis ).Split('.')[0] ) ;
		y = System.Convert.ToInt32( System.Convert.ToString( screenPoint.y /dis ).Split('.')[0] ) ;
		
		IGState.Maps[x,y].prefab = gameObject ;
	    //rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY  |  RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionX |RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ ;
	
		//Debug.Log( string.Format( "_x={0},_y={1} ---  {2},{3}",_x,_y ,screenPoint.x,screenPoint.y) ) ;
		//transform.position  = GameState._gameMaps[_x][_y].ObjectPosition  ;
		////////////////////////////////////////////////////////
		//editingRigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY  | RigidbodyConstraints.FreezeRotationZ |  RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY ;
	
		//print(  string.Format( "x={0} y ={1} ",x,y ) );
		
		//StartCoroutine( initPosition() ) ;
		//StartCoroutine( movePosition() ) ;
		// movePosition()
		
		StartCoroutine( movePosition() ) ;
		
	}
	
	IEnumerator movePosition()
	{
		adjustPosition();
		bool running = true;
		//yield return new WaitForSeconds (0.5f) ;
		while (running) 
		{
			yield return new WaitForSeconds (0.1f) ;
			adjustPosition();
			//rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY  | RigidbodyConstraints.FreezeRotationZ |  RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX  ; // | RigidbodyConstraints.FreezePositionY ;
	
			
		}
	}
	
	void OnCollisionEnter__ (Collision col ) 
	{
		
		if ( col.collider.transform.position.y < transform.position.y )
		{
			
		Vector3 screenPoint =  Camera.main.WorldToScreenPoint( transform.position ) ;
		int x = System.Convert.ToInt32( System.Convert.ToString( screenPoint.x /48.0f ).Split('.')[0] ) ;
		int y = System.Convert.ToInt32( System.Convert.ToString( screenPoint.y /48.0f  ).Split('.')[0] ) ;
		transform.position = IGState.Maps[x,y].position ;
		rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY  | RigidbodyConstraints.FreezeRotationZ |  RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY ;
	
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	void OnTriggerEnter111(Collider objectTrigger ) 
	// void OnCollisionEnter (Collision col ) // OnCollisionEnter
	{
		
		/*
		// return ;
		int x =0 ,y = 0 ;
		Vector3 screenPoint =  Camera.main.WorldToScreenPoint( transform.position ) ;
		for(int i = 0 ;i <21 ; i++ )
		{
			for(int j = 0 ; j < 16 ; j++ )
			{
				Vector3 mapposition =GameState._gameMaps[i][j].ObjectPosition ;
				float distince = Util.GetDistance(screenPoint,mapposition) ;
				if( distince <48 )
				{
					x  = i ;
					y  = j ;
					break ;
				}
			}
		}
		
		//float offsetx  = 64.0f *  1.0f/Camera.main.orthographicSize ;
		//float offsety  = 64.0f *  1.0f/Camera.main.orthographicSize ;
		 //screenPoint = new Vector3(screenPoint.x + offsetx,screenPoint.y ,2.0f);
		
		 gameObject.transform.position = GameState._gameMaps[x][y].ObjectPosition ; // Camera.main.ScreenToWorldPoint(screenPoint);
		
		*/
	
		//print() ;
		//transform.position = GameState.ResetPosion( transform.position ) ;
		
		gameObject.transform.position = IGState.Maps[x,y].position ;
		
		Rigidbody body  = transform.GetComponent ("Rigidbody") as Rigidbody ;
		body.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY  |  RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionX |RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ ;
		
		//print( string.Format( "OnCollisionEnter ,x={0},y ={1} " ,x,y )  ) ;
		
		// print ( "_spritestate.IsActivation = " + _spritestate.IsActivation) ;
		// GameOver() ;
		
		return ;
		
	}


	void sigleObjects()
	{
		
		
		string _name = transform.name ;
		
		//////////////////////////////////////////////////////////////////////////////////////////////
		RaycastHit _ray ;
		for( int i =0 ;i<_listDirection.Count ; i++)
		{
			Vector3 _vec = _listDirection[i] ;
			
			if (Physics.Raycast(transform.position, _vec, out _ray, 0.20f))
	        {
	            //print( _ray.collider.gameObject.name + " " + rayHit.distance);
				if ( _ray.collider.gameObject.name == _name )
				{
					GameObject _gameObject = _ray.collider.gameObject ;
					_gameObject.transform.parent = transform ;
					
					foreach (Transform child in _gameObject.transform )
					{
						child.parent = transform ;
						
						FixedJoint joint    = child.gameObject.AddComponent("FixedJoint") as FixedJoint;
						joint.connectedBody = gameObject.rigidbody ;
						
						//joint.breakForce = 100;
						
						// FixedJoint fd =child.gameObject.AddComponent<
					    // Debug.Log(child.gameObject.name);
						
					}
				}
	        }
		}
		
		if ( transform.childCount>=2)
		{
			print( " Destroy +++++++++++++++++++ " +  transform.childCount );
			//Destroy(transform.gameObject);
		}
		else
		{
			//print(  transform.name +  "  ------------------------- " +  transform.childCount );
		}
	}
	
	
	void multiObjects()
	{
		// string name = transform.name ;
		//////////////////////////////////////////////////////////////////////////////////////////////
		RaycastHit _ray ;
		for( int i =0 ;i<_listDirection.Count ; i++)
		{
			Vector3 _vecDirection = _listDirection[i] ;
		
			//$$$$$$$$$$$$
			
			if (Physics.Raycast(transform.position, _vecDirection, out _ray, 0.20f))
		    {
				if( _ray.collider.gameObject.name != name )
				{
					continue ;
				}
				
				//&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
				// fujiedian == jiechu
				if ( transform.parent == _ray.collider.gameObject.transform ||  transform.parent == _ray.collider.gameObject.transform.parent )
				{
				}
				else
				{
					GameObject _gameObject = _ray.collider.gameObject ;
					//
					if (  _gameObject.transform.parent == null && _gameObject.transform.childCount == 0 )
					{
						_ray.collider.gameObject.transform.parent = transform.parent ;
					}
					else if ( _gameObject.transform.parent == null && _gameObject.transform.childCount > 0 )
					{
						_gameObject.transform.parent = transform.parent ;
						foreach (Transform child in _gameObject.transform )
						{
							child.parent = transform.parent ;
						}
					}
					else if ( _ray.collider.gameObject.transform.parent != null )
					{
						//GameObject 
						_gameObject = _ray.collider.gameObject.transform.parent.gameObject ;
						_gameObject.transform.parent = transform.parent ;
						foreach (Transform child in _gameObject.transform )
						{
							child.parent = transform.parent ;
						}
				
					}
				}
				//&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
				
		    }
			
			//$$$$$$$$$$$$
			
		}
		
		
	}
	
	/// <summary>
	/// Inits the position.
	/// </summary>
	/// <returns>
	/// The position.
	/// </returns>
	IEnumerator initPosition()
	{
		bool running = true;
		while (running) 
		{
			yield return new WaitForSeconds (1.0f) ;
			// dange de 
			if ( transform.parent == null &&  transform.childCount == 0 )
			{
				sigleObjects() ;
			}
			else if ( transform.parent != null &&  transform.childCount == 0 )
			{
				 multiObjects() ;
			}
			else
			{
				print( " initPosition = " + transform.name ) ;
				running =  false ;
				continue ;
			}
		}
	}
	

	
	void adjustPosition()
	{
		if ( transform.parent == null &&  transform.childCount == 0 )
		{
			sigleObjects() ;
		}
		else if ( transform.parent != null &&  transform.childCount == 0 )
		{
			 multiObjects() ;
		}
		else
		{
			print( " initPosition = " + transform.name ) ;
			//running =  false ;
			//continue ;
		}
	}
	
	
	void move()
	{
			// dange de
			if ( transform.parent == null &&  transform.childCount == 0 )
			{
				// single move
				
				RaycastHit rayHit;	
				Vector3 fwd = transform.TransformDirection( Vector3.down ) ;
				bool isDown = false ;
				if ( Physics.Raycast(transform.position, fwd, out rayHit, 0.20f))
		        {
		            // print(rayHit.collider.gameObject.name + " " + rayHit.distance);
		        }
		        else
		        {
					isDown = true ;
		            //print( transform.name +  "  nothing");//这一输出只在开始运行出现，当上面输出一次后，即使前方没有物体，也不再输出了.
		        }
					
				//////////////////////////////////////////////////////////
				
				if ( isDown )
				{
				
				if(y <0)
					return ;
					
				
				    transform.position = IGState.Maps[x,y].position ;
				   y= y-1 ;
				/*
				newTransform.position = GameState._gameMaps[x][y].ObjectPosition ;
				 	
					//newTransform.position = GameState._gameMaps[x][y].ObjectPosition ;
					//iTween.MoveTo(gameObject, GameState._gameMaps[x][y].ObjectPosition ,0);
					//y= y-1 ;
				
					
					// iTween.MoveTo(gameObject,newTransform.position,1.5f) ;
					// print(newTransform.position);
					
					TweenTransform ttf   = transform.GetComponent<TweenTransform>() as TweenTransform; //.AddComponent("TweenTransform") as  TweenTransform ; // <TweenTransform> ; //
					ttf.Reset() ;
					ttf.method = UITweener.Method.Linear;
					ttf.eventReceiver    = gameObject ;
					ttf.callWhenFinished = "moveEffect";
					ttf.from =  transform ;
					ttf.to   =  newTransform ;
					ttf.duration = 0.0f ;
					ttf.Play( true ) ;	
					
					
					*/
					
				}
			}
			else if ( transform.parent == null &&  transform.childCount > 0 )
			{
				// multi move
				
			
				print ( "	else if ( transform.parent == null &&  transform.childCount > 0 ) " ) ;
			
				
				bool isDown = true ;
				foreach (Transform child in transform )
				{
					// child.parent = transform.parent ;
					RaycastHit rayHit;	
					Vector3 fwd = child.TransformDirection( Vector3.down ) ;
					if ( Physics.Raycast(child.position, fwd, out rayHit, 0.20f))
			        {
			            //print(rayHit.collider.gameObject.name + " " + rayHit.distance);
						
						if ( rayHit.collider.gameObject.name != transform.name)
						{
							isDown = false ;
							//break ;
						}
						
						
			        }
				}
				
				{
					RaycastHit rayHit;	
					Vector3 fwd = transform.TransformDirection( Vector3.down ) ;
					if ( Physics.Raycast(transform.position, fwd, out rayHit, 0.20f))
			        {
			            //print(rayHit.collider.gameObject.name + " " + rayHit.distance);
						if ( rayHit.collider.gameObject.name != transform.name)
						{
							isDown = false ;
						}
			        }
				}
				
				if ( isDown )
				{
				
				transform.position =  IGState.Maps[x,y].position ;
				y= y-1 ;
				/*
				    newTransform.position = GameState._gameMaps[x][y].ObjectPosition ;
					//iTween.MoveTo(gameObject, GameState._gameMaps[x][y].ObjectPosition ,0);
					//y= y-1 ;
				
					
				
				
					TweenTransform ttf   = transform.GetComponent<TweenTransform>() as TweenTransform ;
					ttf.Reset();
					ttf.method = UITweener.Method.Linear;
					ttf.eventReceiver    = gameObject ;
					ttf.callWhenFinished = "moveEffect";
					ttf.from =  transform ;
					ttf.to   =  newTransform ;
					ttf.duration = 0.0f ;
					ttf.Play( true ) ;	
					
					
					*/
					
					
				}
				
			}
	}
	
	
	void moveEffect()
	{
		// print( " moveEffect " ) ;
		//GameState._gameMaps[x][y].ObjectPrefab = null  ;
		//GameState._gameMaps[x][y-1].ObjectPrefab = gameObject  ;
		y= y-1 ;
		
		// move();
	}
	
	void OnDestroy()
	{
		//print( "_tail OnDestroy()" ) ;
		//Destroy( _tail );
	}
	
	
	
	void fsfsfs2()
	{
		string name = transform.name ;
		//////////////////////////////////////////////////////////////////////////////////////////////
		RaycastHit _ray;	
		Vector3 _down  = transform.TransformDirection( Vector3.down ) ;
		Vector3 _up    = transform.TransformDirection( Vector3.up ) ;
		Vector3 _left  = transform.TransformDirection( Vector3.left ) ;
		Vector3 _right = transform.TransformDirection( Vector3.right ) ;
		
		
		
		if (Physics.Raycast(transform.position, _down, out _ray, 0.20f))
        {
			if( _ray.collider.gameObject.name == name )
			{
				// fujiedian == jiechu
				if ( transform.parent == _ray.collider.gameObject.transform ||  transform.parent == _ray.collider.gameObject.transform.parent )
				{
				}
				else
				{
					//
					if ( _ray.collider.gameObject.transform.parent == null && _ray.collider.gameObject.transform.childCount == 0 )
					{
						_ray.collider.gameObject.transform.parent = transform.parent ;
					}
					else if ( _ray.collider.gameObject.transform.parent == null && _ray.collider.gameObject.transform.childCount > 0 )
					{
						GameObject _gameObject = _ray.collider.gameObject ;
						_gameObject.transform.parent = transform.parent ;
						foreach (Transform child in _gameObject.transform )
						{
							child.parent = transform.parent ;
						}
					}
					else if ( _ray.collider.gameObject.transform.parent != null )
					{
						GameObject _gameObject = _ray.collider.gameObject.transform.parent.gameObject ;
						
						_gameObject.transform.parent = transform.parent ;
						foreach (Transform child in _gameObject.transform )
						{
							child.parent = transform.parent ;
						}
				
					}
				}
					
				
			}
        }
		
		/////////////////////////////
		
		if (Physics.Raycast(transform.position, _up, out _ray, 0.20f))
        {
			if( _ray.collider.gameObject.name == name )
			{
				// fujiedian == jiechu
				if ( transform.parent == _ray.collider.gameObject.transform ||  transform.parent == _ray.collider.gameObject.transform.parent )
				{
				}
				else
				{
					//
					if ( _ray.collider.gameObject.transform.parent == null && _ray.collider.gameObject.transform.childCount == 0 )
					{
						_ray.collider.gameObject.transform.parent = transform.parent ;
					}
					else if ( _ray.collider.gameObject.transform.parent == null && _ray.collider.gameObject.transform.childCount > 0 )
					{
						GameObject _gameObject = _ray.collider.gameObject ;
						_gameObject.transform.parent = transform.parent ;
						foreach (Transform child in _gameObject.transform )
						{
							child.parent = transform.parent ;
						}
					}
					else if ( _ray.collider.gameObject.transform.parent != null )
					{
						GameObject _gameObject = _ray.collider.gameObject.transform.parent.gameObject ;
						
						_gameObject.transform.parent = transform.parent ;
						foreach (Transform child in _gameObject.transform )
						{
							child.parent = transform.parent ;
						}
				
					}
				}
					
				
			}
        }
		
		/////////////////////////////
		
		if (Physics.Raycast(transform.position, _left, out _ray, 0.20f))
        {
			if( _ray.collider.gameObject.name == name )
			{
				// fujiedian == jiechu
				if ( transform.parent == _ray.collider.gameObject.transform ||  transform.parent == _ray.collider.gameObject.transform.parent )
				{
				}
				else
				{
					//
					if ( _ray.collider.gameObject.transform.parent == null && _ray.collider.gameObject.transform.childCount == 0 )
					{
						_ray.collider.gameObject.transform.parent = transform.parent ;
					}
					else if ( _ray.collider.gameObject.transform.parent == null && _ray.collider.gameObject.transform.childCount > 0 )
					{
						GameObject _gameObject = _ray.collider.gameObject ;
						_gameObject.transform.parent = transform.parent ;
						foreach (Transform child in _gameObject.transform )
						{
							child.parent = transform.parent ;
						}
					}
					else if ( _ray.collider.gameObject.transform.parent != null )
					{
						GameObject _gameObject = _ray.collider.gameObject.transform.parent.gameObject ;
						
						_gameObject.transform.parent = transform.parent ;
						foreach (Transform child in _gameObject.transform )
						{
							child.parent = transform.parent ;
						}
				
					}
				}
					
				
			}
        }
		
		/////////////////////
		
		if (Physics.Raycast(transform.position, _right, out _ray, 0.20f))
        {
			if( _ray.collider.gameObject.name == name )
			{
				// fujiedian == jiechu
				if ( transform.parent == _ray.collider.gameObject.transform ||  transform.parent == _ray.collider.gameObject.transform.parent )
				{
				}
				else
				{
					//
					if ( _ray.collider.gameObject.transform.parent == null && _ray.collider.gameObject.transform.childCount == 0 )
					{
						_ray.collider.gameObject.transform.parent = transform.parent ;
					}
					else if ( _ray.collider.gameObject.transform.parent == null && _ray.collider.gameObject.transform.childCount > 0 )
					{
						GameObject _gameObject = _ray.collider.gameObject ;
						_gameObject.transform.parent = transform.parent ;
						foreach (Transform child in _gameObject.transform )
						{
							child.parent = transform.parent ;
						}
					}
					else if ( _ray.collider.gameObject.transform.parent != null )
					{
						GameObject _gameObject = _ray.collider.gameObject.transform.parent.gameObject ;
						
						_gameObject.transform.parent = transform.parent ;
						foreach (Transform child in _gameObject.transform )
						{
							child.parent = transform.parent ;
						}
				
					}
				}
					
				
			}
        }
		
		/////////////////
	}
	
	
}
