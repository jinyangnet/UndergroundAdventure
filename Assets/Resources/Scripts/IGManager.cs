using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DestoryGameObject 
{
	private int _time ;
	private GameObject _gameObject ;
	private Vector3 _position ;
	public int RemainTime
	{
		get { return _time; }
		set { _time = value; }
	}

	public GameObject DestoryObject
	{
		get { return _gameObject; }
		set { _gameObject = value; }
	}

	public Vector3 Position 
	{
		get { return  _position ; }
		set { _position = value; }
	}
}

public partial class IGManager  : MonoBehaviour 
{
	string[] _boxs = {"box1","box2","box3","box4","box5","box6",
		"box1jewel","box2jewel","box3jewel","box4jewel","box5jewel","box6jewel","box7jewel" ,"box8jewel" , "box9jewel" ,"box10jewel" } ;
	private System.Random _random =new System.Random() ;

	private  Dictionary<string,ItemsType> _jewelItemsList 	= new Dictionary<string,ItemsType>() ;

	private Queue<DestoryGameObject> _destoryObject = new Queue<DestoryGameObject> ();
	public static IGManager gm;
	string asset = "Prefab/CFXM3_Hit_Ice_B_Air" ;
	public GameObject _player ;
	void Awake()
	{
		Application.targetFrameRate = 30;
		gm = this ;

		IGState.st.Score = new IGScores();
		
		IGState.st.Score.HP  = 200;
		IGState.st.Score.OriginalHP  = 200 ;
		IGState.st.Score.GoldCoin  = 0 ;
		//gameObject.collider
		if (_player == null )
			_player = GameObject.FindWithTag ("Player") ;
	}

	// Use this for initialization
	void Start () {

		//StartCoroutine ( DestoryGameObject () ) ;
	}
	
	// Update is called once per frame
	void Update () {
		DestoryGameObject ();
	
	}

	public void Reset()
	{
		_player.transform.position = new Vector3 (-0.08333334f,2.2f,2.0f);
		Camera.main.transform.position = new Vector3 (-0.08333334f,2.2f,0.0f);
		InitSence ();
	}

	public void AddDestoryGameObject( DestoryGameObject items )
	{
		if (items.DestoryObject == null) 
			print ("00000000000000000000");

		_destoryObject.Enqueue (items);
	}
	
	void DestoryGameObject()
	{
		if ( _destoryObject.Count > 0 )
		{
			DestoryGameObject _do = _destoryObject.Dequeue() ;

			if ( _do.DestoryObject == null ) return ;
			_do.RemainTime = _do.RemainTime - 1 ;


			if ( _do.DestoryObject.transform.childCount > 0)
			{
				_destoryObject.Enqueue( _do ) ;
				return ;
			}

			// print ( " DestoryObject = " + _do.DestoryObject );
			// print  ( " _do.RemainTime = " + _do.RemainTime ) ;
			// Vector3 pos = _do.DestoryObject.transform.position ;

			if ( _do.RemainTime <= 0 )
			{
			   	Destroy(	Instantiate( Resources.Load(asset),_do.Position ,Quaternion.identity ) ,2.0f) ;
				// _do.DestoryObject &&
				//_do.DestoryObject.
				// transform.collider
				//print ( _do.DestoryObject );
				if ( _do.DestoryObject &&  !_do.DestoryObject.tag.Contains("jewel")  &&_do.DestoryObject.transform.childCount > 0 )
				{
					foreach ( Transform child in _do.DestoryObject.transform )
					{
						//print ( child.tag );
						child.parent = null ;
					}
				}

				//print ( " childCount = " + _do.DestoryObject.transform.childCount);
				Destroy( _do.DestoryObject ) ;
				_AudioPlay( "RockDie_snd" ) ;

				// AudioController.Play("RockDie_snd"); // RockDie_snd //MglFire_snd
				// ChestCollision_snd Mp5Fire_snd FireballExplosion_snd
				// ChestCollision_snd
			}
			else
			{
				_destoryObject.Enqueue( _do ) ;
			}
		}
			//yield return 2;// new  WaitForSeconds(0.05f);
		
	}


	// FireballExplosion_snd

	public bool IsPlaying()
	{

		if ( IGState.Status== PlayingStatus.Playing) return  true ;
		else return false ;
	}
	
