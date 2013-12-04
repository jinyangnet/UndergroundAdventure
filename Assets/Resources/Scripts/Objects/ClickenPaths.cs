using UnityEngine;
using System.Collections;
using Vectrosity;

public class ClickenPaths : MonoBehaviour {
	
	public int         maxPoints = 500;
	public bool continuousUpdate = true;
	public Rigidbody ballPrefab ;
	public float force = 16.0f;
	
	private Material lineMaterial ;
	private VectorLine pathLine ;
	private int pathIndex = 0;
	private Vector3[] pathPoints ;
	
	// static void SetCamera3D (camera : Camera = Camera.main) : void
	// Use this for initialization
	void Start ()
	{
		Vectrosity.VectorLine.SetCamera3D();
	    
		// SetCamera3D();
		// Vector.SetCamera3D(); 
			
	
		
		//pathLine.
		// pathLine.
		// GameObject ball = Instantiate(ballPrefab, Vector3(-2.25f, -4.4f, -1.9f), Quaternion.Euler(300.0f, 70.0f, 310.0f)) as Rigidbody;
		// ball.useGravity = true;
		// ball.AddForce (ball.transform.forward * force, ForceMode.Impulse);
		
		StartCoroutine(SamplePoints() );
		//SamplePoints ( gameObject.transform ) ;
	}
	
	IEnumerator SamplePoints() // (Transform thisTransform  ) 
	{
		bool running = true;
		while (running) 
		{
			if( IGState.IsGameOver ) 
			{
				running = false;
				break;
			}
			
			if ( IGState.MoveDirection == MoveDirection.Static )
			{
				yield return new WaitForSeconds(0.05f);
				//Debug.Log( "GameState.MoveDirection == MovementDirection.Static " );
			}
			else
			{
				pathPoints[pathIndex] = gameObject.transform.position ;  
				if( pathIndex == 499 )
				{
					pathIndex = 0 ;
				}
				if (++pathIndex == maxPoints) {
					//pathIndex = 0 ;
					running = false;
				}
				yield return new WaitForSeconds(0.05f);
				
				DrawPath() ;
				
				//Debug.Log( string.Format( " DrawPath() pathIndex ={0},GameState.MoveDirection ={1} " ,pathIndex, GameState.MoveDirection  ) );
				
			}
		}
	}
	
	public void CleanPaths()
	{
		Vector3 zero = new Vector3(0f,0f,0f);
		for(int i = 0 ; i < maxPoints;i++)
		{
			pathPoints[i] = zero ;
		}
		//ClickenPaths
		pathLine.Draw();
		pathLine.SetTextureScale (0.01f);
	}
	
	void DrawPath () 
	{
		//print( " DrawPath () " );
		
		if (pathIndex < 1) return ;
		
		pathLine.maxDrawIndex = pathIndex-1 ;
		//pathLine.lineWidth = 6 ;
		pathLine.Draw();
		pathLine.SetTextureScale (1.0f);
	}
	
	
	void OnDestroy()
	{
		//Destroy();
		VectorLine.Destroy(ref pathLine ) ;
	}
	
	
}










