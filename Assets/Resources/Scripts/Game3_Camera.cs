using UnityEngine;
using System.Collections;

public class Game3_Camera : MonoBehaviour
{
	public Transform target;		//The player
	private Vector2 velocity;		//Velocity
	

	void Start () {
		
		
		//return;
		/*
		for(int i =0 ;i<100;i++)
		{
			int j = _random.Next(10,20) ;
			string asset = "Prefab/box2" ;
			if( Resources.Load(asset) ==null ) 
			{
				continue ;
			}
			GameObject ob = (GameObject)Instantiate(Resources.Load(asset));
			ob.transform.position = GameState.GameFieldMaps[j][i].position ;
		}
		*/


		/*
		
		for(int i = 2 ;i <14 ; i++ )
		{
			
			int j =27 ;
			{
				 	string asset = "Prefab/box0" ;
					if( Resources.Load(asset) ==null ) 
					{
						continue ;
					}
					GameObject ob = (GameObject)Instantiate(Resources.Load(asset));
					ob.transform.position = GameState.Maps[i,j].position ;
			}
		}
		*/
		
	}
	
	void  Update ()
	{
		// Rectangle.Contains
		// Set the position
		// transform.position = new Vector3(transform.position.x,  Mathf.SmoothDamp( transform.position.y, target.position.y, ref velocity.y, smoothTime),transform.position.z);
		float smoothTime= 0.3f;	//Smooth Time
		Camera.main.transform.position = new Vector3(Camera.main.transform.position.x,  Mathf.SmoothDamp( Camera.main.transform.position.y,transform.position.y, ref velocity.y, smoothTime),Camera.main.transform.position.z);
		//Camera.main.transform.position = new Vector3(Camera.main.transform.position.x,  transform.position.y,Camera.main.transform.position.z);
	}
}
