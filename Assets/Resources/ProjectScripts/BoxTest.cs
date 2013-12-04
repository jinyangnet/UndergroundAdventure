using UnityEngine;
using System.Collections;
using System.Collections.Generic ;

public class BoxTest : MonoBehaviour {

	public bool _isResetPosition =false ;
	//public Transform _stopper ;
	public bool _isDebug =false ;


	int _tickCount = 0 ;
	int _tickConnect = 0 ;
	int _tickCountMax = 60 ;

	public int _indexX = 1  ;
	public int _indexY = 1  ;

	float _positionX = 0.0f;
	public bool _isFlashing = false ;
	public int _flashingCount = 0 ;

	bool _isStop  = false ;
	public bool _isJewel = false ;
	public bool _isMoved = false ;
	public bool _isDead = false ;
	int _frameIndex = 1 ;
	Transform player ;

	Dictionary<Transform,int> _list ;
	RaycastHit _ray ;

	Vector3 _down ; // = transform.TransformDirection( Vector3.down ) ;

	void Start () {

		_down = transform.TransformDirection( Vector3.down ) ;
		gameObject.layer = LayerMask.NameToLayer("normal") ;// _layerNormal ;
		_positionX = transform.position.x ;
		//if ( !tag.Contains("jewel"))
		StartCoroutine( CheckBoxStatus() ) ;
		//player = GameObject.Find("player").transform ;//.FindGameObjectWithTag ( "player" ).transform ;
	}
	
	IEnumerator CheckBoxStatus(){
		   while ( true )   {

			if ( transform.parent == null &&  transform.childCount > 0 && _isJewel == false) {
				foreach (Transform child in transform )	{
					BoxTest box = child.GetComponent("BoxTest") as BoxTest ;
					if( box && box._isMoved ) {
						_isMoved = true ;
						//return ;
					}
				}
			}

			yield return  new  WaitForSeconds(1.0f);

		    GameObject _player = GameObject.Find("Player") ;
			float _distince = Vector3.Distance ( _player.transform.position , transform.position ) ;

			if (_player.transform.position.y < transform.position.y &&  _distince > 2.0f  || _isDead){
				// Destroy ( gameObject ) ;
			}

			// 检查是否有包块问题/////////////////////////////////////////////////////////

			if ( transform.position.y < _player.transform.position.y  || transform.childCount <= 4 ){
				continue ;
			}

			/*
			if ( ( transform.parent == null &&  transform.childCount >= 5 ) )
			{
				if ( _stopper )
				{
					// if (_stopper.parent !=null ) _stopper = _stopper.parent ;
					BoxTest box = _stopper.GetComponent("BoxTest") as BoxTest ;

					if ( (box._stopper && box._stopper.parent && box._stopper.parent == transform) ||
					    (box._stopper && box._stopper.parent==null && box._stopper == transform) )
					{
						_isDead = true ;
						IGManager.gm.PlayDestroyBoxEffect ( transform ,0.0f) ;
						print ( "****************************************" ) ;
					}
				}
			}
*/

			if ( _list == null  ) _list  = new Dictionary<Transform, int>();  
			_list.Clear() ;

			foreach (Transform child in transform ){
				if ( Physics.Raycast(child.position, _down, out _ray, IGState.RayDistince ) ) {
					if ( _ray.collider.transform != transform && _ray.collider.transform.parent != transform ){

						if ( !_list.ContainsKey(_ray.collider.transform ) ){
							_list.Add( _ray.collider.transform,0 ) ;
						}
					}
				}
			}

			if ( Physics.Raycast(transform.position, _down, out _ray, IGState.RayDistince ) ) {
				if ( _ray.collider.transform.parent != transform ){
					if ( !_list.ContainsKey(_ray.collider.transform ) )
						_list.Add( _ray.collider.transform,0);
				}
			}
			//if ( _isDebug )
				print ( " --------------------------------- = " + _list.Count ) ;
			// ********************************
			foreach(KeyValuePair<Transform,int> _key in _list)
			{
				Transform _transform = _key.Key ;
				DetectMiddleBlock ( _transform ) ;
			}

		 } // while end
	}

