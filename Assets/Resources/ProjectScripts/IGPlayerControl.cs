using UnityEngine;
using System.Collections;
using NarayanaGames.ScoreFlashComponent;

public class IGPlayer
{
	private Vector3 _headPosition;
	private Vector3 _legPosition;
	private Vector3 _armAttackPosition;
	private Vector3 _armIdlePosition;
	private Vector3 _packPosition;

	public GameObject _head ;
	GameObject _armidle;
	GameObject _leg;
	GameObject _armattack;
	GameObject _pack;
	GameObject _body;

	public IGPlayer()
	{
	}

	public GameObject Head
	{
		get { return _head ; }
		set { _head = value; }
	}

	public GameObject Armidle
	{
		get { return _armidle ; }
		set { _armidle = value; }
	}

	public GameObject Armattack
	{
		get { return _armattack ; }
		set { _armattack = value; }
	}

	public GameObject Leg
	{
		get { return _leg ; }
		set { _leg = value; }
	}

	public GameObject Pack
	{
		get { return _pack ; }
		set { _pack = value; }
	}

	public GameObject Body
	{
		get { return _body ; }
		set { _body = value; }
	}
	/// <summary>
	/// //
	/// </summary>
	/// <value>The head position.</value>

	public Vector3 HeadPosition
	{
	     get { return _headPosition ; }
	     set { _headPosition = value; }
	}
	
	public Vector3 LegPosition
	{
	     get { return _legPosition ; }
	     set { _legPosition = value; }
	}
	
	public Vector3 ArmAttackPosition
	{
	     get { return _armAttackPosition ; }
	     set { _armAttackPosition = value; }
	}
	
	public Vector3 ArmIdlePosition
	{
	     get { return _armIdlePosition ; }
	     set { _armIdlePosition = value; }
	}
		
	public Vector3 PackPosition
	{
	     get { return _packPosition ; }
	     set { _packPosition = value; }
	}
}

public class IGPlayerControl : MonoBehaviour {
	
	//public GameObject _head ;

	/*
	public Texture2D jewelmessagebox1;
	public Texture2D jewelmessagebox2;
	public Texture2D jewelmessagebox3;
	public Texture2D jewelmessagebox4;
	public Texture2D jewelmessagebox5;
	public Texture2D jewelmessagebox6;
	public Texture2D jewelmessagebox7;
	public Texture2D jewelmessagebox8;
*/

	IGPlayer _player ;
	
	float _flashing = -0.001f ;
	IGPlayer left;
	IGPlayer right;
	private string boxjewel = "jewel" ;
	private TextMesh textMesh;
	private GameObject trail;
	private GameObject _Player ;
	//Vector3 PlayerPosition ;
	Vector3 _vecDown ;//= transform.TransformDirection( Vector3.down ) ;
	Vector3 _vecLeft ;
	Vector3 _vecRight ;
	Vector3 _vecUp ;
	bool _isJump = false ;
	bool _isMove = false ;
	bool _isAttack = false ;
	MoveDirection md;
	private Vector2 velocity;		//Velocity
	Vector3 _swipeStartPosition ;
	Vector3 _bodyPosition = new Vector3(0.0f,-0.01f,0.0f);
	public AchievementsCustomRenderer achievementPrefab;

	private AchievementsCustomRenderer GenerateAchievement(string title, string description,int _tex) {
		AchievementsCustomRenderer achievement = (AchievementsCustomRenderer) achievementPrefab.CreateInstance(this.transform);
		achievement.AchievementUnlocked(title, description,_tex);
		return achievement;
	}

	void OnEnable(){
		EasyTouch.On_SwipeStart += On_SwipeStart;
		EasyTouch.On_Swipe += On_Swipe;
		EasyTouch.On_SwipeEnd += On_SwipeEnd;		
		EasyTouch.On_SimpleTap += On_SimpleTap;
	}
	
		
	void UnsubscribeEvent(){
		EasyTouch.On_SwipeStart -= On_SwipeStart;
		EasyTouch.On_Swipe -= On_Swipe;
		EasyTouch.On_SwipeEnd -= On_SwipeEnd;	
		EasyTouch.On_SimpleTap -= On_SimpleTap;	
	}

	void OnDisable(){
		UnsubscribeEvent();
		
	}
	
	void OnDestroy(){
		UnsubscribeEvent();
	}

	void Awake()
	{

	}


