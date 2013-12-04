using UnityEngine;
using System.Collections;
using System.Collections.Generic ;

public class BoxTest3 : MonoBehaviour {
	
	int x ;
	int y ;
	
	int TickCount = 0 ;
	int IndexX = 1  ;
	int IndexY = 1  ;
	
	public bool _isFlashing = false ;
	public Transform newTransform ;
	IList <Vector3> _listDirection =new List <Vector3>() ;
	bool _isMoveing = false ;
	bool _isStop  = false ;
	bool _isMoved = false ;
	
	public float smoothTime= 0.3f;	//Smooth Time
	private Vector2 velocity;		//Velocity
	float MoveDistince;
	float _y = 0f ;
	
	// Use this for initialization
	
	int tick = 0 ;
	void Start () {
		
		MoveDistince = 0.166667f/20.0f ;
		int x =-1 ;
		
		//newTransform = new Transform ();
		// StartCoroutine( initPosition() ) ;
		Vector3 screenPoint =  Camera.main.WorldToScreenPoint( transform.position ) ;
		
		if ( IGState.TransformQueue.Count > 0 )
			newTransform = IGState.TransformQueue.Dequeue() ;
		
		Vector3 _down  = transform.TransformDirection( Vector3.down ) ;
		Vector3 _up    = transform.TransformDirection( Vector3.up ) ;
		Vector3 _left  = transform.TransformDirection( Vector3.left ) ;
		Vector3 _right = transform.TransformDirection( Vector3.right ) ;
		
		_listDirection.Add(_down) ;
		_listDirection.Add(_right) ;
		
		//_listDirection.Add(_up) ;
		//_listDirection.Add(_left) ;
		
		
		/////////
		
		//x = System.Convert.ToInt32( System.Convert.ToString( screenPoint.x / GameState._boxWidth ).Split('.')[0] ) ;
		//y = System.Convert.ToInt32( System.Convert.ToString( (  screenPoint.y)  / GameState._boxWidth ).Split('.')[0] ) ;
		//transform.position = GameState._gameMaps[x][y].position ;
		
		//getPosition() ;
		
		
		for(int i = 0 ;i <21 ; i++ )
		{
			for(int j = 0 ; j < 100 ; j++ )
			{
				if ( IGState.Maps[i,j].rect.Contains(transform.position) )
				{
					IndexX = i ;
					IndexY = j ;
					transform.position = IGState.Maps[i,j-1].position ;
					break ;
				}
			}
		}
		//StartCoroutine( mmm() ) ;
		//StartCoroutine( aaaa() ) ;
		// StartCoroutine( movePosition() ) ;
		
		// movePosition()
	}
	
	
	public void ResetPosition()
	{
		for(int j = IndexY ; j < 100 ; j++ )
		{
			if ( IGState.Maps[IndexX,j].rect.Contains(transform.position) )
			{
				transform.position = IGState.Maps[IndexX,j-1].position ;
				return ;
				break ;
			}
		}
		
		//print( string.Format( "error error error error error error error error x={0} name={1}",x,name ) );
	}
	
	void ResetPositionTransform( Transform _transform)
	{
		
		BoxTest box = _transform.GetComponent<BoxTest>() as BoxTest; 
		box.ResetPosition();
		
		return ;
		
		// getPosition(_transform);
		int _x=0,_y =0;
		for(int i = 3 ;i <16 ; i++ )
		{
			for(int j = 0 ; j < 100 ; j++ )
			{
				if ( IGState.Maps[i,j].rect.Contains( _transform.position ) )
				{
					_x = i ;
					_y = j ;
					break ;
				}
			}
		}
		
		_transform.position = IGState.Maps[_x,_y-1].position ;
		
	}
	

	void MoveDownSingle()
	{
		// dange de
		RaycastHit rayHit;	
		Vector3 fwd = transform.TransformDirection( Vector3.down ) ;
		_isStop = false ;
		if ( Physics.Raycast(transform.position, fwd, out rayHit, 0.10f))
        {
            // print(rayHit.collider.gameObject.name + " " + rayHit.distance);
			_isStop = true ;
        }
		//////////////////////////////////////////////////////////
		if ( _isStop )
		{
			TickCount = 0 ;
			
			_isFlashing = true ;
			
			{
				
			}
			
			ResetPosition() ;
		}
		else
		{
			
			if( _isFlashing )
			{
				if( TickCount++ ==10 )
				{
					_isFlashing = false ;
				}
			}
			else
				
			{
				//print("55555555555555555");
				//_isMoved = true ;
				transform.position = new Vector3(transform.position.x,  transform.position.y- MoveDistince,transform.position.z);
			}
		}
		
		
		sigleObjects() ;
		
	}
	
