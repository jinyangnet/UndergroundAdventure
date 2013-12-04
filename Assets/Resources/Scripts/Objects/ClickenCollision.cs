using UnityEngine;
using System.Collections;

public class ClickenCollision : MonoBehaviour {
	
	public GameObject testParticle;
	
	public GameObject ice_Effect ;
	public GameObject fire_Effect ;
	public GameObject explosion ;
	public GameObject pickupHeart ;
	public GameObject boxEffect ;
	
	private GameObject _tail ;
	private int _tick ;
	private PlayerState _playerState;
	
	
	/////////////////////////////////////////////////////
	
	public Transform target;		//The player
	public float smoothTime= 0.3f;	//Smooth Time
	private Vector2 velocity;		//Velocity
	
	/////////////////////////////////////////////////////
	
	//public GameObject panelGameover ;
	//public Transform _Transfrom_from ;
	//public Transform _Transfrom_to ;
	
	
	
	
	// Use this for initialization
	void Start () 
	{
		_playerState = PlayerState.normal;
		_tick = 0 ;
		//print( " ClickenCollision Start () " + transform.name + " x= " + transform.position.x  + " y= "  +  transform.position.y ) ;   
		StartCoroutine( SetPlayerState( PlayerState.ice ) ) ;
		
		//GameState.Reset();
		IGState.SetObjectMoveDirection_( "static",null ) ;
		// _tail =  (GameObject)Instantiate(ice_Effect);
		// testParticle
		// GameObject particles = (GameObject)Instantiate(testParticle);
		// particles.transform.position = new Vector3(0f,0f,1f) ;
	}
	
	

	
	
	void  Update0 ()
	{
		
	
	}
	
	IEnumerator SetPlayerState( PlayerState playerState )
	{
		
		if( _tail)
		{
			Destroy( _tail );
		}
		
		if ( playerState == PlayerState.fire)
		{
			_tail =  (GameObject)Instantiate(fire_Effect);
			_playerState = PlayerState.fire ;
		}
		else if ( playerState == PlayerState.ice )
		{
			_tail =  (GameObject)Instantiate( ice_Effect ) ;
			_playerState = PlayerState.ice ;
		}
		else
		{
			_playerState = PlayerState.normal ;
		}
		
		yield return new WaitForSeconds(6f);
		if( _tail)
		{
			_playerState = PlayerState.normal ;
			Destroy( _tail );
			print("WaitForSeconds(6f) Destroy( _tail );");
		}
	}
	
	IEnumerator initPosition()
	{
		yield return new WaitForSeconds(0.1f);
		print( " ClickenCollision initPosition () " + transform.name + " x= " + transform.position.x  + " y= "  +  transform.position.y ) ;  
		rigidbody.isKinematic = false ;
		rigidbody.useGravity = true  ;
	}
	
	// Update is called once per frame
	void Update () {
		
		/*
			//Set the position
		Camera.main.transform.position =
			new Vector3(Mathf.SmoothDamp(Camera.main.transform.position.x, transform.position.x, ref velocity.x, smoothTime),
				Mathf.SmoothDamp( Camera.main.transform.position.y, transform.position.y, 
				ref velocity.y, smoothTime),Camera.main.transform.position.z);
				
				*/
		
		Camera.main.transform.position = new Vector3(Camera.main.transform.position.x,transform.position.y,Camera.main.transform.position.z);
		/*
		animSprite.Play("demo_once");
		animSprite.animationCompleteDelegate = delegate(tk2dAnimatedSprite sprite, int clipId) 
		{ 
			animSprite.Play("demo_pingpong"); 
			animSprite.animationCompleteDelegate =null ; //new tk2dAnimatedSprite.AnimationCompleteDelegate();// null; 
		} ;
		*/
		
		_tick ++ ;
		if( _tail ) // && _tick ==10 )
		{
			_tick = 0 ;
			_tail.transform.position = gameObject.transform.position ;
		}
		
		if ( _tick == 10 && !IGState.IsGameOver )
		{
			_tick = 0 ;
			Vector3 screenPoint =  Camera.main.WorldToScreenPoint( transform.position ) ;
			
			if( screenPoint.x > Screen.width || screenPoint.x < 0 || screenPoint.y > Screen.height || screenPoint.y <0)
			{
				//print(" GameOver Update ... ");
				//GameOver() ;
			}
		}
		//print( " ClickenCollision Update " + transform.name + " x= " + transform.position.x  + " y= "  +  transform.position.y ) ;   
	}
	
