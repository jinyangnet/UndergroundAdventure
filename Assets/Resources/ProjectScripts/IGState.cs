using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NarayanaGames.ScoreFlashComponent;


public class IGState : MonoBehaviour {
	
	//public static GameState _common ;
	// Use this for initialization
	private  static bool _isZoonOut ; // 
	private  static bool _isSuccess ;
	private  static bool _isFailed  ;
	private  static bool _isGameOver;
	
	private static  int  _levelsPageIndex  ;
	private static  int  _gameLevelsIndex ;
	private static  bool _isEditing ;
	
	private static int _starNumber ;
	private static GameObject _clickenSprite ;
	
	private static MoveDirection _moveDirection ;
	private static PlayingStatus _status ;
	private static Dictionary<int, IGLevelMode> _levelDataList 		= new Dictionary<int, IGLevelMode>();
	private static Dictionary<GameObject,string> _playingStyleBombList 	= new Dictionary<GameObject,string>() ;
	private static Dictionary<GameObject,string> _playingStyleTheFifthElementList 	= new Dictionary<GameObject,string>() ;

	private static Queue<Transform> _transformQueue = new Queue<Transform>();
	private static Dictionary<int, float> _ylist = new Dictionary<int, float>();
	private static Dictionary<int, float> _xlist = new Dictionary<int, float>();
	
	// public static GameObject mainCamera ;
	private static IGLevelMode _levelData ;
	// public
	
	public GameObject _Prefab ;
	private static IGMaps[,] _Maps ;// =new GameMap[16][] ; // = new int[16][12]; 
	public static int _gameScore = 1000 ;
	
	public static float _gridWidth = 64.0f ;
	public static float _screenHeigh = 768.0f ;
	static float _flashing = 0.006f ;
	// private int[][] =new int[16][12];
	
	static int  _layerFlash ;
	static int  _layerFlashMask ;
	static int  _layerNormal ;
	static IList <Vector3Direction> _listDirection =new List <Vector3Direction>() ;
	static float _rayDistince = 0.1f;
	
	static float _stepMoveDistince ;
	static float _minDistince = 0.0f ;
	static int _mapsIndex = 1;
	static Vector3[] _up_down ;
	private IGScores _score ;
	public static IGState st ;

	IEnumerator FlashingOffset()
	{
		   while ( true )
		   {
				 _flashing = -_flashing ;
		        yield return 2 ; // new  WaitForSeconds(0.020f);
		   }
	}
	
	//  IList <Vector3> _listDirection
	
	public  static IList <Vector3Direction> ListDirection
    {
       get { return _listDirection ; }
    }
	
	public static Vector3[] UP_DOWN
	{
		get { return _up_down ; }
	}

	public  static float Flashing
    {
       get { return _flashing ; }
    }
	public static float RayDistince
	{
		get { return _rayDistince ; }
	}

	public static int MapsIndex
	{
		get { return _mapsIndex ; }
		set {  _mapsIndex = value ; }
	}
	
		//_layerFlashMask    = 1 << LayerMask.NameToLayer("flash") ;
		//_layerFlash = LayerMask.NameToLayer("flash") ;
		//_layerNormal =  LayerMask.NameToLayer("normal") ;
	
	public  static int LayerFlashMask
    {
       get { return _layerFlashMask ; }
    }
	public  static int LayerFlash
    {
       get { return _layerFlash ; }
    }
	public  static int LayerNormal
    {
       get { return _layerNormal ; }
    }
	
	public static Dictionary<int, IGLevelMode> LevelDataList
    {
       get { return _levelDataList; }
    }
	
	public static IGMaps[,] Maps
    {
       get { return _Maps ; }
    }

	public  static Dictionary<int, float> Ylist
    {
       get { return _ylist; }
    }
	
	public  static Dictionary<int, float> Xlist
    {
       get { return _xlist; }
    }
	
	// PlayingStyle.thefifthelement
	// PlayingStyle.bombLocked
	