	void OnClick()
	{
		print("void OnClick()");
	}

	/// <summary>
	/// Gets the gold coin.
	/// </summary>
	public  void PlayerGetGoldCoin( Vector3 _position )
	{
		IGState.st.Score.GoldCoin = IGState.st.Score.GoldCoin+10 ;

		_ScoreFlashPushWorld (_position,new Vector2(0.0f,-30.3f),10, Color.green); // int
		//print ( _position.z );
	}

	public  bool PlayerPower(int _hp)
	{
		IGState.st.Score.HP =IGState.st.Score.HP + _hp ;
		if ( IGState.st.Score.HP > 200 ) IGState.st.Score.HP = 200 ;

		if ( IGState.st.Score.HP >0 ) return true ;
		else return false ;
	}

	public  void InitSence()
	{
		IGState.MapsIndex = 15 ;
		for(int j = 0 ; j < IGState.MapsIndex ; j++ )
		{
			for(int i = 4;i <12 ; i++ )
			{
				bool _IsJewel =false ;
				int index = _random.Next(0,20);
				if ( index == 10 ) 
				{
					_IsJewel = true ;
					index = _random.Next(6,16);
				}
				else
					index = _random.Next(0,6);

				string asset = "Prefab/" + _boxs[index] ;
				if( Resources.Load(asset) ==null  ) 
				{
					continue ;
				}
				
				GameObject ob = (GameObject)Instantiate(Resources.Load(asset));
				ob.transform.position = IGState.Maps[i,j].position ;
				
				BoxTest bt = ob.GetComponent("BoxTest") as BoxTest;
				bt.IndexX = i ;
				bt.IndexY = j ;
				if ( _IsJewel ) bt.IsJewel = true ;
			}
		}
		
		GameObject bottom = GameObject.Find("_Bottom");
		bottom.transform.position = IGState.Maps[8,IGState.MapsIndex].position ;
		float _distince = Vector3.Distance (bottom.transform.position , transform.position ) ;
		print ( "_distince = " + _distince) ;
		IGState.Status = PlayingStatus.Playing ;
	}

	public  void SenceAddBox()
	{
		IGState.MapsIndex++ ;
		int j = IGState.MapsIndex -1 ;
		if (IGState.MapsIndex == 99) {
			SenceNext() ;
				}

		if ( IGState.MapsIndex >= 99 ) return ;	
		for(int i = 4;i <12 ; i++ )
		{
			bool _IsJewel =false ;
			int index = _random.Next(0,20);
			if ( index == 10 ) 
			{
				_IsJewel = true ;
				index = _random.Next(6,16);
			}
			else
				index = _random.Next(0,6);
			
			string asset = "Prefab/" + _boxs[index] ;
			if( Resources.Load(asset) ==null  ) 
			{
				continue ;
			}
			
			GameObject ob = (GameObject)Instantiate(Resources.Load(asset));
			ob.transform.position = IGState.Maps[i,j].position ;
			
			BoxTest bt = ob.GetComponent("BoxTest") as BoxTest;
			bt.IndexX = i ;
			bt.IndexY = j ;
			if ( _IsJewel ) bt.IsJewel = true ;
			
		}

		GameObject bottom = GameObject.Find("_Bottom");
		bottom.transform.position = IGState.Maps[8,IGState.MapsIndex].position ;
		float _distince = Vector3.Distance (bottom.transform.position , transform.position ) ;
		//print ( "_distince = " + _distince) ;
	}



	public  void SenceNext()
	{
		IGState.MapsIndex = 106 ;
		for(int j = 99 ; j < IGState.MapsIndex ; j++ )
		{
			for(int i = 4;i <12 ; i++ )
			{
				string asset = "Prefab/box"  ;
				if( Resources.Load(asset) ==null  ) 
				{
					continue ;
				}
				
				GameObject ob = (GameObject)Instantiate(Resources.Load(asset));
				ob.transform.position = IGState.Maps[i,j].position ;
				
				//BoxTest bt = ob.GetComponent("BoxTest") as BoxTest;
				//bt.IndexX = i ;
				//bt.IndexY = j ;
				//if ( _IsJewel ) bt.IsJewel = true ;
			}
		}
		
		GameObject bottom = GameObject.Find("_Bottom");
		bottom.transform.position = IGState.Maps[8,20].position ;
		float _distince = Vector3.Distance (bottom.transform.position , transform.position ) ;
		//print ( "_distince = " + _distince) ;
		IGState.Status = PlayingStatus.Playing ;
	}