	public void DetectMiddleBlock( Transform _transform )
	{
		Vector3 __down = _transform.TransformDirection( Vector3.down ) ;
		if (_transform && Physics.Raycast(_transform.position, __down, out _ray, IGState.RayDistince ) ) {
			if ( _ray.collider.transform == transform  || _ray.collider.transform.parent == transform ){
				//if ( _isDebug )
				print ( " --------------------------------- " ) ;
				IGManager.gm.PlayDestroyBoxEffect ( _transform,0.0f) ;
				//Destroy(  _ray.collider.gameObject ) ;
			}
			else
			{
				if ( _isDebug )
					print ( " 00 +++++ " + name + " -- " +  _transform.name ) ;
			}
		}
		else
		{
			if ( _isDebug )
				print ( " 11 +++++ "  + name + " -- " +  _transform.name ) ;
		}
	}
	
	public void ResetPosition(){
		try {
		 if (_indexY > 99 ) {print( "_indexY =" + _indexY) ;return ;}
		 if ( transform == null ) return ;

		for(int j = _indexY ; j < 100 ; j++ ){
			if ( IGState.Maps[_indexX,j].rect.Contains(transform.position) )
			{
				transform.position = IGState.Maps[_indexX,j-1].position ;
				_indexY = j-1;

				if ( _isResetPosition )
						print( string.Format( " _indexX = {0} _indexY = {1} ,position ={2},Maps={3}",_indexX , _indexY,transform.position ,IGState.Maps[_indexX,_indexY].position) );
				return ;//break ;
			}
		}
		}
		catch( UnityException ex )
		{
			print( string.Format( " _indexX = {0} _indexY = {1} ,position ={2},Maps={3}",_indexX , _indexY,transform.position , ex.Message ) );
		}
	}
	
	void ResetPositionTransform( Transform _transform){
		BoxTest box = _transform.GetComponent<BoxTest>() as BoxTest; 
		if ( box ) box.ResetPosition();
	}

	void MoveDownSingle(){

		_isStop = false ;
		if (  Physics.Raycast(transform.position, _down, out _ray,  IGState.RayDistince,~IGState.LayerFlashMask )) {
			//_stopper = _ray.collider.transform ;

			ResetPosition() ;
			_tickCount = 0 ;
			//collider.enabled = true ;
			gameObject.layer = IGState.LayerNormal ;
			_isStop = true ;
			_isFlashing = false ;
        }
		else{
			gameObject.layer = IGState.LayerFlash;
			//collider.enabled = false ;
			if( _tickCount++ < _tickCountMax ){
				transform.position = new Vector3(_positionX + IGState.Flashing ,  transform.position.y,transform.position.z) ;
				_isFlashing = true ;
			}
			else{
				if ( _isFlashing && Physics.Raycast(transform.position, _down, out _ray, IGState.RayDistince ,IGState.LayerFlashMask)  ){
					transform.position = new Vector3(_positionX + IGState.Flashing ,  transform.position.y,transform.position.z) ;
				}
				else{
					_isMoved = true ;
					transform.position = new Vector3( _positionX ,  transform.position.y- IGState.StepMoveDistince ,transform.position.z);
				}
			}
		}
		_tickConnect ++ ;
		//if ( _tickConnect == 8 )
		{ 
			//print ( " _tickConnect = " + _tickConnect) ;
			_tickConnect =0 ;
			if(!_isJewel ) SigleObjects() ;
		}
	}
	