	public static Dictionary<GameObject,string> PlayingStyleTheFifthElementList
    {
       get { return _playingStyleTheFifthElementList; }
    }
	
	public static Dictionary<GameObject,string> PlayingStyleBombList
    {
       get { return _playingStyleBombList; }
    }
	
	public static Queue<Transform> TransformQueue
    {
       get { return _transformQueue; }
    }
	
	public static float StepMoveDistince
    {
       get { return _stepMoveDistince; }
    }
	
	public static float MinDistince 
    {
       get { return _minDistince; }
    }
	
	
	void Awake ()
	{
		st = this ;
		//Rect r =new Rect();
		_stepMoveDistince = 0.166667f/16.0f ;
		_minDistince   =   _stepMoveDistince * 2;
		
		Vector3 _down  = transform.TransformDirection( Vector3.down ) ;
		Vector3 _up    = transform.TransformDirection( Vector3.up ) ;
		Vector3 _left  = transform.TransformDirection( Vector3.left ) ;
		Vector3 _right = transform.TransformDirection( Vector3.right ) ;

		//Vector3 _down  = transform.TransformDirection( Vector3.down ) ;
		//Vector3 _up    = transform.TransformDirection( Vector3.up ) ;
		_up_down = new Vector3[2];
		_up_down [0] = _up;
		_up_down [1] = _up;

		Vector3Direction vdown = new Vector3Direction(_down, 0.166667f  + _minDistince );
		Vector3Direction vup = new Vector3Direction(_up, 0.166667f  + _minDistince );
		
		Vector3Direction vleft = new Vector3Direction( _left ,  _minDistince );
		Vector3Direction vright = new Vector3Direction( _right ,  _minDistince );
		
		_listDirection.Add(vdown) ;
		_listDirection.Add(vright) ;
		//_listDirection.Add(_up) ;
		_listDirection.Add(vleft) ;
		
		_isGameOver = false ;
		_isEditing  = false  ;
		_status = PlayingStatus.Normal ;
		_gridWidth =( Screen.width /1024.0f) * _gridWidth;
		
		_layerFlashMask    = 1 << LayerMask.NameToLayer("flash") ;
		_layerFlash = LayerMask.NameToLayer("flash") ;
		_layerNormal =  LayerMask.NameToLayer("normal") ;
		
		float _width = _gridWidth ;
		
		// x 21 y 100
		_Maps = new IGMaps[16,108];
		for(int i = 0 ;i <16 ; i++ )
		{
			//_Maps[i] = new ObjectsMap[100] ;
			for(int j = 0 ; j < 108 ; j++ )
			{
				_Maps[i,j] = new IGMaps() ;
				_Maps[i,j].x = i ;
				_Maps[i,j].y = j ;
				
				//_gameMaps[i][j].ObjectPoint    = new Vector2(  _width * i + _width/2 ,_width*j + _width/2 ) ;
				
				_Maps[i,j].position = Camera.main.ScreenToWorldPoint ( new Vector3( _width * i + _width/2 , _screenHeigh - (_width*j+_width/2)  ,2.0f ) ) ;
				Vector3 v3=  Camera.main.ScreenToWorldPoint ( new Vector3( _width * i  , _screenHeigh - ( _width*j )  ,2.0f ) ) ;
				_Maps[i,j].rect = new Rect(v3.x,v3.y,0.1666667f,0.1666667f);
				
				//Camera.main.WorldToScreenPoint
				//_gameMaps[i][j].
				// _gameMaps[i][j].ObjectPosition = Camera.main.ScreenToWorldPoint ( new Vector3( _width * i + _width/2 , 768 - (_width*j+_width/2)  ,2.0f ) ) ;
			
			}
		}
		
		/*
		for(int j = 0 ; j < 100 ; j++ )
		{
			Vector3 v3 = Camera.main.ScreenToWorldPoint ( new Vector3( 0 , 0 - (_width*j+_width/2)  ,2.0f ) ) ;
			Rect r =new Rect(0,0,0.166667f,0.166667f);
			Ylist.Add( j ,v3.y );
			print( v3.y ) ;
		}
		*/
		
		 // x=-1.166667,y=-0.8333333
		 // 64x=-1.166667,64y=0.1666667,0x=-1.333333,0y=0,1024x=1.333333,768y=2
		Vector3 v64 = Camera.main.ScreenToWorldPoint ( new Vector3( 64.0f , 64.0f ,2.0f ) ) ;
		Vector3 v0 = Camera.main.ScreenToWorldPoint ( new Vector3( 0f , 0f ,2.0f ) ) ;
		Vector3 v1024 = Camera.main.ScreenToWorldPoint ( new Vector3( 1024f , 768f ,2.0f ) ) ;
		
		print( string.Format( "64x={0},64y={1},0x={2},0y={3},1024x={4},768y={5}",v64.x ,v64.y ,v0.x , v0.y  ,v1024.x ,v1024.y  ) ) ;
	
		StartCoroutine(FlashingOffset());
		//	GameObject _object = (GameObject)Instantiate(_Prefab);
		//	_object.transform.position = _gameMaps[3][8].ObjectPosion ;
	}
	
