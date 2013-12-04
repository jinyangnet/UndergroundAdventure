using UnityEngine;
using System.Collections;

public class btnLevelItem : MonoBehaviour {
	public int levelItemIndex ;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnClick()
	{
		//print ("btnLevelItem void OnClick()  ") ;
		//print (transform.name) ;
		
		string tfName = transform.name;
		if( string.IsNullOrEmpty(tfName) )
		{
		}
		else
		{
			tfName =tfName.Replace("level","");
		}
		
		Debug.Log( "OnClick() this level index = " + tfName );
		
		switch(tfName)
		{
			
		case "0":
			IGState.InitLevel (0);
			break;
		
		case "1":
			IGState.InitLevel (1);
			break;
		
		case "2":
			IGState.InitLevel (2);
			break;
		
		case "3":
			IGState.InitLevel (3);
			break;
		
		case "4":
			IGState.InitLevel (4);
			break;
			
		case "5":
			IGState.InitLevel (5);
			break;
		
		case "6":
			IGState.InitLevel (6);
			break;
		
		case "7":
			IGState.InitLevel (7);
			break;
		
		case "8":
			IGState.InitLevel (8);
			break;
		
		case "9":
			IGState.InitLevel (9);
			break;
		
		case "10":
			IGState.InitLevel (10);
			break;
		
		case "11":
			IGState.InitLevel (11);
			break;
		
		case "12":
			IGState.InitLevel (12);
			break;
		
		case "13":
			IGState.InitLevel (13);
			break;
		
		case "14":
			IGState.InitLevel (14);
			break;
		
		case "15":
			IGState.InitLevel (15);
			break;
		
		case "16":
			IGState.InitLevel (16);
			break;
		
		case "17":
			IGState.InitLevel (17);
			break;
		
		case "18":
			IGState.InitLevel (18);
			break;
		
		case "19":
			IGState.InitLevel (19);
			break;
		
		case "20":
			IGState.InitLevel (20);
			break;
		
		case "21":
			IGState.InitLevel (21);
			break;
		
		case "22":
			IGState.InitLevel (22);
			break;
		
		case "23":
			IGState.InitLevel (23);
			break;
		
		case "24":
			IGState.InitLevel (24);
			break;
		
		case "25":
			IGState.InitLevel (25);
			break;
		
		case "26":
			IGState.InitLevel (26);
			break;
			
		case "27":
			IGState.InitLevel (27);
			break;
			
		case "28":
			IGState.InitLevel (28);
			break;
			
		case "29":
			IGState.InitLevel (29);
			break;
			
		}
		
		if(tfName=="0")
		{
		}
		
		IGState.IsGameOver =false ;
		IGState.LaunchGameViewSence();
		
		//Application.LoadLevel(0); //.loadedLevel();
	}
	
	
}



	//GameObject a = GameObject.Find("UISprite0");
	//UISprite b = NGUITools.AddSprite(a,at,"Highlight - Thin");
	//b.MakePixelPerfect();
	
    //GameObject parent = GameObject.Find("");
    //UIAtlas atlas = (UIAtlas)Resources.Load("RESOURCES/Images/UI/UIAtlas.prefab",typeof(UIAtlas));
	
	/*	
    UIAtlas atlas = Resources.Load("Assets/Resources/Images/ui/UIAtlas", typeof(UIAtlas)) as UIAtlas; //Assets/Resources/Images/ui/
	//atlas.spriteList[0];
    //UISprite sprite = GameObject.Find("UISprite0");  //NGUITools.AddSprite(parent,atlas,"");  
	GameObject _object = GameObject.Find("UISprite0");
	UISprite _uisprite = _object.GetComponent<UISprite>() as UISprite;
	
	print("atlas=" + atlas ) ;
		
	_uisprite.atlas = atlas ;
		
	//UIAtlas.Sprite _sprite = atlas.GetSprite("ui_level_star3"); 
	//_uisprite.spriteName = "ui_level_star3" ; //.sprite = _sprite ;
		
	_uisprite.pivot = UIWidget.Pivot.Center ;
    _uisprite.MakePixelPerfect();
    */
		
		
   //Page0
		
    //GameObject page = GameObject.Find("Page0");  
    //UISprite b = NGUITools.AddSprite(page,at,"Highlight - Thin");  
	//GameObject levelprefab = (GameObject)Instantiate(particlesStarEffects);
	//NGUITools.AddChild(page, starPrefab ) ;
    //b.MakePixelPerfect();