	// Use this for initialization
	void Start () {
		IGManager.gm.Reset ();
		//IGManager.gm.InitSence();
	
		_player = new IGPlayer();
		_vecDown = transform.TransformDirection( Vector3.down );
		_vecLeft  = transform.TransformDirection( Vector3.left );
		_vecRight = transform.TransformDirection( Vector3.right );
		_vecUp = transform.TransformDirection( Vector3.up );
	
	//PlayerPosition = new Vector3( transform.position.x,transform.position.y -0.01f,2.0f) ;
	//textMesh = GameObject.Find("LastSwipeText").transform.gameObject.GetComponent("TextMesh") as TextMesh;
	//_Player = gameObject ; // GameObject.Find ("clicken") ;
		
		_player.Armidle = GameObject.FindGameObjectWithTag("player_armidle");
		_player.Leg = GameObject.FindGameObjectWithTag("player_leg");
		_player.Armattack = GameObject.FindGameObjectWithTag("player_armattack");
		_player.Pack = GameObject.FindGameObjectWithTag("player_pack");
		_player.Head = GameObject.FindGameObjectWithTag("player_head");
		_player.Body= GameObject.FindGameObjectWithTag("player_body");
		//player_armidle
		//player_head 
		//player_leg
		//player_pack
		//player_armattack
		
		left =new IGPlayer();
		right = new IGPlayer();
		
		right.HeadPosition		 	 = new Vector3(0.0f,0.0220f,-0.010f);
		right.LegPosition 			 = new Vector3(0.0f,-0.03609955f,0.01f);
		right.ArmAttackPosition	 = new Vector3(-0.02931446f,0.012f,-0.011f);
		right.ArmIdlePosition		 = new Vector3(0.016f,0.016f,0.0111f);
		right.PackPosition 			 = new Vector3(-0.02293965f,0.006278038f,0.01f);
		
		left.HeadPosition             = new Vector3(-0.0f,0.022f,-0.010f) ;
		left.LegPosition               = new Vector3( 0.0f,-0.03609955f,0.01f) ;
		left.ArmAttackPosition     = new Vector3(0.02372074f,0.012f,-0.011f);
		left.ArmIdlePosition         = new Vector3(-0.016f,0.016f,0.01f);
		left.PackPosition              = new Vector3(0.02346617f,0.01180243f,0.01f);
	
		StartCoroutine( FlashingOffset() );
	}

	void PlarerDead()
	{

		Quaternion q = Quaternion.AngleAxis( -90.0f , Vector3.forward ) ;
		_player.Body.transform.localRotation=q;
		_player.Body.transform.localPosition = new Vector3(0.0f,-0.06f,0.0f);
		return ;

		_player.Armidle.AddComponent("BoxCollider") ;
		_player.Armidle.AddComponent("Rigidbody");

		_player.Leg.AddComponent("BoxCollider") ;
		_player.Leg.AddComponent("Rigidbody");

		_player.Armattack.AddComponent("BoxCollider") ;
		_player.Armattack.AddComponent("Rigidbody");

		//_player.Head.AddComponent("BoxCollider") ;
		//_player.Head.AddComponent("Rigidbody");

		//_player.Body.AddComponent("BoxCollider") ;
		//_player.Body.AddComponent("Rigidbody");

		//_player.Head.AddComponent(BoxCollider);
		//_player.Head.AddComponent(Rigidbody) ;

		//_player.Leg = GameObject.FindGameObjectWithTag("player_leg");
		//_player.Armattack = GameObject.FindGameObjectWithTag("player_armattack");
		//_player.Pack = GameObject.FindGameObjectWithTag("player_pack");
		//_player.Head = GameObject.FindGameObjectWithTag("player_head");
		//_player.Body= GameObject.FindGameObjectWithTag("player_body");
	}
	
	void SetPlayerHeadAngle(float _angle)
	{
		Quaternion q = Quaternion.AngleAxis( _angle , Vector3.forward ) ;
		_player.Head.transform.rotation=q;
	}
	
	void SetPlayerArmattackAngle(float _angle)
	{
		Quaternion q = Quaternion.AngleAxis( _angle , Vector3.forward ) ;
		_player.Armattack.transform.rotation=q;
	}
	
	void SetPlayerArmidleAngle(float _angle)
	{
		Quaternion q = Quaternion.AngleAxis( _angle , Vector3.forward ) ;
		_player.Armidle.transform.rotation=q;
	}

	IEnumerator SetPlayerRush()
	{
		//Quaternion q = new Quaternion( 0.5f, 0.0f, 0.0f, -0.9f );// Quaternion.AngleAxis( 160.0f , Vector3.left ) ;
		/*
		// 0.5, 0.0, 0.0, -0.9
		Quaternion q = new Quaternion( 0.5f, 0.0f, 0.0f, -0.9f );// Quaternion.AngleAxis( 160.0f , Vector3.left ) ;
		_player.Body.transform.localRotation= q ;
		print (  _player.Body.transform.localRotation );
		_player.Body.transform.localPosition = new Vector3(0.0f,-0.05f,00f);
*/
		_player.Body.transform.localPosition = new Vector3(0.0f,-0.06f,0.0f);
		yield return  new  WaitForSeconds(0.50f);

		// EffectHealing

	
		/*
		 q = Quaternion.AngleAxis( 0.0f , Vector3.left ) ;
		_player.Body.transform.localRotation=q;
		_player.Body.transform.localPosition = new Vector3(0.0f,0.0f,00f);

*/

		Quaternion q = Quaternion.AngleAxis( 0.0f , Vector3.forward ) ;
		_player.Body.transform.localRotation=q;
		_player.Body.transform.localPosition =  _bodyPosition ;//new Vector3(0.0f,-0.10f,0.0f);


		if ( !IGManager.gm.PlayerPower( -20 ))
			PlarerDead();
		else
		{
			GameObject _EffectHealing = GameObject.Find ( "EffectHealing" ) ;
			tk2dSpriteAnimator anim  = _EffectHealing.GetComponent("tk2dSpriteAnimator") as tk2dSpriteAnimator;  ///EffectHealing
			anim.Play();
		}


	}

