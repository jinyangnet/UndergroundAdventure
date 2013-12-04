using UnityEngine;
using System.Collections;
using System.Collections.Generic ;

public class GameOverManager : MonoBehaviour {
	
	public GameObject objects;
	public GameObject particlesStarEffects;
	
	/// <summary>
	/// The star1.
	/// </summary>
	public GameObject star1;
	public GameObject star2;
	public GameObject star3;
	
	public GameObject star1Prefab;
	public GameObject star2Prefab;
	public GameObject star3Prefab;
	
	//  public Transform platformPrefab;
	/// <summary>
	/// The game over panel.
	/// </summary>
	public  GameObject gameOverPanel ;
	
	public  Transform _transfromPanel_from ;
	public  Transform _transfromPanel_to ;
	
	public static GameOverManager _GOM ;
	
	
	/// <summary>
	/// The star prefab.
	/// </summary>
	/// 
	private GameObject starPrefab ;
	private GameObject star ;
	
	private int _starIndex = 0 ;
	private Dictionary<int, GameObject> _GameObjectTempList = new Dictionary<int, GameObject>();
	
	void Awake ()
	{
		_starIndex= 0 ;
		_GOM = this ;
		
		//__transfromPanel_from = (Transform)Instantiate(_transfromPanel_from);
		//__transfromPanel_to   = (Transform)Instantiate(_transfromPanel_to);
		//print( "GameOverManager Awake ()" ) ;
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		
	}
	
	 public int  StarIndex
     {
          get { return _starIndex; }
          set { _starIndex = value; }
     }
	
	/// <summary>
	/// Shows the star.
	/// 
	/// </summary>
	void showStar()
	{
		print("----showStar---- _starIndex="+ _starIndex ) ;
		
		// GameObject starPrefab ;
		// GameObject star ;
		
		if( _starIndex >= IGState.StarNumber ) 
		{
			return ;
		}
		
		if(_starIndex == 0 )
		{
			starPrefab = star1Prefab ;
			star       = star1 ;
		}
		else if( _starIndex ==1)
		{
			starPrefab = star2Prefab ;
			star       = star2 ;
		}
		else if (_starIndex == 2)
		{
			starPrefab = star3Prefab ;
			star       = star3 ;
		}
		else
		{
			return ;
		}
		_starIndex ++ ;
		
	
		
		// ttf.Play(false) 
	    //GameObject  _temp=  NGUITools.AddChild(gameOverPanel, starPrefab ) ;
		//_GameObjectTempList.Add(_starIndex,_temp ) ;
		// GameObject particle = getParticle( star1.transform.position ); //(GameObject)Instantiate(particlesStarEffects);
	}
	
	//
	/// <summary>
	/// Shows the star effect.
	/// </summary>
	void showStarEffect()
	{
		//print( "---- showStarEffect222 ----" );
		//GameObject particle = 
		getParticle( star.transform.position ) ;
		if( _starIndex < 3 )
		{
			showStar() ;
		}
	}
	
	
	void showStarEffects(GameObject _object)
	{
		//GameObject particle = 
		getParticle( _object.transform.position );
	}
	
	#region start show
	
	/// <summary>
	/// Games the over panel show.
	/// update level data
	/// show star
	/// </summary>
	public void GameOverPanelShow()
	{
		IGState.IsGameOver = true ;
		StartCoroutine( WaitGameOverPanelShow() );
	}
	
	IEnumerator WaitGameOverPanelShow()
	{
		yield return new WaitForSeconds(1f);
	
	}
	
	public void GameOverPanelHide()
	{
		//print("begin gameOverPanelShow ") ;
		foreach (KeyValuePair<int, GameObject> gb in _GameObjectTempList)
        {
			Destroy(gb.Value) ;
        }
		_GameObjectTempList.Clear();
		_starIndex = 0 ;
		//gameOverPanel.transform.localPosition = new Vector3(0,10000,0) ;// _transfromPanel_from.position ; 
		//return  ;
		if(IGState.IsGameOver)
		{
		
		}
		else
		{
			gameOverPanel.transform.localPosition = new Vector3(0,10000,0) ;
		}
		// 
		//SQLManager.updateStarNumber(1,0);

		//print("end gameOverPanelShow ") ;
	}
	
	#endregion 
	
	private GameObject getParticle( Vector3 position )
	{
		GameObject particles = (GameObject)Instantiate(particlesStarEffects);
		//float Y = 0.0f ;
		//particles.transform.localScale =new Vector3(0,0,0) ;
		particles.transform.position =  new Vector3( position.x,position.y,-1 ) ; // new Vector3(0,0,-1);
		return particles;
	}
	
	
}