	void MoveDownMultiple()
	{

			_isStop = false ;
			bool _isChildStop = false ;
			foreach (Transform child in transform )
			{
				RaycastHit _ray;	
				Vector3 _down = child.TransformDirection( Vector3.down ) ;
				if ( Physics.Raycast(child.position, _down, out _ray, 0.10f))
			    {
			        //print( _ray.collider.gameObject.name + " " + _ray.distance);
					if (_ray.collider.transform != transform && _ray.collider.transform.parent != transform)
					{
						_isStop = true ;
						break ;
					}
			    }
			}
			
			
			if( _isStop == false )
			{
					RaycastHit _ray;	
					Vector3 _down = transform.TransformDirection( Vector3.down ) ;
					if ( Physics.Raycast(transform.position, _down, out _ray, 0.10f))
			        {
			            //print(rayHit.collider.gameObject.name + " " + rayHit.distance);
						if ( _ray.collider.transform.parent != transform )
						{
							_isStop = true ;
							//adjustPosition();
						}
			        }
			}
		
			if ( _isStop  )
			{
			
				if ( transform.childCount >=3 && _isMoved )
				{
					//Destroy(gameObject,0.5f);
				}
				ResetPosition();
			    //AdjustPosition();
			
			
			
			
			}
			else
			{
				_isMoved = true ;
				transform.position = new Vector3(transform.position.x,  transform.position.y- MoveDistince ,transform.position.z);
			}
		
		
			ConnectionTransformParent();
				//$$$$$$$$$$$$
				foreach (Transform childTransform in transform )
				{
					ConnectionTransform(childTransform );
				}
		
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if ( transform.parent == null &&  transform.childCount == 0 )
		{
			MoveDownSingle();
		}
		else if ( transform.parent == null &&  transform.childCount > 0 )
		{
			MoveDownMultiple();
		}
	}

	
	
		//Vector3 _down  = transform.TransformDirection( Vector3.down ) ;
		//Vector3 _up    = transform.TransformDirection( Vector3.up ) ;
		//Vector3 _left  = transform.TransformDirection( Vector3.left ) ;
		//Vector3 _right = transform.TransformDirection( Vector3.right ) ;