	void SetPlayerLeft()
	{

		_player.Head.transform.localPosition=left.HeadPosition;
		_player.Armidle.transform.localPosition = left.ArmIdlePosition ;
		_player.Armattack.transform.localPosition = left.ArmAttackPosition;
		_player.Leg.transform.localPosition = left.LegPosition;
		_player.Pack.transform.localPosition = left.PackPosition ;
		
		tk2dSprite _tkhead = _player.Head.GetComponent("tk2dSprite") as tk2dSprite ;
		_tkhead.FlipX = true ;
		
		tk2dSprite _tkarmidle = _player.Armidle.GetComponent("tk2dSprite") as tk2dSprite ;
		_tkarmidle.FlipX = true ;
		
		tk2dSprite _tkarmattack = _player.Armattack.GetComponent("tk2dSprite") as tk2dSprite ;
		_tkarmattack.FlipX = true ;
		
		tk2dSprite _tkleg = _player.Leg.GetComponent("tk2dSprite") as tk2dSprite ;
		_tkleg.FlipX = true ;
		
		tk2dSprite _tkpack = _player.Pack.GetComponent("tk2dSprite") as tk2dSprite ;
		_tkpack.FlipX = true ;
		
		tk2dSprite _tkbody =   _player.Body.GetComponent("tk2dSprite") as tk2dSprite ;
		_tkbody.FlipX = true ;

		Quaternion q = Quaternion.AngleAxis( 0.0f , Vector3.forward ) ;
		_player.Body.transform.localRotation=q;
		_player.Body.transform.localPosition = _bodyPosition ;//new Vector3(0.0f,-0.10f,0.0f);
		
	}
	
	void SetPlayerRight()
	{
		_player.Armidle.transform.localPosition =  right.ArmIdlePosition ;
		_player.Armattack.transform.localPosition = right.ArmAttackPosition;
		_player.Leg.transform.localPosition = right.LegPosition;
		_player.Head.transform.localPosition=right.HeadPosition;
		_player.Pack.transform.localPosition = right.PackPosition ;
		
		
		tk2dSprite _tkhead = _player.Head.GetComponent("tk2dSprite") as tk2dSprite ;
		_tkhead.FlipX = false ;
		
		tk2dSprite _tkarmidle = _player.Armidle.GetComponent("tk2dSprite") as tk2dSprite ;
		_tkarmidle.FlipX = false ;
		
		tk2dSprite _tkarmattack = _player.Armattack.GetComponent("tk2dSprite") as tk2dSprite ;
		_tkarmattack.FlipX = false ;
		
		tk2dSprite _tkleg = _player.Leg.GetComponent("tk2dSprite") as tk2dSprite ;
		_tkleg.FlipX = false ;
		
		tk2dSprite _tkpack = _player.Pack.GetComponent("tk2dSprite") as tk2dSprite ;
		_tkpack.FlipX = false ;
		
		tk2dSprite _tkbody =  _player.Body.GetComponent("tk2dSprite") as tk2dSprite ;
		_tkbody.FlipX = false ;

		Quaternion q = Quaternion.AngleAxis( 0.0f , Vector3.forward ) ;
		_player.Body.transform.localRotation=q;
		_player.Body.transform.localPosition =_bodyPosition ;// new Vector3(0.0f,-0.10f,0.0f);
		
	}
	
	
	// Update is called once per frame
	void Update () {

		// print (  _body.transform.localRotation );

		GameObject wall = GameObject.Find("wall") ;
		GameObject background3 = GameObject.Find("background3") ;

		float smoothTime= 0.3f;	//Smooth Time

		/*
		Camera.main.transform.position = new Vector3(Camera.main.transform.position.x,  
		                                             Mathf.SmoothDamp( Camera.main.transform.position.y,transform.position.y, ref velocity.y, smoothTime),
		                                             Camera.main.transform.position.z);

*/
		wall.transform.position = new Vector3(wall.transform.position.x,  
		                                      Mathf.SmoothDamp( wall.transform.position.y,-Camera.main.transform.position.y/3, ref velocity.y, smoothTime),
		                                      wall.transform.position.z);

		//background3.transform.position = new Vector3(background3.transform.position.x,  
		  //                                           Mathf.SmoothDamp( background3.transform.position.y,-Camera.main.transform.position.y/6, ref velocity.y, smoothTime),
		    //                                         background3.transform.position.z);


		// wall
		RaycastHit _ray ;	
		if ( !_isJump && Physics.Raycast( transform.position , _vecUp, out _ray,  0.1f ))
		{
			Transform _tf = _ray.collider.transform ;
			if ( Vector3.Distance( transform.position,_tf.position ) <0.16f )
			{
				if ( _tf.tag.Contains( boxjewel))
				{
					JewelMessages(_ray.collider.gameObject);
				}
				else
				{
					print ("QQQQQQQQQQQQQQQQQQQQ") ;
					StartCoroutine(SetPlayerRush());
					;

					IGManager.gm.PlayDestroyBoxEffect( _tf ,0.0f) ; 
					RaycastHit[]  rh = Physics.RaycastAll(transform.position, _vecUp,1.0f ) ;//,  _ray,  100.0f  ) ;
					for( int i = 0 ; i <rh.Length ; i ++ )
					{
						IGManager.gm.PlayDestroyBoxEffect(rh[i].collider.transform ,0.0f) ; 
					}
				}
			}
		}
		if ( _isJump || _isMove ) return ;
		MoveDownSingle();
	}