	void Start () {

		Reset() ;
	}
	
//Application.dataPath :                    Application/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx/xxx.app/Data
//Application.streamingAssetsPath : Application/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx/xxx.app/Data/Raw
//Application.persistentDataPath :    Application/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx/Documents
//Application.temporaryCachePath : Application/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx/Library/Caches
			
	public static string GetDataPath()
	{
		string filepath = Application.streamingAssetsPath + string.Format( "/{0}.xml",IGState.LevelsIndex) ;
		
#if UNITY_EDITOR
		 filepath = Application.streamingAssetsPath + string.Format( "/{0}.xml",IGState.LevelsIndex) ; //"/my.xml";
#elif UNITY_IPHONE
		filepath = Application.persistentDataPath + string.Format( "/{0}.xml",IGState.LevelsIndex) ;
#endif
		return filepath ;
	}
	
	
	public static string GetLevelDataPath()
	{
		string filepath = Application.streamingAssetsPath + string.Format( "/gamelevel.xml") ;
		
#if UNITY_EDITOR
		 filepath =Application.streamingAssetsPath + string.Format( "/gamelevel.xml") ;
#elif UNITY_IPHONE
	    filepath = Application.persistentDataPath + string.Format( "/gamelevel.xml" ) ;
#endif
		return filepath ;
	}
	
	// 
	
	public static string GetStreamingAssetsPath()
	{
		string filepath = Application.streamingAssetsPath + string.Format( "/{0}.xml",IGState.LevelsIndex) ;
		return filepath ;
	}
	
	public static void Reset()
	{
		//Physics.gravity = new Vector3(0.0f,0.0f,0.0f);
		_isGameOver = false ;
		_starNumber = 0 ;
		_gameScore = 1000 ;
	}
	

	
	/// <summary>
	/// Replay this instance.
	/// </summary>
	/// 
	public static void Replay()
	{
		///////////
		GameOverManager._GOM.GameOverPanelHide() ;
		//LevelManager._LM.InitGameObjectsData() ;
		//Reset() ;
		//////////////////////
		// Application.LoadLevel( Application.loadedLevel )  ; 
		// mainCamera.SendMessage("init","",SendMessageOptions.DontRequireReceiver );
	}
	
	public static void LaunchNextLevel()
	{
		_gameLevelsIndex  ++ ;
		//GameOverManager._GOM.GameOverPanelHide() ;
		//LevelManager._LM.InitGameObjectsData() ;
		
		Reset() ;
		
		// Application.LoadLevel( Application.loadedLevel )  ; 
		// mainCamera.SendMessage("init","",SendMessageOptions.DontRequireReceiver );
	}
	