	/// <summary>
	/// Raises the trigger enter event.
	/// </summary>
	/// <param name='objectTrigger'>
	/// Object trigger.
	/// </param>
	/// 
	void OnTriggerEnter(Collider objectTrigger ) 
	{
		if ( objectTrigger.transform.name.Contains( "littlestar" ) )
		{
			Destroy( objectTrigger.gameObject );
			
			//StopCoroutine("SetPlayerState");
			StopAllCoroutines();
			StartCoroutine( SetPlayerState( PlayerState.fire ) ) ;
		}
		else if( objectTrigger.transform.name.Contains( "box2" ) )
		{
			if (_playerState == PlayerState.ice)
			{
				Destroy(objectTrigger.gameObject);
			}
			else
			{
				//objectTrigger.
				SetStatic( objectTrigger.transform ) ;
				//GameState.SetObjectMoveDirection_( "static" ,objectTrigger.transform ) ;
				return ;
			}
		}
		
		else if( objectTrigger.transform.name.Contains( "box4" ) )
		{
			//.AddComponent("TweenTransform") as  TweenTransform ; // <TweenTransform> ; //
			//BoxChangeShape bcs = objectTrigger.transform.gameObject.GetComponent<BoxChangeShape>() as BoxChangeShape; 
			//bcs.MoveChildBody();
			
			StartCoroutine ( Box4ChangeShape( objectTrigger.gameObject )) ;
			
			///
			// Destroy(objectTrigger.gameObject);
			//objectTrigger.
			//SetStatic( objectTrigger.transform ) ;
			//return ;
		}
		else if( objectTrigger.transform.name.Contains( "boxl" ) )
		{
			//objectTrigger.
			SetStatic( objectTrigger.transform ) ;
			//GameState.SetObjectMoveDirection_( "static" ,objectTrigger.transform ) ;
			return ;
		}
		else if( objectTrigger.transform.name.Contains( "boxr" ) )
		{
			//objectTrigger.
			SetStatic( objectTrigger.transform ) ;
			//GameState.SetObjectMoveDirection_( "static" ,objectTrigger.transform ) ;
			return ;
		}
		
	    print( objectTrigger.gameObject.name );
		IGState.StarNumber ++ ;
	}
	
	IEnumerator Box4ChangeShape( GameObject boxObject )
	{
		yield return new WaitForSeconds(0.3f);
		BoxChangeShape bcs = boxObject.GetComponent<BoxChangeShape>() as BoxChangeShape; //.AddComponent("TweenTransform") as  TweenTransform ; // <TweenTransform> ; //
		bcs.MoveChildBody();
	
		// boxObject
		//print( " ClickenCollision initPosition () " + transform.name + " x= " + transform.position.x  + " y= "  +  transform.position.y ) ;  
		//rigidbody.isKinematic = false ;
		//rigidbody.useGravity = true  ;
	}
	
	// StartCoroutine( SetPlayerState( PlayerState.ice ) ) ;
	// BoxChangeShape
	
	
	void OnTriggerStay(Collider objectTrigger ) 
	{
		if( objectTrigger.transform.name.Contains( "box1ta" ) )
		{
			GameObject _object = GameObject.Find("box1tb(Clone)");
			if( _object )
			{
				//transform.position = _object.transform.position ;
				SetNewPosition ( _object.transform ) ;
				print(" box1tb(Clone) ");
			}
		}
		else if( objectTrigger.transform.name.Contains( "box1tb" ) )
		{
			GameObject _object = GameObject.Find("box1ta(Clone)");
			if( _object )
			{
				SetNewPosition ( _object.transform ) ;
				//transform.position = _object.transform.position ;
				//print(" box1ta(Clone) ");
			}
		}
	}
	
	
	//OnTriggerExit
	void OnTriggerExit(Collider objectTrigger ) 
	{
		if( objectTrigger.transform.name.Contains( "box2" ) && ( _playerState ==PlayerState.ice ) )
		{
			GameObject _explosion = (GameObject)Instantiate(boxEffect);
			_explosion.transform.position = objectTrigger.transform.position ;
			
			/*
			string asset = "Prefab/box5";
			if(Resources.Load(asset)==null)
			{
				print(Resources.Load(asset));
			}
			GameObject ob = (GameObject)Instantiate(Resources.Load(asset),objectTrigger.transform.position,new Quaternion(0f,0f,0f,0f) );
			print(ob.tag);
			*/
			
			Destroy( objectTrigger.gameObject );
		}
	    print( objectTrigger.gameObject.name );
		IGState.StarNumber ++ ;
		print("	void OnTriggerEnter(Collider objectTrigger ) ");
	}
	