	IEnumerator Jump(MoveDirection _md, float _movestep )
	{
		//bool _isjump = true ;
		//yield return  new  WaitForSeconds(0.6666667f/3);
		float _step = 0.0f ;
		int count = 0 ;
		Vector3 vecd = _vecLeft ;
		if ( _md == MoveDirection.Left ) {  _step = -IGState.StepMoveDistince ;}
		else { vecd = _vecRight ; _step = IGState.StepMoveDistince ;}
		tk2dSpriteAnimator anim = _player.Leg.GetComponent("tk2dSpriteAnimator") as tk2dSpriteAnimator ;
		RaycastHit _ray;	
		while ( _isJump )
		{

		

			if ( count ++ < 8 )
			{
				if (  Physics.Raycast( transform.position , _vecUp , out _ray,  0.1f ))
				{
					md= MoveDirection.Static ;
					anim.Play("PlayerLegsStandGray") ;
					_isJump = false ;
					break ;
				}

				transform.position = new Vector3( transform.position.x ,    transform.position.y+ IGState.StepMoveDistince*2,   transform.position.z  ) ;
				if(count == 8){ anim.Play("PlayerLegsRunGray") ;}
				//print( " count = " + count +" -- " + transform.position.y ) ;
			
			}
			else
			{
				//RaycastHit _ray;	
				if (  Physics.Raycast( transform.position , vecd, out _ray,  0.1f ))
				{
					_isJump = false ;
					md= MoveDirection.Static ;
					_step = 0.0f ;
				}
				transform.position = new Vector3( transform.position.x +_step,    transform.position.y ,transform.position.z);
				if ( count ++ > 24 )
				{
					_isJump = false ;
					md= MoveDirection.Static ;
					//tk2dSpriteAnimator anim =_leg.GetComponent("tk2dSpriteAnimator") as tk2dSpriteAnimator ;
					anim.Play("PlayerLegsStandGray") ;
				}
			}
			yield return  1 ; // new  WaitForSeconds(0.033333f);
	}
}

	IEnumerator MoveStandard(MoveDirection _md, float _movestep )
	{
		//bool _isjump = true ;
		//yield return  new  WaitForSeconds(0.6666667f/3);
		float _step = 0.0f ;
		int count = 0 ;
		_isMove = true ;
		tk2dSpriteAnimator anim =_player.Leg.GetComponent("tk2dSpriteAnimator") as tk2dSpriteAnimator ;
		anim.Play("PlayerLegsRunGray") ;

		while ( _isMove )
		{
			Vector3 vecd = _vecLeft ;
			if ( _md == MoveDirection.Left ) {  _step = -IGState.StepMoveDistince ;}
			else { vecd = _vecRight ; _step = IGState.StepMoveDistince ;}
			
		
			if ( count ++ < 16)
			{
				RaycastHit _ray;	
				if (  Physics.Raycast( transform.position , vecd, out _ray,  0.1f ))
				{
					_isMove = false ;
					md= MoveDirection.Static ;
					_step = 0.0f ;
				}
				
				transform.position = new Vector3( transform.position.x +_step,    transform.position.y ,transform.position.z);
				if ( count == 16 )
				{
					_isMove = false ;
				    md= MoveDirection.Static ;
					anim.Play("PlayerLegsStandGray") ;
				}
			}
			yield return  1 ; // new  WaitForSeconds(0.033333f);
		}
	}

	void JewelPowder( string _name )
	{
		_name = _name.Replace( "_jewel" , "") ;
		GameObject [] go = GameObject.FindGameObjectsWithTag( _name ) ;
		foreach ( GameObject _go in go)
		{
			IGManager.gm.PlayDestroyBoxEffect( _go.transform ,0.0f);
		}
	}
	//box1jewel,box2jewel ,box3jewel,box4jewel,box5jewel,box6jewel,box7jewel,box8jewel,
	void JewelMessages( GameObject _jewel )// string jeweltag )
	{
		//  _ray.collider.gameObject
		string asset = "Prefab/CFXM2_PickupDiamond" ; 
		Destroy( (GameObject)Instantiate(Resources.Load(asset),_jewel.transform.position,Quaternion.identity),3.0f);

		string jeweltag = _jewel.tag ;
		Destroy( _jewel ) ;

		int _tex  = 1 ;
		if ( jeweltag == "box1jewel" ) {_tex = 1 ;}
		else if ( jeweltag == "box2_jewel" ) {_tex = 2 ;}
		else if ( jeweltag == "box3_jewel" ) {_tex = 3 ;}
		else if ( jeweltag == "box4_jewel" ) {_tex = 4 ;}
		else if ( jeweltag == "box5_jewel" ) {_tex = 5 ;}
		else if ( jeweltag == "box6_jewel" ) {_tex = 6 ;}
		else if ( jeweltag == "box7_jewel" ) {_tex = 7 ;}
		else if ( jeweltag == "box8_jewel" ) {_tex = 8 ;}
		else if ( jeweltag == "box9_jewel" ) {_tex = 9 ;}
		else if ( jeweltag == "box10_jewel" ) 
		{
			_tex = 10 ;
			IGManager.gm.JewelPowerEnergy();
			return ;
		}

		//print ( " jeweltag = " + jeweltag + " " + _tex) ;

		IGManager.gm._ScoreFlashPushScreen ( new Vector2(Screen.width/2  ,0.0f),(GenerateAchievement( "",  "",_tex )));  

		// ScoreFlash.Instance.PushScreen(new Vector2(Screen.width/2  ,0.0f),(GenerateAchievement( "",  "",_tex )));  

		IGManager.gm.PickupItems (_jewel);

	}

