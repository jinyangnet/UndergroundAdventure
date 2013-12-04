using UnityEngine;
using System.Collections;
using Vectrosity ;

public class MakeGridLine : MonoBehaviour {
	
public int gridPixels = 48 ;
private VectorLine gridLine ;
	
	// Use this for initialization
	void Start () {
		gridLine = new VectorLine("Grid", new Vector2[2], null, 1.0f);
		//MakeGrid();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void MakeGrid () 
	{
		Vector2[] gridPoints = new Vector2[((Screen.width/gridPixels + 1) + (Screen.height/gridPixels + 1)) * 2];
		gridLine.Resize (gridPoints);
		
		int index = 0;
		for (int x = 0; x < Screen.width; x += gridPixels) 
		{
			gridPoints[index++] = new  Vector2(x, 0);
			gridPoints[index++] = new Vector2(x, Screen.height-1);
		}
			
		for (int y = 0; y < Screen.height; y += gridPixels) 
		{
			gridPoints[index++] = new  Vector2(0, y);
			gridPoints[index++] = new  Vector2(Screen.width-1, y);
		}
			
		gridLine.Draw();
}
	
}
	
	
	
	