	IEnumerator createBox(Vector3  position )
	{
		yield return new WaitForSeconds(.3f);
		string asset = "Prefab/box";
		if(Resources.Load(asset)==null)
		{
			print(Resources.Load(asset));
		}
		GameObject ob = (GameObject)Instantiate(Resources.Load(asset),position,new Quaternion(0f,0f,0f,0f) );
	}
	
	void OnCollisionEnter (Collision col ) // OnCollisionEnter
	{
		// game is over
		if( col.transform.name.Contains( "little" ) )
		{
			gameObject.transform.localRotation = new Quaternion(0f,0f,0f,0f);
			SetStatic( col.transform ) ;
			
			LittleState _spritestate = col.transform.GetComponent<LittleState>() ;
			if( _spritestate.IsActivation )
			{
				GameObject particle  = (GameObject)Instantiate(pickupHeart);
				particle.transform.position = transform.localPosition ;
				GameOverCleanObjects () ;
			}
			else
			{
				
			}
			//print ( "_spritestate.IsActivation = " + _spritestate.IsActivation) ;
			
			// GameOver() ;
			return ;
		}
		// 1 cao 4 iron 5 woodbox
		else if( col.transform.name.Contains( "box1" )  || col.transform.name.Contains( "box4" ) || col.transform.name.Contains( "box5" )  )
		{
			SetStatic( col.transform ) ;
			return ;
		}
		else if( col.transform.name.Contains( "box6" ) )
		{
			if (_playerState ==PlayerState.fire)
			{
				Destroy(col.gameObject);
			}
			else
			{
				SetStatic( col.transform ) ;
				return ;
			}
		}
		else if( col.transform.name.Contains( "box8" ) )
		{
			//.AddComponent("TweenTransform") as  TweenTransform ; // <TweenTransform> ; //
			BoxMagic bcs = col.gameObject.GetComponent<BoxMagic>() as BoxMagic; 
			bcs.SetEffect() ;
			CheckState();
	
			SetStatic( col.transform ) ;
			//return ;
		}
		
		else if( col.transform.name.Contains( "ice" ) )
		{
			//boxEffect
			GameObject _explosion = (GameObject)Instantiate(boxEffect);
			_explosion.transform.position = col.transform.position ;
			
			//Transform colTransform = new Transform( col.transform) ;
			Vector3 position = new Vector3(col.transform.position.x,col.transform.position.y,col.transform.position.z);
			StartCoroutine( createBox(position) ) ;
			
	    	Destroy(col.gameObject) ;
			//SetStatic( col ) ;
			return ;
		}
		else if( col.transform.name.Contains( "triangle_topleft" ) )
		{
			if(IGState.MoveDirection == MoveDirection.Up )
			{
				IGState.SetObjectMoveDirection_( "right" ,col.transform ) ;
			}
			else if (IGState.MoveDirection == MoveDirection.Left )
			{
				IGState.SetObjectMoveDirection_( "down" , col.transform ) ;
			}
			else
			{
				SetStatic( col.transform ) ;
			}
			// print (  GameState.MoveDirection );
			return ;
		}
		else if( col.transform.name.Contains( "triangle_topright" ) )
		{
			if(IGState.MoveDirection == MoveDirection.Up )
			{
				IGState.SetObjectMoveDirection_( "left" ,col.transform ) ;
			}
			else if ( IGState.MoveDirection == MoveDirection.Right )
			{
				IGState.SetObjectMoveDirection_( "down" ,col.transform ) ;
				// GameState.SetObjectMoveDirection_( "left" ) ;
				// print (  " triangle_topright - > down "  );
			}
			else
			{
				SetStatic( col.transform ) ;
			}
			return ;
		}
		else if( col.transform.name.Contains( "triangle_bottomright" ) )
		{
			if(IGState.MoveDirection == MoveDirection.Down )
			{
				IGState.SetObjectMoveDirection_( "left" ,col.transform ) ;
			}
			else if ( IGState.MoveDirection == MoveDirection.Right )
			{
				IGState.SetObjectMoveDirection_( "up" ,col.transform ) ;
			}
			else
			{
				SetStatic( col.transform ) ;
			}
			return ;
		}
		else if( col.transform.name.Contains( "triangle_bottomleft" ) )
		{
			if(IGState.MoveDirection == MoveDirection.Down )
			{
				IGState.SetObjectMoveDirection_( "right" ,col.transform ) ;
			}
			else if ( IGState.MoveDirection == MoveDirection.Left )
			{
				IGState.SetObjectMoveDirection_( "up" ,col.transform ) ;
			}
			else
			{
				SetStatic( col.transform ) ;
			}
			return ;
		}// TNT 
		else if ( col.transform.name.Contains( "box3" ) )
		{
			
			if ( IGState.PlayingStyleBombList.ContainsKey(col.gameObject) )
			{
				IGState.PlayingStyleBombList.Remove( col.gameObject ) ;
			}
			CheckState() ;
			
			print("COL TNT ..... ");
			tk2dAnimatedSprite _sprite = col.transform.GetComponent<tk2dAnimatedSprite>() ;
			if( _sprite )
			{
				if( _sprite.Playing )
				{
					SetStatic( col.transform ) ;
					return ;
				}
				
				_sprite.Play() ;
				_sprite.animationCompleteDelegate = delegate(tk2dAnimatedSprite sprite, int clipId) 
				{ 
					_sprite.animationCompleteDelegate = null ; //new tk2dAnimatedSprite.AnimationCompleteDelegate();// null; 
					GameObject _explosion = (GameObject)Instantiate(explosion);
					_explosion.transform.position = col.transform.position ;
					
					float distince =IGUtil.GetDistance( col.transform.position , gameObject.transform.position ) ;
					if( distince < 64.0f )
					{
						//GameOver() ;
					}
					// col.transform.position
					Destroy(col.gameObject) ;
			
					
				} ;
			}
			
			//GameState.SetObjectMoveDirection_( "static", col.transform) ;
			
			{
				SetStatic( col.transform ) ;
			}
			return ;
		}
		else if ( col.transform.name.Contains( "star" ) )
		{
			IGState.StarNumber ++ ;
			Destroy( col.gameObject );
			
			//GameObject particles = (GameObject)Instantiate(bloodParticle);
			//particles.transform.position = transform.position ;//new Vector3(transform.position.x ,transform.position.y ,1.0f);
			return ;
		}
		else
		{
			//GameState.SetObjectMoveDirection_( "static" ) ;
			return ;
		}
    }
	