	IEnumerator MoveJewel(MoveDirection _md ) 
	{
		//bool _isjump = true ;
		//yield return  new  WaitForSeconds(0.6666667f/3);
		float _stepX = 0.0f ;
		float _stepY = 0.0f ;
		int count = 0 ;
		_isMove = true ;
		tk2dSpriteAnimator anim =_player.Leg.GetComponent("tk2dSpriteAnimator") as tk2dSpriteAnimator ;
		anim.Play("PlayerLegsRunGray") ;
		
		while ( _isMove )
		{
			Vector3 vecd = _vecLeft ;
			if ( _md == MoveDirection.Left )
			{  
				_stepX = -IGState.StepMoveDistince ;
			}
			else if ( _md == MoveDirection.Right )
			{ 
				vecd = _vecRight ; 
				_stepX = IGState.StepMoveDistince ;
			}
			else if ( _md == MoveDirection.Down )
			{ 
				vecd = _vecDown ; 
				_stepY = -IGState.StepMoveDistince ;
			}

			if ( count ++ < 16)
			{
				RaycastHit _ray;	
				if (  Physics.Raycast( transform.position , vecd, out _ray,  0.1f ))
				{
					if ( _ray.collider.transform.tag.Contains ( boxjewel  ) )
					{  
						// JewelPowder ( _ray.collider.transform.tag ) ;



						//Destroy ( )  ; 

						JewelMessages( _ray.collider.gameObject ) ;
						// Get a green  Jewel
					}
					else
					{
							_isMove = false ;
							md= MoveDirection.Static ;
							_stepX = 0.0f ;
					}
				}
				
				transform.position = new Vector3( transform.position.x +_stepX,    transform.position.y  + _stepY,transform.position.z);
				if ( count == 16 )
				{
					_isMove = false ;
					md = MoveDirection.Static ;
					anim.Play("PlayerLegsStandGray") ;
					//anim.
				}
			}
			yield return  1 ; // new  WaitForSeconds(0.033333f);
		}
	}

	void MoveDownSingle()
	{
		//Physics2D.Raycast(
		RaycastHit _ray;	
		//Vector3 vec = new Vector3(transform.position.x ,transform.position.y+0.012f ,transform.position.z );
		if (  Physics.Raycast( transform.position , _vecDown, out _ray,  IGState.RayDistince ))
		{
			// 向下运动
			if ( _ray.collider.transform.tag.Contains ( boxjewel ) )
			{
				StartCoroutine ( MoveJewel (   MoveDirection.Down) ) ;
				return ;
			}

			if( md == MoveDirection.Static)
			{

				transform.position = new Vector3(_ray.collider.transform.position.x ,  _ray.collider.transform.position.y+0.166667f ,transform.position.z);
			}
			else
			{
				transform.position = new Vector3( transform.position.x ,  _ray.collider.transform.position.y+0.166667f ,transform.position.z);
			}
		}
		else
		{
			transform.position = new Vector3( transform.position.x ,  transform.position.y- IGState.StepMoveDistince ,transform.position.z);
		}
		
	}

	IEnumerator FlashingOffset()
	{
		//yield return  new  WaitForSeconds(0.6666667f/3);
		while ( true )
	   {
			_flashing = -_flashing ;
			_player.Head.transform.position = new Vector3(_player.Head.transform.position.x ,  _player.Head.transform.position.y + _flashing  ,_player.Head.transform.position.z) ;
		
		    //print ( "  FlashingOffset ------ " );
			//if ( _flashing > 0 ) SetPlayerLeft();
			//else SetPlayerRight();
			GameObject bottom = GameObject.Find("_Bottom");
			float _distince = Vector3.Distance ( bottom.transform.position , transform.position ) ;
			if ( _distince < 1.200f )
			{
				IGManager.gm.SenceAddBox();
				//print ( " 00 _distince = " + _distince );
			}
	        yield return  new  WaitForSeconds(0.6666667f/2);
	   }
	}
	
	
	// At the swipe beginning 
	private void On_SwipeStart( Gesture gesture){

		if ( !IGManager.gm.IsPlaying()) return ;

		// Only for the first finger
		if (gesture.fingerIndex==0 ) // && trail==null)
		{ 
			// the world coordinate from touch for z=5
			//Vector3 position = gesture.GetTouchToWordlPoint(5);
			//_swipeStartPosition = position;
			//trail = Instantiate( Resources.Load("Trail"),position,Quaternion.identity) as GameObject;
		}
	}
	
