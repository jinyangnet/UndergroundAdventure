using UnityEngine;
using System.Collections;

public class Vector3Direction
{
	private Vector3 _vecDirection ;
	private float _length ;
	
	public Vector3Direction( Vector3 vecDirection,float length )
	{
		_vecDirection  = vecDirection ;
		_length 			 = length ;
	}
	
	public  float  Length
	{
	     get { return _length ; }
	     set { _length = value; }
	}
	
	public Vector3 VecDirection 
	{
	     get { return _vecDirection ; }
	     set { _vecDirection = value; }
	}
	
}

/*
public class IGUtilThird  
{
	private static IGUtilThird instance = null ;

	public static IGUtilThird Instance {
		get {
			if (instance == null || ((IGUtilThird)instance) == null) {
					instance = (IGUtilThird)Object.FindObjectOfType (typeof(IGUtilThird));
					return instance; 
				}

			return instance; 
			}


		}
}
*/

public class IGUtil : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	
	
	/// <summary>
	/// Computs the circle area.
	/// </summary>
	/// <returns>
	/// The circle area.
	/// </returns>
	/// <param name='radius'>
	/// Radius.
	/// </param>
	public static float ComputCircleArea(float radius)
    {
        return (IGConfig.PI * (radius * radius));
    }
	
	/// <summary>
	/// Computs the rectangle area.
	/// </summary>
	/// <returns>
	/// The rectangle area.
	/// </returns>
	/// <param name='width'>
	/// Width.
	/// </param>
	/// <param name='height'>
	/// Height.
	/// </param>
	public static float ComputRectangleArea(float width,float  height)
    {
        return  (width * height);
    }
	
	/// <summary>
	/// Computs the triangle area.
	/// </summary>
	/// <returns>
	/// The triangle area.
	/// </returns>
	/// <param name='width'>
	/// Width.
	/// </param>
	/// <param name='height'>
	/// Height.
	/// </param>
	public static float ComputTriangleArea(float width,float  height)
    {
        return  (width * height)/2;
    }
	
	
	public static float GetDistance(Vector2 p1, Vector2 p2)
	{
	    return (float)System.Math.Sqrt(System.Math.Pow(System.Math.Max(p1.x, p2.x) - System.Math.Min(p1.x, p2.x), 2) + System.Math.Pow(System.Math.Max(p1.y, p2.y) - System.Math.Min(p1.y, p2.y), 2));
	}
	
	public static float GetDistance(Vector3 posion1, Vector3 posion2)
	{
		Vector3 p1 =  Camera.main.WorldToScreenPoint( posion1 ) ;
		Vector3 p2 =  Camera.main.WorldToScreenPoint( posion2 ) ;
		
	    return (float)System.Math.Sqrt(System.Math.Pow(System.Math.Max(p1.x, p2.x) - System.Math.Min(p1.x, p2.x), 2) + System.Math.Pow(System.Math.Max(p1.y, p2.y) - System.Math.Min(p1.y, p2.y), 2));
	}
	
}