	void MoveDownMultiple(){
			_isStop = false ;
			foreach (Transform child in transform ){
				//RaycastHit _ray;	
				//Vector3 _down = child.TransformDirection( Vector3.down ) ;
				if ( Physics.Raycast(child.position, _down, out _ray, IGState.RayDistince ,~IGState.LayerFlashMask)) {
			        //print( _ray.collider.gameObject.name + " " + _ray.distance);
					if (_ray.collider.transform != transform && _ray.collider.transform.parent != transform){
							_isStop = true ;
							//_stopper = _ray.collider.transform ;
							break ;
					}
			    }
			}
		
			if( _isStop == false  ){
				if ( Physics.Raycast(transform.position, _down, out _ray, IGState.RayDistince ,~IGState.LayerFlashMask)) {
					if ( _ray.collider.transform.parent != transform ){
						//_stopper = _ray.collider.transform ;
						_isStop = true ;
					}
		        }
			}

			if ( _isStop  ){
				if ( transform.childCount >= 3 && _isMoved && !_isDead  ){
					_isDead = true ;
					IGManager.gm.PlayDestroyBoxEffect ( transform ,0.0f) ;
				}
			
				ResetPosition() ;
				_tickCount = 0   ;
				_flashingCount = 0;
			
				gameObject.layer = IGState.LayerNormal ;
				foreach (Transform child in transform ){
					child.gameObject.layer = IGState.LayerNormal ;
				}
				_isFlashing = false ;
			}
			else{
				gameObject.layer = IGState.LayerFlash ;
				//collider.enabled = false ;
				foreach (Transform child in transform ){
					//child.collider.enabled = false ;
					child.gameObject.layer = IGState.LayerFlash ;
				}
			
				if( _tickCount++ < _tickCountMax ){
				transform.position = new Vector3(_positionX + IGState.Flashing ,  transform.position.y,transform.position.z) ;
					_isFlashing = true ;
				}
				else{
					if ( _isFlashing && MoveDownMultipleFlashing() ){
					_flashingCount++; 
					if( _flashingCount > 60) {
						_isDead = true ;
						IGManager.gm.PlayDestroyBoxEffect(transform,0.0f) ;
					}

						transform.position = new Vector3( _positionX + IGState.Flashing ,  transform.position.y,transform.position.z) ;
					}
					else{
						_flashingCount = 0;
						_isMoved = true ;
						transform.position = new Vector3( _positionX ,  transform.position.y - IGState.StepMoveDistince,transform.position.z);
					}
				}
		}

		_tickConnect ++ ;
		if (  _tickConnect  ==  2 )	{
			_tickConnect = 0 ;
			ConnectionTransformParent() ;
			foreach (Transform childTransform in transform ){
				ConnectionTransform(childTransform );
			}
		}
		/// ////////////////
	}
	// 检查下面的是否在晃动
	 bool MoveDownMultipleFlashing(){
		bool	__isStop = false ;
		foreach (Transform child in transform )
		{
			if ( Physics.Raycast(child.position, _down, out _ray, IGState.RayDistince ,IGState.LayerFlashMask)) {
		        //print( _ray.collider.gameObject.name + " " + _ray.distance);
				if (_ray.collider.transform != transform && _ray.collider.transform.parent != transform){
						__isStop = true ;
					 	break ;
				}
		    }
		}
		
		if( __isStop == false  ){
			if ( Physics.Raycast(transform.position, _down, out _ray, IGState.RayDistince ,IGState.LayerFlashMask)) {
				if ( _ray.collider.transform.parent != transform ){
					__isStop = true ;
				}
	        }
		}
		
		if ( __isStop ) ResetPosition();
		return __isStop ;
	}
	
	// Update is called once per frame
	void Update (){

		if (_isResetPosition)
			ResetPosition ();
		//print ( " _tickConnect = " + _tickConnect) ;
		if( _isDead ) return ;

		if ( ( transform.parent == null &&  transform.childCount == 0 )  || _isJewel ){
			MoveDownSingle();
		}
		else if ( transform.parent == null &&  transform.childCount > 0 ){
			MoveDownMultiple();
		}
	}