	// During the swipe
	private void On_Swipe(Gesture gesture){
	}
	
	// At the swipe end 
	private void On_SwipeEnd(Gesture gesture){
		if ( !IGManager.gm.IsPlaying()) return ;
		if ( _isJump ) return ;
		if (  gesture.swipeLength  < 20.0f )
		{
			tap(  gesture.position  ) ;
			return ;
		}

		// Get the swipe angle
		float angles = gesture.GetSwipeOrDragAngle();
		string s =  "  Last swipe : " + gesture.swipe.ToString() + " /  vector : " + gesture.swipeVector.normalized + " / angle : " + angles.ToString("f2") + " / " + gesture.deltaPosition.x.ToString("f5");
		string _swipe =  gesture.swipe.ToString().ToLower();

		if (  _swipe == "right" )
		{
			TabRight() ;
		}
		else
		{
			TabLeft();
		}

		//Swipe( _swipe , gesture.position );

}

	public void TabRight()
	{
		tk2dSpriteAnimator anim =_player.Leg.GetComponent("tk2dSpriteAnimator") as tk2dSpriteAnimator ;
		SetPlayerRight() ;
		RaycastHit _ray ;	
		if (  Physics.Raycast( transform.position , _vecRight, out _ray,  0.1f ))
		{
			if ( _ray.collider.transform.tag.Contains ( boxjewel ) )
			{
				anim.Play("PlayerLegsRunGray") ; 
				StartCoroutine ( MoveJewel (   MoveDirection.Right) ) ;
			}
			/*
				else if( _ray.collider.transform.tag.Contains ( "box" ) )
				{
					tap( position);
				}*/
			else
			{
				// *************************************
				anim.Play("PlayerLegsJumpGray") ;
				//md= MoveDirection.Static ;
				_isJump = true ;
				StartCoroutine ( Jump (   MoveDirection.Right, 0.01f) ) ;
			}
		}
		else
		{
			anim.Play("PlayerLegsRunGray") ;
			_isJump = false ;
			//rigidbody.AddForce( new Vector3(600.0f,8.0f,0.0f),ForceMode.Impulse) ;
			md = MoveDirection.Right;
			StartCoroutine ( MoveStandard(MoveDirection.Right,0.0f));
		}
}

	public void TabLeft()
	{
		tk2dSpriteAnimator anim = _player.Leg.GetComponent ("tk2dSpriteAnimator") as tk2dSpriteAnimator;
		SetPlayerLeft();
		RaycastHit _ray;	
		if (  Physics.Raycast( transform.position , _vecLeft, out _ray,  0.1f ))
		{
			if ( _ray.collider.transform.tag.Contains ( boxjewel ) )
			{
				anim.Play("PlayerLegsRunGray") ; 
				StartCoroutine ( MoveJewel (   MoveDirection.Left ) ) ;
			}/*
				else if ( _ray.collider.transform.tag.Contains ( "box" ) )
				{
					tap(position ) ;
				}*/
			else
			{
				anim.Play("PlayerLegsJumpGray") ;
				_isJump = true ;
				StartCoroutine ( Jump ( MoveDirection.Left , -0.01f) ) ;
			}
		}
		else
		{
			anim.Play("PlayerLegsRunGray") ;
			_isJump = false ;
			//rigidbody.AddForce( new Vector3(-2.0f,0.60f,0.0f),ForceMode.Impulse) ;
			md = MoveDirection.Left;
			StartCoroutine ( MoveStandard(MoveDirection.Left,0.0f));
		}
	
	
	}


	//playerBackpackStand
	//PlayerBodyJumpGray
	//PlayerBodyRunGray
	//PlayerBodyStandGray
	//PlayerHeadHelmet
	//PlayerLegsJumpGray
	//PlayerLegsRunGray
	//PlayerLegsStandGray
	//PlayerPickAttack
	//PlayerSwordAttack
	//PlayerWenchNakedBg
	
	private void On_SimpleTap( Gesture gesture){

		if ( gesture.position.x <100.0f && gesture.position.y <50.0f) return ;
		if ( !IGManager.gm.IsPlaying()) return ;
		//print("tap......." + gesture.position.x + "--"+ gesture.position.y);
		if ( _isJump ) return ;
		tk2dSpriteAnimator anim = _player.Armattack.GetComponent("tk2dSpriteAnimator") as tk2dSpriteAnimator ;
		if (anim.IsPlaying("PlayerSwordAttack")) return; 

		string _direction = "left";


		if( transform.position.x > gesture.position.x ) _direction = "left";
		else _direction = "right";

		//Swipe (_direction, gesture.position);
		tap(  gesture.position  ) ;
	}

