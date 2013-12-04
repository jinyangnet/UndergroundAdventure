using UnityEngine;
using System.Collections;

public class IGItemPack : MonoBehaviour {
	bool _isMove = false ;
	// Use this for initialization
	GameObject items ;//= GameObject.Find("items");
	Vector3 target =new Vector3( 0.0f,2.0f,0.5f ) ;
	private Vector2 velocity ;
	//private  Dictionary<GameObject,string> _jewelItemsList 	= new Dictionary<GameObject,string>() ;

/*
	GameObject _jewel1 ;
	GameObject _jewel2 ;
	GameObject _jewel3 ;
	GameObject _jewel4 ;
	GameObject _jewel5 ;
	GameObject _jewel6 ;
	GameObject _jewel7 ;
	GameObject _jewel8 ;
	GameObject _jewel9 ;
	GameObject _jewel10 ;
*/

	void Start () {
	
		 items = GameObject.Find("items");
	}
	
	// Update is called once per frame
	void Update () {

		float smoothTime= 0.3f;	//Smooth Time
		items.transform.localPosition = new Vector3(items.transform.localPosition.x,  
		                                            Mathf.SmoothDamp( items.transform.localPosition.y,target.y, ref velocity.y, smoothTime),
		                                       items.transform.localPosition.z);
		//Camera.main.
	
	}

	// _jewelItemsList



	public void ItemPack()
	{ 
		// target
		if ( target.y == 0.0f ){ 
			target = new Vector3(0.0f,2.0f,0.5f) ; 
			IGState.Status = PlayingStatus.Playing; 
		}
		else{target = new Vector3(0.0f,0.0f,0.5f); 
			IGState.Status = PlayingStatus.Pause; 
			IGManager.gm.ItemsShow ();
		}


		return ;
		//if ( _isMove ) return ;

		//GameObject items = GameObject.Find("items");
		if ( items.transform.localPosition.y == 0.0f )
		{
			items.transform.localPosition = new Vector3(0.0f,2.0f,0.5f);
		}
		else
		{
			items.transform.localPosition = new Vector3(0.0f,0.0f,0.5f);
		}

			
		print("------------------");
	}




}