	private void CheckState()
	{
		if( IGState.Status == PlayingStatus.bombLocked )
		{
			if( IGState.PlayingStyleBombList.Count ==0)
			{
					GameObject _object = GameObject.Find("little(Clone)");
					{
						if( _object )
						{
							tk2dAnimatedSprite _sprite = _object.transform.GetComponent<tk2dAnimatedSprite>() ;
							if( _sprite )
							{
								_sprite.Stop() ;
								_sprite.Play( "little" ) ;
								print ( "_sprite.Play( little );" ) ;
								// _sprite.p
							}
							
							LittleState _spritestate = _object.transform.GetComponent<LittleState>() ;
							if( _spritestate )
							{
								_spritestate.IsActivation = true ;
							}
						
						}
					}
			}
		}
		else if ( IGState.Status == PlayingStatus.bomb )
		{
			// game over
			if( IGState.PlayingStyleBombList.Count ==0)
			{
				GameOverCleanObjects () ;
			}
		}
		else if ( IGState.Status == PlayingStatus.thefifthelement )
		{
			// game over
			if( IGState.PlayingStyleTheFifthElementList.Count ==0)
			{
				GameOverCleanObjects () ;
			}
		}
		
		else if ( IGState.Status == PlayingStatus.bombLockedthefifthelement )
		{
			if( IGState.PlayingStyleBombList.Count !=0 || IGState.PlayingStyleTheFifthElementList.Count !=0)
			{
				return ;
			}
			
			GameObject _object = GameObject.Find("little(Clone)");
			if( _object == null )
			{
			}
			
			tk2dAnimatedSprite _sprite = _object.transform.GetComponent<tk2dAnimatedSprite>() ;
			if( _sprite )
			{
				_sprite.Stop() ;
				_sprite.Play( "little" ) ;
				print ( "_sprite.Play( little );" ) ;
				// _sprite.p
			}
			
			LittleState _spritestate = _object.transform.GetComponent<LittleState>() ;
			if( _spritestate )
			{
				_spritestate.IsActivation = true ;
			}
			
			
		}
		
		
		print ("   	private void CheckState() PlayingStyleBombList.Count =" +  IGState.PlayingStyleBombList.Count ) ;
		
		
	}
	
	
	private void SetStatic( Transform _transform )
	{
		Vector3 screenPoint =  Camera.main.WorldToScreenPoint( _transform.position ) ;
		if( IGState.MoveDirection == MoveDirection.Left )
		{
			float offsetx  = 64.0f *  1.0f/Camera.main.orthographicSize ;
			screenPoint = new Vector3(screenPoint.x + offsetx,screenPoint.y,2.0f);
			gameObject.transform.position = Camera.main.ScreenToWorldPoint(screenPoint);
		}
		else if( IGState.MoveDirection == MoveDirection.Right )
		{
			float offsetx  = 64.0f *  1.0f/Camera.main.orthographicSize ;
			screenPoint = new Vector3(screenPoint.x - offsetx,screenPoint.y,2.0f);
			gameObject.transform.position = Camera.main.ScreenToWorldPoint(screenPoint);
		}
		else if( IGState.MoveDirection == MoveDirection.Up )
		{
			float offsety  = 64.0f *  1.0f/Camera.main.orthographicSize ;
			screenPoint = new Vector3(screenPoint.x ,screenPoint.y - offsety,2.0f);
			//print(" -- up = " + screenPoint);
			gameObject.transform.position = Camera.main.ScreenToWorldPoint(screenPoint);
		}
		else if( IGState.MoveDirection == MoveDirection.Down )
		{
			float offsety  = 64.0f *  1.0f/Camera.main.orthographicSize ;
			screenPoint = new Vector3(screenPoint.x ,screenPoint.y + offsety,2.0f);
			//print("down = " + screenPoint);
			gameObject.transform.position = Camera.main.ScreenToWorldPoint(screenPoint);
		}
		//SetNewPosition ( _transform ) ;
		IGState.SetObjectMoveDirection_( "static" ,_transform ) ;
	}
	
