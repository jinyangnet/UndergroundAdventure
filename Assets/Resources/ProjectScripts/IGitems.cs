using UnityEngine;
using System.Collections;
using System.Collections.Generic ;

public class ItemsType 
{
	private string _tag ;
	private int _count ;
	private Vector3 _position ;
	public string Tag
	{
		get { return _tag; }
		set { _tag = value; }
	}
	
	public int Count
	{
		get { return _count; }
		set { _count = value; }
	}

}

public class IGitems : MonoBehaviour {

	//Dictionary<Vector3
	Vector3[] _box8Pos = new Vector3[5];
	float _gridWidth = 0.1666667f ;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// box8
	void SetGlareBowArea()
	{
		GameObject _player = GameObject.FindWithTag ("Player");
		Vector3 _playerPos = _player.transform.position;
		for (int i = -2; i<=2; i++) {
				Vector3 _pos1 = new Vector3 (_playerPos.x + _gridWidth * i, _playerPos.y +  _gridWidth * 2 , _playerPos.z);
				_box8Pos [i+2] = _pos1;
		}

		//transform.childCount
	}

	public void ItemPower( )
	{
	
		// SendMessage ("ItemPack");
		Camera.main.SendMessage ( "ItemPack" );
		IGManager.gm.ItemUsed (tag);
		//transform.parent.SendMessage ( "ItemPack" );
		string _tag = tag.Replace ("_jewel","");
		GameObject _player = GameObject.FindWithTag ("Player");


		//
		if (_tag == "box7") {
			Vector3 _vecDown =  _player.transform.TransformDirection( Vector3.down );
			RaycastHit[]  rh = Physics.RaycastAll( _player.transform.position, _vecDown,1.0f ) ;//,  _ray,  100.0f  ) ;
			for( int i = 0 ; i <rh.Length ; i ++ )
			{
				//IGManager.gm.PlayDestroyBoxEffect(rh[i].collider.transform ,0.0f) ; 

				DestoryGameObject _do = new DestoryGameObject() ; //new DestoryObject();
				_do.DestoryObject =  rh[i].collider.gameObject ;
				_do.RemainTime =  i + 6 ;//* 0.01f ;
				_do.Position = rh[i].collider.transform.position ;
				IGManager.gm.AddDestoryGameObject ( _do  ) ;
			}
			return ;
		}

		// 0.16666
		if ( _tag == "box8" )
		{
			SetGlareBowArea();
			Vector3 _vecDown =  _player.transform.TransformDirection( Vector3.down );

			for ( int i = 0 ;i < _box8Pos.Length ; i ++ )
			{
				RaycastHit[]  rh = Physics.RaycastAll( _box8Pos[i] , _vecDown, 0.1666667f *4 ) ;//,  _ray,  100.0f  ) ;
				//print ( " _pos1 = " + _pos1 +" rh.Length = "+ rh.Length );
				for( int j = 0 ; j <rh.Length ; j ++ )
				{
					//IGManager.gm.PlayDestroyBoxEffect(rh[i].collider.transform ,0.0f) ;
					DestoryGameObject _do = new DestoryGameObject() ; //new DestoryObject();
					_do.DestoryObject =  rh[j].collider.gameObject ;

					BoxTest bt = rh[j].collider.gameObject.GetComponent<BoxTest>();
					bt.enabled = false ;
					//rh[j].collider.gameObject.tag = "jewel" ; //collider.enabled = false ;
					rh[j].collider.gameObject.transform.parent = null ;

					_do.RemainTime =  j  ;//* 0.01f ;
					_do.Position = rh[j].collider.transform.position ;
					IGManager.gm.AddDestoryGameObject ( _do ) ;
				}
			}
			return ;
		}

		// 
		if (_tag == "box9") {

			IGManager.gm.JewelPowerEnergy() ;
		
			// CFXM3_ResurrectionLight_Circle
			return ;
		}

		// 

		GameObject[] _items = GameObject.FindGameObjectsWithTag( _tag );
		print ( " sender.tag = " + tag + "--" + _items.Length );

		int _time = 0 ; //0.20f ;
		//int i = 1;
		for (int i = 0 ;i<  _items.Length ; i ++ )
		{
			//Destroy(_items[i],_time * i) ;
			DestoryGameObject _do = new DestoryGameObject() ; //new DestoryObject();
			_do.DestoryObject =  _items[i] ;
			_do.RemainTime = _time  + i  ;//* 0.01f ;
			_do.Position = _items[i].transform.position ;
			IGManager.gm.AddDestoryGameObject ( _do ) ;//.PlayDestroyBoxEffect( _items[i].transform,0.0f );
			//i++ ;
		}
		
	}


}