	public void tap( Vector3 tapPosition  )
	{
		Quaternion q = Quaternion.AngleAxis( 0.0f , Vector3.forward ) ;
		_player.Body.transform.localRotation=q;
		_player.Body.transform.localPosition =_bodyPosition ;// new Vector3(0.0f,-0.10f,0.0f);

		//if ( _isAttack ) return ;
		Vector3 playerPosition = Camera.main.WorldToScreenPoint( transform.position ) ;
		Vector3 _vec =  GetAngle( tapPosition , playerPosition ) ;


		PlayerAttack( _vec ) ;


		if (!IGManager.gm.PlayerPower(-1))
		{
			PlarerDead();
		}

	}

	void PlayerAttackAnimation()
	{
		tk2dSpriteAnimator anim = _player.Armattack.GetComponent("tk2dSpriteAnimator") as tk2dSpriteAnimator ;
		anim.Play("PlayerSwordAttack") ;//PlayerPickAttack  // PlayerSwordAttack  PlayerFlaregunShot

		AudioController.Play("SwordAttack_snd");
		//AudioController.Play("BoxCollision_snd");
		//BoxCollision_snd.mp3
		//HeroJump_snd
		//PickAttack_snd
		//CoinCollision_snd
		//CoinCollect_snd
	}

	void PlayerAttack( Vector3 _fwd  )
	{
		RaycastHit _ray ;	
		//Vector3 vec = new Vector3(transform.position.x ,transform.position.y ,transform.position.z );
		if (Physics.Raycast (transform.position, _fwd, out _ray, IGState.RayDistince * 1.2f)) {
						Transform _tfs = _ray.collider.transform;
						// *********************************************************************************
						if (_tfs.tag == "box") {
								PlayerAttackAnimation ();
								IGManager.gm.Reset ();
								return;
						}
						// *********************************************************************************

						if (_tfs.tag.Contains ("jewel")) {
								return;
						}

						if (_tfs.tag == "box6") {
								//anim.Play("PlayerSwordAttack") ;//PlayerPickAttack  // PlayerSwordAttack
								StartCoroutine (StoneBoxAttacked (_tfs));
								PlayerAttackAnimation ();
								return;
						}
		

						if (_tfs.parent == null) {
								StartCoroutine (DestoryAttacked (_tfs));
						} else if (_tfs.parent != null) {
								StartCoroutine (DestoryAttacked (_ray.collider.transform.parent));
						}

						//Destroy( _ray.collider.gameObject,0.6f);
						PlayerAttackAnimation ();

				} else {

			if ( _fwd == transform.TransformDirection( Vector3.right ) || _fwd == transform.TransformDirection( Vector3.up ) )
			{

			}



				}


		// 
		//rigidbody2D.
		
	}


	IEnumerator StoneBoxAttacked( Transform tf)
	{
		_isAttack = true ;
		//if ( _isAttack) StopCoroutine("StoneBoxAttacked");// = true ;
		yield return  new  WaitForSeconds(0.6666667f/2);
		BoxTest box =  tf.GetComponent("BoxTest") as BoxTest ;
		tk2dSpriteAnimator anim = tf.GetComponent("tk2dSpriteAnimator") as tk2dSpriteAnimator ;
		//if ( anim.IsPlaying("box6") ) return ;

		int i = box.FrameIndex++ ;
		if ( i < 1 ) i = 1;
		if ( i == 5 )
		{
			IGManager.gm.PlayDestroyBoxEffect2( tf ,0.0f);
			//return ;
		}
			//i = 5 ;
		anim.SetFrame(i);
		_isAttack = false ;
	}

	IEnumerator DestoryAttacked( Transform tf)
	{
		yield return  new  WaitForSeconds(0.6666667f/2);
		IGManager.gm.PlayDestroyEffectWithScore( tf ,0.0f);
	}

