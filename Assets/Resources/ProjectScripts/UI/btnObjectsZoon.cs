using UnityEngine;
using System.Collections;

public class btnObjectsZoon : MonoBehaviour {
	
	void OnActivate(bool isActive) 
	{
		//print (isActive);
		IGState.IsZoonOut = isActive ;
	}
}