	private void SetNewPosition( Transform _transform )
	{
		Vector3 screenPoint =  Camera.main.WorldToScreenPoint( _transform.position ) ;
		
		if( IGState.MoveDirection == MoveDirection.Left )
		{
			float offsetx  = 64.0f *  1.0f/Camera.main.orthographicSize ;
			screenPoint = new Vector3(screenPoint.x - offsetx,screenPoint.y,2.0f);
			gameObject.transform.position = Camera.main.ScreenToWorldPoint(screenPoint);
		}
		else if( IGState.MoveDirection == MoveDirection.Right )
		{
			float offsetx  = 64.0f *  1.0f/Camera.main.orthographicSize ;
			screenPoint = new Vector3(screenPoint.x + offsetx,screenPoint.y,2.0f);
			gameObject.transform.position = Camera.main.ScreenToWorldPoint(screenPoint);
		}
		else if( IGState.MoveDirection == MoveDirection.Up )
		{
			float offsety  = 64.0f *  1.0f/Camera.main.orthographicSize ;
			screenPoint = new Vector3(screenPoint.x ,screenPoint.y + offsety,2.0f);
			print(" -- up = " + screenPoint);
			gameObject.transform.position = Camera.main.ScreenToWorldPoint(screenPoint);
		}
		else if( IGState.MoveDirection == MoveDirection.Down )
		{
			float offsety  = 64.0f *  1.0f/Camera.main.orthographicSize ;
			screenPoint = new Vector3(screenPoint.x ,screenPoint.y - offsety,2.0f);
			print("down = " + screenPoint);
			gameObject.transform.position = Camera.main.ScreenToWorldPoint(screenPoint);
		}
	}
	
	
	private void GameOverCleanObjects()
	{
		if( _tail )
		{
			_tail.transform.position = new Vector3(-100f,-100f,-100f);
			Destroy( _tail.gameObject );
		}
		
		ClickenPaths paths =gameObject.transform.GetComponent<ClickenPaths>() as ClickenPaths ;
		if( paths )
		{
			paths.CleanPaths() ;
		}
		
		// game over
		
		IGManager.GameOver();
		
	}
	
	/*
	void OnMouseDown()
	{
		RaycastHit hit = new RaycastHit();
		if(this.collider.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 9999f))
		{
			//GameObject particle = spawnParticle();
			//particle.transform.position = hit.point + particle.transform.position;
		}
	}
	*/
	
	private GameObject spawnParticle()
	{
		GameObject particles = (GameObject)Instantiate( testParticle ) ;
		//float Y = 0.0f;
		particles.transform.position = new Vector3(0,0,0);
		return particles;
	}
	
	void OnDestroy()
	{
		print( "_tail OnDestroy()" ) ;
		Destroy( _tail );
	}
	
}