	void SigleObjects(){
		for( int i =0 ;i< IGState.ListDirection.Count ; i++){  // _listDirection.Count {
			Vector3Direction _vec = IGState.ListDirection[i] ;
			if (Physics.Raycast(transform.position, _vec.VecDirection, out _ray, IGState.RayDistince )){
	            //print( _ray.collider.gameObject.name + " " + rayHit.distance);
				Transform _raytf = _ray.collider.transform ;
				if ( _raytf.name == name ){
					if (  System.Math.Abs( _raytf.position.y -transform.position.y) >= _vec.Length ){
						//print ( "  System.Math.Abs( _raytf.position.y -transform.position.y) = " +  System.Math.Abs( _raytf.position.y -transform.position.y) ) ;
						continue ;
					}
					
					if( _raytf.parent ==null  ){
						BoxTest box = _raytf.GetComponent<BoxTest>() as BoxTest; 
						box.ResetPosition();
						//ResetPositionTransform(_raytf) ;
						transform.parent = _raytf ;
						ResetPosition() ;
						//print ( "  sigleObjects _raytf.parent ==null , " + name  ) ;
					}
					
					if( _raytf.parent != null ){
						transform.parent = _raytf.parent ;
						//ResetPositionTransform(_raytf.parent) ;
						BoxTest box = _raytf.parent.GetComponent<BoxTest>() as BoxTest; 
						box.ResetPosition();
						ResetPosition() ;
						//print ( "  s_raytf.parent != null , " + name  ) ;
					}
				}
				
	        }
		}
}
	
	void ConnectionTransformParent()	{
		//float _distince = 0.10f ;
		for( int i =0 ;i<IGState.ListDirection.Count ; i++){
			Vector3Direction _vec = IGState.ListDirection[i] ;
			//childTransform.parent = transform.parent ;
			if (  Physics.Raycast( transform.position, _vec.VecDirection, out _ray, IGState.RayDistince )  ){
				Transform _raytf = _ray.collider.transform ;
				if( _raytf.name != name  ||  _raytf  == transform ||  _raytf.parent  ==  transform )	{
					continue ;
				}
	
				if (  System.Math.Abs( _raytf.position.y -transform.position.y) >= _vec.Length  ){
					//print ( "  System.Math.Abs( _raytf.position.y -transform.position.y) = " +  System.Math.Abs( _raytf.position.y -transform.position.y) ) ;
					continue ;
				}
				
				ResetPositionTransform( _raytf ) ;
				ResetPosition() ;
				
				if ( _raytf.parent == null ){
					foreach ( Transform child in _raytf)	{
						child.parent = transform ;
						ResetPositionTransform( child );
					}
					_raytf.parent = transform ;
				}
	
				
				if ( _raytf.parent != null ){
					foreach ( Transform child in _raytf.parent){
						ResetPositionTransform( child );
						child.parent = transform ;
					}
					_raytf.parent = transform ;
				}
			}
			
		}
	}
	
	void ConnectionTransform( Transform _transform ){
		for( int i =0 ;i< IGState.ListDirection.Count ; i++){
			Vector3Direction _vec = IGState.ListDirection[i] ;
			//childTransform.parent = transform.parent ;
			if (  Physics.Raycast(_transform.position, _vec.VecDirection, out _ray, IGState.RayDistince )  ){
				Transform _raytf = _ray.collider.transform ;
				if( _raytf.name != name  ||  _raytf  == transform ||  _raytf.parent ==  _transform.parent ){
					continue ;
				}
			
				if (  System.Math.Abs( _raytf.position.y -_transform.position.y) >= _vec.Length ){
					//print ( "  System.Math.Abs( _raytf.position.y -transform.position.y) = " +  System.Math.Abs( _raytf.position.y -_transform.position.y) ) ;
					continue ;
				}
				
				if ( _raytf.parent == null ){
					ResetPosition() ;
					ResetPositionTransform(  _raytf ) ;
					foreach ( Transform child in  _raytf ){
						//ResetPosition() ;
						ResetPositionTransform( child ) ;
						child.parent = transform ;
					}
					_raytf.parent = transform ;
				}
				
				if ( _raytf.parent != null ){
					ResetPosition() ;
					ResetPositionTransform( _raytf.parent ) ;
					//print( "ResetPosition__( _gameObject.transform); = "+ _raytf.name + " childCount  =" + _raytf.parent.childCount  );
					foreach ( Transform child in  _raytf.parent ){
						ResetPositionTransform( child ) ;
						child.parent = transform ;
					}
					_raytf.parent = transform ;
				}
			}
		} // FOR
	}
	