	public static MoveDirection MoveDirection
	{
	     get { return _moveDirection ; }
	     set { _moveDirection = value; }
	}
	
	public static PlayingStatus Status
	{
	     get { return _status ; }
	     set { _status = value; }
	}

	public  IGScores Score
	{
	     get { return _score ; }
		 set { _score = value; }
	}
	
	public static GameObject ClickenSprite
	{
	     get { return _clickenSprite ; }
	     set { _clickenSprite = value; }
	}
	
	public static  void LaunchGameLevelSence()
	{
		Application.LoadLevel("GameLevelSence") ;
	}
	
	public static  void LaunchGameViewSence()
	{
		Application.LoadLevel("GameSence") ;
	}
	
	/// <summary>
		/// Gets or sets the index of the current level.
		/// </summary>
		/// <value>
		/// The index of the current level.
		/// </value>
	
	public static int  LevelsPageIndex
	{
	     get { return _levelsPageIndex; }
	     set { _levelsPageIndex = value; }
	}
		
	public static int  LevelsIndex
	{
	     get { return _gameLevelsIndex; }
	     set { _gameLevelsIndex = value; }
	}
	
	public static int  StarNumber
	{
	     get { return _starNumber ; }
	     set { _starNumber = value; }
	}
	
	public static bool  IsEditing
	{
	     get { return _isEditing; }
	     set { _isEditing = value; }
	}
	
	public static bool  IsZoonOut
	{
	     get { return _isZoonOut; }
	     set { _isZoonOut = value; }
	}
	
	public static bool  IsGameOver
	{
	     get { return _isGameOver; }
	     set { _isGameOver = value; }
	}
	
	public static IGLevelMode LevelData
	{
	     get { return _levelData; }
	     set { _levelData = value; }
	}
	
	// LevelMode _thisLevel
	//
	public static float  GridWidth
	{
		get { return _gridWidth; }
		set { _gridWidth = value; }
	}
	
	public static void  setPageIndex( string item )
	{
		try
		{
			if( string.IsNullOrEmpty(item) )
			{
				LevelsPageIndex = 1 ;
			}
			else
			{
				item.Replace( "level","");
			}
			
			int itemindex = System.Convert.ToInt32(item);
			setPageIndex(itemindex);
		}
		catch(System.Exception ex  )
		{
			LevelsPageIndex = 1 ;
			Debug.Log( ex.Message );
		}
	}
	
	public static void setPageIndex(int itemindex)
	{
		if( itemindex<=10 )
		{
			LevelsPageIndex = 1 ;
		}
		else if(itemindex>10 && itemindex<=20)
		{
			LevelsPageIndex = 2 ;
		}
		else if(itemindex>20 && itemindex<=30)
		{
			LevelsPageIndex = 3 ;
		}
		else
		{
			LevelsPageIndex = 1 ;
		}
	}
	
	
	public static void InitLevel(int levelIndex)
	{
		levelIndex = levelIndex +1 ;
		// page
		setPageIndex(levelIndex);
		
		// level
		LevelsIndex = levelIndex;
		
		Debug.Log( " public static void InitLevel(int levelIndex)=" + LevelsIndex);
		Application.LoadLevel(0);
	}
	
	public static Vector3 ResetPosion( Vector3 posion)
	{
		Vector3 screenPoint =  Camera.main.WorldToScreenPoint( posion ) ; // ViewportToScreenPoint  // ScreenToViewportPoint
		float distince = 48.0f ;
		
		int x = System.Convert.ToInt32( screenPoint.x /distince )-1 ;
		int y = System.Convert.ToInt32( screenPoint.y /distince )-1 ;
		
        float __x = x*distince  + distince/2 ;
        float __y = y*distince  + distince/2 ;
		
		Vector3 newPosion = Camera.main.ScreenToWorldPoint ( new Vector3(__x,__y,2.0f ) ) ;  //ScreenToWorldPoint
		
		//print ( " x,y   = " + x + ","  + y    + "  screenPoint.x,y=" + screenPoint.x + " , " +  screenPoint.y );
		//print ( " _x,_y = " + __x +","+ __y + "  posion = " + posion + "newPosion.x,y=" + newPosion.x +","+ newPosion.y );
		
		// Vector3 newPosion = new  Vector3 (__x,__x,2.0f) ;
		return newPosion ;
	}
	
