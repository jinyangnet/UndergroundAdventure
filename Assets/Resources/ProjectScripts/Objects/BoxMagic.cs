using UnityEngine;
using System.Collections;

public class BoxMagic : MonoBehaviour {
	
	
	private GameObject left_Effect ;
	private GameObject right_Effect ;
	private GameObject up_Effect ;
	private GameObject down_Effect ;
	string asset_up 	= "particle/__billboard12_up"    ;
	string asset_down 	= "particle/__billboard12_down"  ;
	string asset_left 	= "particle/__billboard12_left"  ;
	string asset_right 	= "particle/__billboard12_right" ;
	private bool _isActivation ;
	// Use this for initialization
	void Start () {
		
		_isActivation = false ;

		/*
		 GameObject left = (GameObject)Instantiate(left_Effect);
		 left.transform.position = transform.position ;
		
		 GameObject right = (GameObject)Instantiate(right_Effect);
		 right.transform.position = transform.position ;
		
		 GameObject down = (GameObject)Instantiate(down_Effect);
		 down.transform.position = transform.position ;
		
		GameObject up = (GameObject)Instantiate(up_Effect);
		 up.transform.position = transform.position ;
		 
		 */
		
		//left_Effect = (GameObject)Instantiate(Resources.Load(asset_left) );
		//left_Effect.transform.position = transform.position ;
	
	}
	
	public bool IsActivation
	{
	     get { return _isActivation ; }
	     set { _isActivation = value; }
	}
	
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void SetLeftEffect()
	{
		if( Resources.Load(asset_left) !=null ) 
		{
			left_Effect = (GameObject)Instantiate(Resources.Load(asset_left) );
			left_Effect.transform.position = transform.position ;
		}
	}
	
	public void SetRightEffect()
	{
		if( Resources.Load(asset_right) !=null ) 
		{
			right_Effect = (GameObject)Instantiate(Resources.Load(asset_right) );
			right_Effect.transform.position = transform.position ;
		}
	}
	
	public void SetUpEffect()
	{
		if( Resources.Load(asset_up) !=null ) 
		{
		   up_Effect = (GameObject)Instantiate(Resources.Load(asset_up) );
		   up_Effect.transform.position = transform.position ;
		}
	}
	
	public void SetDownEffect()
	{
		if( Resources.Load(asset_down) !=null ) 
		{
			down_Effect = (GameObject)Instantiate(Resources.Load(asset_down) );
			down_Effect.transform.position = transform.position ;
		}
	}
	
	
	public void SetEffect()
	{
		
		print( "SetEffect()" ) ;
		
		if(IGState.MoveDirection == MoveDirection.Up )
		{
				if( Resources.Load(asset_down) !=null && down_Effect == null) 
				{
					down_Effect = (GameObject)Instantiate(Resources.Load(asset_down) );
					down_Effect.transform.position = transform.position ;
				}
		}
		
		else if(IGState.MoveDirection == MoveDirection.Down )
		{
				if( Resources.Load(asset_up) !=null &&  up_Effect == null ) 
				{
				   up_Effect = (GameObject)Instantiate(Resources.Load(asset_up) );
				   up_Effect.transform.position = transform.position ;
				}
		}
		
		else if(IGState.MoveDirection == MoveDirection.Left )
		{
				if( Resources.Load(asset_right) !=null && right_Effect == null   ) 
				{
					right_Effect = (GameObject)Instantiate(Resources.Load(asset_right) );
					right_Effect.transform.position = transform.position ;
				}
		}
		
		else if(IGState.MoveDirection == MoveDirection.Right )
		{
				if( Resources.Load(asset_left) !=null && left_Effect == null )
				{
					left_Effect = (GameObject)Instantiate(Resources.Load(asset_left) );
					left_Effect.transform.position = transform.position ;
				}
		}
		
		if ( left_Effect ==null || right_Effect == null || up_Effect ==null || down_Effect == null )
		{
			_isActivation = false ;
			
			Debug.Log( "left_Effect ==null || right_Effect == null || up_Effect ==null || down_Effect == null count = " + IGState.PlayingStyleTheFifthElementList.Count ) ;
		}
		else
		{
			_isActivation = true ;
			if ( IGState.PlayingStyleTheFifthElementList.ContainsKey(gameObject))
			{
				IGState.PlayingStyleTheFifthElementList.Remove(gameObject) ;
				Debug.Log( "GameState.SpecialObjectsList.Remove(gameObject) count = " + IGState.PlayingStyleTheFifthElementList.Count ) ;
			}
			else
			{
				Debug.Log( "GameState.SpecialObjectsList.NOT Remove(gameObject) count = " + IGState.PlayingStyleTheFifthElementList.Count ) ;
			}
		}
	}
	
	void OnDestroy()
	{
		Destroy( left_Effect );
		Destroy( right_Effect );
		Destroy( up_Effect );
		Destroy( down_Effect );
	}
}