	public void PlayDestroyBoxEffect( Transform _transform ,float _time )
	{
		if (_transform == null) return;
		//GameObject _player = GameObject.Find("Player");
		//string asset = "Prefab/CFXM3_Hit_Ice_B_Air" ; 
		if ( _transform.childCount > 0 )
		{
			foreach ( Transform child in _transform )
			{
				// _ScoreFlashPushWorld(_player.transform.position,new Vector2(0.0f,-30.3f),10, Color.green); // int
				Destroy( (GameObject)Instantiate(Resources.Load(asset),child.position,Quaternion.identity),1.0f);
			}
		}
		// _ScoreFlashPushWorld(_player.transform.position,new Vector2(0.0f,-30.3f),10, Color.yellow); // int
		//string asset = "Prefab/CFXM3_Hit_Ice_B_Air" ; 
		Destroy( (GameObject)Instantiate(Resources.Load(asset),_transform.position,Quaternion.identity),1.0f);

		Destroy( _transform.gameObject,_time);
	}

	public void PlayDestroyEffectWithScore( Transform _transform ,float _time )
	{
		if (_transform == null) return;
		//GameObject _player = GameObject.Find("Player");
		//string asset = "Prefab/CFXM3_Hit_Ice_B_Air" ; 
		if ( _transform.childCount > 0 )
		{
			foreach ( Transform child in _transform )
			{
				//_ScoreFlashPushWorld(_player.transform.position,new Vector2(0.0f,-30.3f),10, Color.green); // int
				IGManager.gm.PlayerGetGoldCoin(_player.transform.position);

				Destroy( (GameObject)Instantiate(Resources.Load(asset),child.position,Quaternion.identity),1.0f);
			}
		}
		// _ScoreFlashPushWorld(_player.transform.position,new Vector2(0.0f,-30.3f),10, Color.yellow); // int
		IGManager.gm.PlayerGetGoldCoin(_player.transform.position);
		Destroy( (GameObject)Instantiate(Resources.Load(asset),_transform.position,Quaternion.identity),1.0f);
		Destroy( _transform.gameObject,_time);
	}

	public void PlayDestroyBoxEffect2( Transform _transform ,float _time )
	{
		//GameObject _player = GameObject.Find("Player");
		//string asset = "Prefab/CFXM3_Hit_Ice_B_Air" ; //boxexplosion"   "Prefab/boxexplosion" ;//
		if ( _transform.tag == "box6" && _transform.childCount > 0 )
		{
			foreach ( Transform child in _transform )
			{
				child.parent = null ;
			}
		}
		
		if ( _transform.tag != "box6" && _transform.childCount > 0 )
		{
			foreach ( Transform child in _transform )
			{
				
				_ScoreFlashPushWorld(_player.transform.position,new Vector2(0.0f,-30.3f),10, Color.green); // int
				//GameObject ob = (GameObject)Instantiate(Resources.Load(asset),child.position,Quaternion.identity);
				//ob.transform.position = transform.position ;
				//string asset = "Prefab/CFXM3_Hit_Ice_B_Air" ; 
				Destroy( (GameObject)Instantiate(Resources.Load(asset),child.position,Quaternion.identity),1.0f);
			}
		}

		_ScoreFlashPushWorld(_player.transform.position,new Vector2(0.0f,-30.3f),10, Color.yellow); // int

		Destroy( (GameObject)Instantiate(Resources.Load(asset),_transform.position,Quaternion.identity),1.0f);
		Destroy( _transform.gameObject,_time);
	}

	public static void GameOver()
	{
		IGState.IsGameOver = true ;
		IGState.SetObjectMoveDirection_( "static",null ) ;
		GameOverManager._GOM.GameOverPanelShow() ;
		
		// StartCoroutine( ShowGameOverPanel() );
	    //StartCoroutine( initPosition() ) ;
	}



	
	public Dictionary<string,ItemsType> JewelItemsList
	{
		get { return _jewelItemsList; }
		set { _jewelItemsList = value; }
	}
	
}
