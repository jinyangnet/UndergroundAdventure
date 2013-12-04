using UnityEngine;
using System.Collections;

public class LittleState : MonoBehaviour {
	
	private bool _isActivation ;
	
	// Use this for initialization
	void Start () {
		//_isActivation = true ;
	}
	
	public bool IsActivation 
	{
	     get { return _isActivation ; }
	     set { _isActivation = value; }
	}
	
}
