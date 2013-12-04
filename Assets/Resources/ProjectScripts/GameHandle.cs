using UnityEngine;
using System.Collections;

public class GameHandle : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	void OnClick( )
	{
		
		//print(  GameState.MoveDirection );
		if(IGState.MoveDirection == MoveDirection.Static )
		{
		}
		else
		{
			return ;
		}
		//transform.name
		if (transform.name == "right" || transform.name == "left" || transform.name == "down"|| transform.name == "up")
		{
			IGState.SetObjectMoveDirection_( transform.name ,null ) ;
			
			//print (  transform.name );
		}
	}
	
}