	void sigleObjects()
	{
		//string _name = transform.name ;
		float _distince = 0.10f ;
		//////////////////////////////////////////////////////////////////////////////////////////////
		RaycastHit _ray ;
		for( int i =0 ;i<_listDirection.Count ; i++)
		{
			Vector3 _vec = _listDirection[i] ;
			
			if (Physics.Raycast(transform.position, _vec, out _ray,_distince ))
	        {
	            //print( _ray.collider.gameObject.name + " " + rayHit.distance);
				Transform _raytf = _ray.collider.transform ;
				if ( _raytf.name == name )
				{
					float MaxDistince = 0.0f ;
					if ( _vec == transform.TransformDirection( Vector3.down ) ||  _vec ==  transform.TransformDirection( Vector3.up ) )
					{
						MaxDistince =  0.166667f + MoveDistince * 2;
					}
					else
					{
						MaxDistince =   MoveDistince * 2;
					}
					
					if (  System.Math.Abs( _raytf.position.y -transform.position.y) > MaxDistince )
					{
						print ( "  System.Math.Abs( _raytf.position.y -transform.position.y) = " +  System.Math.Abs( _raytf.position.y -transform.position.y) ) ;
						continue ;
					}
					
					
					
					//ResetPositionTransform(_raytf) ;
					//BoxTest bt = _raytf.parent.GetComponent( "BoxTest" ) as BoxTest ;
					//bt._isMoved = true ;
					
					if( _raytf.parent ==null  )
					{
						BoxTest box = _raytf.GetComponent<BoxTest>() as BoxTest; 
						box.ResetPosition();
						//ResetPositionTransform(_raytf) ;
						transform.parent = _raytf ;
						ResetPosition() ;
						print ( "  sigleObjects _raytf.parent ==null " ) ;
					}
					
					
					if( _raytf.parent != null )
					{
						transform.parent = _raytf.parent ;
						//ResetPositionTransform(_raytf.parent) ;
						
						BoxTest box = _raytf.parent.GetComponent<BoxTest>() as BoxTest; 
						box.ResetPosition();
						
						ResetPosition() ;
						print ( "  s_raytf.parent != null" ) ;
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
	
	
	void ConnectionTransformParent()
	{
		float _distince = 0.10f ;
		RaycastHit _ray ;
		
		for( int i =0 ;i<_listDirection.Count ; i++)
		{
			Vector3 _vec = _listDirection[i] ;
			//childTransform.parent = transform.parent ;
			if (  Physics.Raycast( transform.position, _vec, out _ray, _distince )  )
	    	{
				Transform _raytf = _ray.collider.transform ;
				
				if( _raytf.name != name  ||  _raytf  == transform ||  _raytf.parent  ==  transform )
				{
					continue ;
				}
				
				
				
				
				/*
				//////////////////////////////////////////////////////////////
			
				if (  System.Math.Abs( raytf.position.y - transform.position.y ) < _distince )
				{
					if( raytf.position.y  > transform.position.y )
					{
						raytf.position = new Vector3(raytf.position.x , transform.position.y,2.0f );
					}
					else
					{
						//transform.position = new Vector3(transform.position.x , _gameObject.transform.position.y,2.0f );
						//print( "transform.position = new Vector3(transform.position.x , _gameObject.transform.position.y,2.0f );" ) ;
						raytf.position = new Vector3(raytf.position.x , transform.position.y,2.0f );
					}
				}
				
				*/
				
				
					float MaxDistince = 0.0f ;
					if ( _vec == transform.TransformDirection( Vector3.down ) ||  _vec ==  transform.TransformDirection( Vector3.up ) )
					{
						MaxDistince =  0.166667f + MoveDistince * 2;
					}
					else
					{
						MaxDistince =   MoveDistince * 2;
					}
					
					if (  System.Math.Abs( _raytf.position.y -transform.position.y) > MaxDistince )
					{
						print ( "  System.Math.Abs( _raytf.position.y -transform.position.y) = " +  System.Math.Abs( _raytf.position.y -transform.position.y) ) ;
						continue ;
					}
				
				
				ResetPositionTransform( _raytf);
				
				//BoxTest box = raytf.GetComponent<BoxTest>() as BoxTest; 
				//box.ResetPosition();
				
				ResetPosition() ;
				
				if ( _raytf.parent == null && _raytf.childCount > 0)
				{
					foreach ( Transform child in _raytf)
					{
						//ResetPosition() ;
						
						child.parent = transform ;
						
						ResetPositionTransform( child );
					}
					_raytf.parent = transform ;
				}
				
				if ( _raytf.parent == null && _raytf.childCount == 0)
				{
					_raytf.parent = transform ;
				}
				
				if ( _raytf.parent != null )
				{
					foreach ( Transform child in _raytf.parent)
					{
						//ResetPosition() ;
						ResetPositionTransform( child );
						child.parent = transform ;
					}
					_raytf.parent = transform ;
				}
				//////////////////////////////////////////////////////////////
			}
			
		}
	}
	
	void ConnectionTransform( Transform _transform )
	{
		float _distince = 0.10f ;
		RaycastHit _ray ;
		
		for( int i =0 ;i<_listDirection.Count ; i++)
		{
			Vector3 _vec = _listDirection[i] ;
			//childTransform.parent = transform.parent ;
			if (  Physics.Raycast(_transform.position, _vec, out _ray, _distince )  )
	    	{
				
				Transform _raytf = _ray.collider.transform ;
				if( _raytf.name != name  ||  _raytf  == transform ||  _raytf.parent ==  _transform.parent )
				{
					continue ;
				}
				
					float MaxDistince = 0.0f ;
					if ( _vec == _transform.TransformDirection( Vector3.down ) ||  _vec ==  _transform.TransformDirection( Vector3.up ) )
					{
						MaxDistince =  0.166667f + MoveDistince * 2;
					}
					else
					{
						MaxDistince =   MoveDistince *2;
					}
					
					if (  System.Math.Abs( _raytf.position.y -_transform.position.y) > MaxDistince )
					{
						print ( "  System.Math.Abs( _raytf.position.y -transform.position.y) = " +  System.Math.Abs( _raytf.position.y -_transform.position.y) ) ;
						continue ;
					}
				
				if ( _raytf.parent == null && _raytf.childCount == 0 )
				{
					ResetPosition() ;
					ResetPositionTransform(  _raytf ) ;
					print("ResetPosition__( _gameObject.transform); = "+ _raytf.name  );
					_raytf.parent = transform ;
				}
				
				
				if ( _raytf.parent != null )
				{
					ResetPosition() ;
					ResetPositionTransform( _raytf.parent ) ;
					
					print( "ResetPosition__( _gameObject.transform); = "+ _raytf.name + " childCount  =" + _raytf.childCount  );
					
					foreach ( Transform child in  _raytf.parent )
					{
						//ResetPosition() ;
						ResetPositionTransform( child ) ;
						child.parent = transform ;
					}
					_raytf.parent = transform ;
				}
				
				
				/*
				if (  System.Math.Abs( raytf.position.y -_transform.position.y) < _distince )
				{
					if( raytf.position.y > _transform.position.y)
					{
						raytf.position = new Vector3( raytf.position.x , _transform.position.y,2.0f );
					}
					else
					{
						//transform.position = new Vector3(transform.position.x , _gameObject.transform.position.y,2.0f );
						//print( "transform.position = new Vector3(transform.position.x , _gameObject.transform.position.y,2.0f );" ) ;
						raytf.position = new Vector3( raytf.position.x , _transform.position.y,2.0f );
					}
				}
				
				*/
				//ResetPosition__( _gameObject.transform);
				//////////////////////////////////////////////////////////////
			}
			
		}
	}
	
	void OnDestroy()
	{
		//print( "_tail OnDestroy()" ) ;
		//Destroy( _tail );
	}
	
}