	//
	void OnDestroy(){
	}
	public  int IndexX{
	     get { return _indexX ; }
	     set { _indexX = value; }
	}
	
	public  int IndexY{
	     get { return _indexY ; }
	     set { _indexY = value; }
	}

	public  int FrameIndex{
		get { return _frameIndex ; }
		set { _frameIndex = value; }
	}

	public  bool IsJewel{
		get { return _isJewel ; }
		set { _isJewel = value; }
	}
}
	/*
	void MoveDownSingleFlashing()
	{
		// dange de
		RaycastHit rayHit;	
		Vector3 fwd = transform.TransformDirection( Vector3.down ) ;
		_isStop = false ;
		if (  Physics.Raycast(transform.position, fwd, out rayHit, 0.10f ))
        {
			ResetPosition() ;
			_tickCount = 0 ;
			//collider.enabled = true ;
			gameObject.layer = GameState.LayerNormal ; // _layerNormal ;
			_isStop = true ;
        }
		else
		{
			gameObject.layer =  GameState.LayerFlash ;  //_layerFlash;
			//collider.enabled = false ;
			if( _tickCount++ < _tickCountMax )
			{
				transform.position = new Vector3(transform.position.x + GameState.Flashing ,  transform.position.y,transform.position.z) ;
				_isFlashing = true ;
			}
			else
			{
				//collider.enabled = true ;
				_isMoved = true ;
				transform.position = new Vector3( _positionX ,  transform.position.y- _moveDistince,transform.position.z);
				_isFlashing = false ;
			}
		}
		
		SigleObjects() ;
		
	}

		if ( transform.parent != null &&  _isMoved )  //  transform.parent.childCount >= 3 
		{
			
			BoxTest _box = transform.parent.GetComponent("BoxTest") as  BoxTest ;
			_box._isMoved = true ;
			print ( "  -- " + name ) ;
			//print( " Destroy +++++++++++++++++++ " +  transform.childCount );
			//Destroy(transform.parent.gameObject,0.5f);
		}
		else
		{
			print  ( transform.name +  " -- transform.parent = " +  transform.parent  +  " _isMoved = " +   _isMoved ) ;
			//print(  transform.name +  "  ------------------------- " +  transform.childCount );
		}



	//
	bool CheckPlayer(Transform tf   ){
		bool _isPlayer = false ;
		if ( tf.name == "Player" ) {
			RaycastHit _ray;	
			Vector3 fwd = transform.TransformDirection( Vector3.up ) ;
			RaycastHit[]  rh = Physics.RaycastAll(transform.position,fwd ) ;//,  _ray,  100.0f  ) ;
			for( int i = 0 ; i <rh.Length ; i ++ ){

				IGManager.gm.PlayDestroyBoxEffect(rh[i].collider.transform ,0.0f) ; 
				//GameState.PlayDestroyBoxEffect(rh[i].collider.transform ,0.0f) ; 
			}

			IGManager.gm.PlayDestroyBoxEffect(transform,0.0f) ;
			_isPlayer = true ;
		}
		return _isPlayer ;
	}

								
	*/
	