	Vector3 GetAngle( Vector3 p1_tap, Vector3 p2_ori  )
	{
		string direction = "left" ;
		if ( p1_tap.x > p2_ori.x )
		{
			direction = "right" ;
			SetPlayerRight() ;
		}
		else
		{
			SetPlayerLeft() ;
		}

			//Vector3 p1 =  transform.position ; //GameObject.Find("ship_ropedot").transform.position ;
			//Vector3 p2 = GameObject.Find("hook").transform.position ;
			float angle = Mathf.Atan( (p2_ori.y - p1_tap.y) / (p2_ori.x - p1_tap.x)); 
			float _angle =angle*180/Mathf.PI ;
			//1
			if(p2_ori.x<p1_tap.x &&p2_ori.y <p1_tap.y)
			{
				//if( _angle >= 50 )
				//	_angle = 50 ;
			}//2
			else if (p2_ori.x>p1_tap.x &&p2_ori.y <p1_tap.y)
			{
				_angle +=180;
				//if( _angle >= 50 )
				//	_angle = 50 ;
			}//3
			else if (p2_ori.x>p1_tap.x &&p2_ori.y >p1_tap.y)
			{
				_angle+=180;
				//if( _angle >= 50 )
				//	_angle = 50 ;
			}//4
			else if (p2_ori.x<p1_tap.x &&p2_ori.y >p1_tap.y)
			{
				_angle+=360;
				//	if( _angle <= 330 )
				//	 _angle = 330 ;
			}
			//return _angle ;


		//***************************************

		//rigidbody2D.AddForce(new Vector2(1.0f,0.0f));
		Vector3 _fwd = transform.TransformDirection( Vector3.down );
		if (_angle < 45 || _angle > 315 )
		{
			SetPlayerHeadAngle(0.0f);
			SetPlayerArmattackAngle(45.0f);
			SetPlayerArmidleAngle(0.0f);
			
			_fwd = transform.TransformDirection( Vector3.right );
		}
		else if (_angle > 45 && _angle < 135 )
		{
			if ( direction == "right" )
			{
				SetPlayerHeadAngle(45.0f);
				SetPlayerArmattackAngle(120.0f);
				SetPlayerArmidleAngle(50.0f);
			}
			else
			{
				SetPlayerHeadAngle(-45.0f);
				SetPlayerArmattackAngle(-120.0f);
				SetPlayerArmidleAngle(-50.0f);
			}
			
			_fwd = transform.TransformDirection( Vector3.up );


		}
		else if (_angle > 135 && _angle < 225 )
		{
			
			SetPlayerHeadAngle(0.0f);
			SetPlayerArmattackAngle(-45.0f);
			SetPlayerArmidleAngle(0.0f);
			
			_fwd = transform.TransformDirection( Vector3.left );
		}
		else if (_angle > 225 && _angle < 315 )
		{
			if ( direction == "right" )
			{
				SetPlayerHeadAngle(-45.0f);
				SetPlayerArmattackAngle(-45.0f);
				SetPlayerArmidleAngle(-45.0f);
			}
			else
			{
				SetPlayerHeadAngle(45.0f);
				SetPlayerArmattackAngle(45.0f);
				SetPlayerArmidleAngle(45.0f);
			}
			_fwd = transform.TransformDirection( Vector3.down );
		}
		return _fwd ;
	}

	void OnCollisionEnter (Collision col ) 
	{
		print (  "col.collider.name  = " +  col.collider.name ) ;
	}
	
	void PlayerAttackAction( float _angle,string direction  )
	{
		//rigidbody2D.AddForce(new Vector2(1.0f,0.0f));
		Vector3 _fwd = transform.TransformDirection( Vector3.down );
		if (_angle < 45 || _angle > 315 )
		{
			SetPlayerHeadAngle(0.0f);
			SetPlayerArmattackAngle(45.0f);
			SetPlayerArmidleAngle(0.0f);
			
			_fwd = transform.TransformDirection( Vector3.right );
		}
		else if (_angle > 45 && _angle < 135 )
		{
			if ( direction == "right" )
			{
				SetPlayerHeadAngle(45.0f);
				SetPlayerArmattackAngle(120.0f);
				SetPlayerArmidleAngle(50.0f);
			}
			else
			{
				SetPlayerHeadAngle(-45.0f);
				SetPlayerArmattackAngle(-120.0f);
				SetPlayerArmidleAngle(-50.0f);
			}
			
			_fwd = transform.TransformDirection( Vector3.up );
		}
		else if (_angle > 135 && _angle < 225 )
		{
			
			SetPlayerHeadAngle(0.0f);
			SetPlayerArmattackAngle(-45.0f);
			SetPlayerArmidleAngle(0.0f);
			
			_fwd = transform.TransformDirection( Vector3.left );
		}
		else if (_angle > 225 && _angle < 315 )
		{
			if ( direction == "right" )
			{
				SetPlayerHeadAngle(-45.0f);
				SetPlayerArmattackAngle(-45.0f);
				SetPlayerArmidleAngle(-45.0f);
			}
			else
			{
				SetPlayerHeadAngle(45.0f);
				SetPlayerArmattackAngle(45.0f);
				SetPlayerArmidleAngle(45.0f);
			}
			
			_fwd = transform.TransformDirection( Vector3.down );
		}
		
		RaycastHit _ray;	
		Vector3 vec = new Vector3(transform.position.x ,transform.position.y ,transform.position.z );
		if (  Physics.Raycast( vec , _fwd, out _ray,  IGState.RayDistince*2 ))
		{
			
			Transform _tfs = _ray.collider.transform ;
			
			if(_tfs.tag == "box6" )
			{
				tk2dSpriteAnimator anim = _player.Armattack.GetComponent("tk2dSpriteAnimator") as tk2dSpriteAnimator ;
				if (anim.IsPlaying("PlayerSwordAttack")) return; 
				//anim.Play("PlayerSwordAttack") ;//PlayerPickAttack  // PlayerSwordAttack
				StartCoroutine( StoneBoxAttacked(_tfs)) ;
				PlayerAttackAnimation() ;
				return ;
			}
			
			if( _tfs.parent == null )
			{
				StartCoroutine( DestoryAttacked( _tfs )) ;
			}
			else if ( _tfs.parent != null )
			{
				StartCoroutine( DestoryAttacked(  _ray.collider.transform.parent  )) ;
			}
			
			//Destroy( _ray.collider.gameObject,0.6f);
		}
		
		PlayerAttackAnimation() ;
		// 
		//rigidbody2D.
		
	}

}