	/////////////////////////////////////
	public static void SetObjectMoveDirection_(string direction ,Transform  transform  )
	{
		
		Physics.gravity = new Vector3 ( 0f,0f,0f ) ;
		return;
		
		_moveDirection = MoveDirection.Static ;
		string player = "clicken(Clone)" ;
		GameObject _object = GameObject.Find(player) ;
		if ( _object == null )
		{
			 player = "clicken" ;
			 _object = GameObject.Find(player) ;
			if ( _object == null ) {
				return ;
			}
			else
			{
				//return ;
			}
		}
		
		Rigidbody rig = _object.GetComponent<Rigidbody>() as Rigidbody;
		if( rig == null )
		{
			return ;
		}
		float gravityXY = 9.8f/2.0f ;
		Vector3 screenPoint =  Camera.main.WorldToScreenPoint( _object.transform.position ) ;
		
		// print( " screenPoint = " + screenPoint) ;
		if( screenPoint.x < 0 || screenPoint.x > Screen.width  || screenPoint.y< 0 || screenPoint.y> Screen.height    )
		{
			return ;
		}
		
		if ( direction == "right")
		{
			if( transform !=null)
			{
				_object.transform.position = new Vector3( _object.transform.position.x , transform.position.y , _object.transform.position.z);
			}
			Physics.gravity = new Vector3 ( gravityXY,0f,0f ) ;
			rig.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY  |  RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY;
			//print(pickedObject.name);
			rig.isKinematic = false ;
			rig.useGravity = true  ;
			_moveDirection = MoveDirection.Right ;
		}
		// test the object name Lefy
		else if ( direction == "left" )
		{
			if( transform !=null)
			{
				_object.transform.position = new Vector3( _object.transform.position.x , transform.position.y , _object.transform.position.z);
			}
			
			Physics.gravity = new Vector3 ( -gravityXY,0f,0f ) ;
			rig.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY  |  RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY;
			//print(pickedObject.name);
			rig.isKinematic = false ;
			rig.useGravity = true  ;
			_moveDirection = MoveDirection.Left ;
		}
		else if ( direction == "down" )
		{
			if( transform !=null)
			{
				_object.transform.position = new Vector3(  transform.position.x ,_object.transform.position.y , _object.transform.position.z);
			}
			
			Physics.gravity = new Vector3 ( 0f,-gravityXY,0f ) ;
			rig.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY  |  RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX;
			//print(pickedObject.name);
			rig.isKinematic = false ;
			rig.useGravity = true  ;
			_moveDirection = MoveDirection.Down ;
			
		}
		else if ( direction == "up" )
		{
			if( transform !=null )
			{
				_object.transform.position = new Vector3(  transform.position.x ,_object.transform.position.y , _object.transform.position.z);
			}
			
			Physics.gravity = new Vector3 ( 0f,gravityXY,0f ) ;
			rig.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY  |  RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX;
			//print(pickedObject.name);
			rig.isKinematic = false ;
			rig.useGravity = true  ;
			
			_moveDirection = MoveDirection.Up ;
		}
		else if ( direction == "static" )
		{
			Physics.gravity = new Vector3 ( 0f,-0.98f,0f ) ;
			rig.isKinematic = true ;
			_moveDirection = MoveDirection.Static ;
			
			// print ("+++++++static++++++") ;
			// _moveDirection = MovementDirection.Static ;
		}
		
	
		
		print( " SetObjectMoveDirection_ = " +  Physics.gravity );
		// rig.useGravity = true  ;
	}
	
}
