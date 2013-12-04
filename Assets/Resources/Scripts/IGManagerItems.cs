using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class IGManager : MonoBehaviour
{
	public void JewelPowerEnergy()
	{
		GameObject _player = GameObject.FindWithTag ("Player");
		string asset = "Prefab/CFXM3_ResurrectionLight_Circle" ;
		Vector3 pos = new Vector3(_player.transform.position.x,_player.transform.position.y -0.1f,1.9f);

		GameObject _go = (GameObject)Instantiate( Resources.Load(asset), pos ,Quaternion.identity ) ;
		_go.transform.parent = _player.transform ;

		_ScoreFlashPushWorld ( _player.transform.position, new Vector2 (0.0f, -30.3f), 20, Color.red ) ;

		PlayerPower (20);


	}

	/// <summary>
	/// Items the used.
	/// </summary>
	/// <param name="tag">Tag.</param>
	public void ItemUsed(string tag )
	{
		if (_jewelItemsList.ContainsKey(tag) ) 
		{
			ItemsType _item = _jewelItemsList[tag] ;
			_item.Count = _item.Count - 1 ;

			print ( tag + " --- " + _item.Count );

			if( _item.Count <= 0 ) _jewelItemsList.Remove(tag);
			//else  _jewelItemsList
		}
	}

	public void ItemsShow()
	{
		// items
		print ( " ItemsShow = "  + _jewelItemsList.Count );
		GameObject _items = GameObject.Find("items");
		if ( _items.transform.childCount>0 )
		{
			foreach (Transform child in _items.transform )
			{
				Destroy(child.gameObject) ;
			}
		}
		
		//print ( " ItemsShow 2 " );
		
		float _x = -0.5f ;
		float _y= 0.30f ;
		float _z= -0.1f;
		float _offset_y = 0.20f ;
		float _offset_x = 0.0f ;
		int i = 0 ;
		foreach( KeyValuePair<string, ItemsType > kv in _jewelItemsList )
		{
			
			string _Key = kv.Key ;
			ItemsType _item = kv.Value ;
			
			print ( " ItemsShow = " + _Key  );
			
			int _index = 1 ;
			if ( _Key == "box1_jewel" ) {_index = 0 ;}
			else if ( _Key == "box2_jewel" ) {_index = 1 ;}
			else if ( _Key == "box3_jewel" ) {_index = 2 ;}
			else if ( _Key == "box4_jewel" ) {_index = 3 ;}
			else if ( _Key == "box5_jewel" ) {_index = 4 ;}
			else if ( _Key == "box6_jewel" ) {_index = 5 ;}
			else if ( _Key == "box7_jewel" ) {_index = 6 ;}
			else if ( _Key == "box8_jewel" ) {_index = 7 ;}
			else if ( _Key == "box9_jewel" ) {_index = 8 ;}
			else { continue ; } ;
			
			// print ( " ItemsShow 3 " );
			
			GameObject go = (GameObject)Instantiate(Resources.Load("Prefab/box_jewel_text"));
			if ( go == null ) continue ;
			
			go.transform.parent = _items.transform ;
			//print ( " ItemsShow 4 " );
			go.tag = _Key ;
			
			tk2dSpriteAnimator anim = go.GetComponent("tk2dSpriteAnimator") as tk2dSpriteAnimator ;
			anim.SetFrame( _index );
			
			tk2dTextMesh tktextMeshHP = go.GetComponentInChildren<tk2dTextMesh>() ;// as tk2dTextMesh ;
			tktextMeshHP.text = string.Format ( "X{0}" , _item.Count ) ;
			tktextMeshHP.Commit();
			
			//if (i++ ==4 )
			{
				go.transform.localPosition = new Vector3(_x + _offset_x,_y - _offset_y * i ,_z ) ;
			}
			
			i++ ;
			
			if ( i ==4 )
			{ 
				i = 0;
				_offset_x = _offset_x +0.5f; 
				_y = 0.30f;
			} ;
		}
		
		//print ( " ItemsShow 5 " );
	}

	public void JewelPower()
	{
		GameObject _player = GameObject.FindWithTag ("Player");
		string asset = "Prefab/CFXM3_ResurrectionLight_Circle" ;
		Vector3 pos = new Vector3(_player.transform.position.x,_player.transform.position.y -0.1f,1.9f);
		Instantiate( Resources.Load(asset), pos ,Quaternion.identity ) ;
	}

	public void PickupItems( GameObject _jewel )
	{
		if ( _jewelItemsList.ContainsKey( _jewel.tag ))
		{
			ItemsType _item = _jewelItemsList[ _jewel.tag ] ; 
			_item.Count ++ ;
			_jewelItemsList.Remove(  _jewel.tag ) ;
			_jewelItemsList.Add ( _jewel.tag,_item ) ;
		}
		else
		{
			ItemsType _item =new ItemsType();
			_item.Count = 1;
			_jewelItemsList.Add( _jewel.tag,_item) ;
		}
		
		print ( " _jewelItemsList = " + _jewelItemsList.Count );
	}

}
