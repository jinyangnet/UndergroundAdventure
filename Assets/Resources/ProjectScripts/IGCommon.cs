using UnityEngine;
using System.Collections;

public enum MoveDirection { Static,Left, Right, Up, Down } ;

public enum PlayerState    { normal, ice, fire };
public enum ChildrenState  { normal, freezing };
public enum PlayingStatus   { Normal, Playing,Pause,GameOver,bomb,bombLocked,thefifthelement,bombLockedthefifthelement };

public enum BoxType   {box, box1, box2,box3,box4,box5,box6,box7,box8,jewel,
	box1jewel,box2jewel ,box3jewel,box4jewel,box5jewel,box6jewel,box7jewel,box8jewel,
	CoinSmallBronze,CoinSmallGold};



// zhengchang
// zhadan
// zhadan suozhu
// thefifthelement

public class IGMaps //: MonoBehaviour
{
	private Vector3 _objectPosion ;
	private Vector2 _objectPoint ;
	
	private int _x ; 
	private int _y ;
	private GameObject _objectPrefab ;
	private Rect _rect ;
	
    public IGMaps()
    {
    }
	
	public  Rect rect
    {
        get { return _rect; }
        set { _rect = value; }
    }
	
    public int x
    {
        get { return _x; }
        set { _x = value; }
    }
	
	public int y
    {
        get { return _y; }
        set { _y = value; }
    }
	
	public GameObject prefab
    {
        get { return _objectPrefab; }
        set { _objectPrefab = value; }
    }
	
	public Vector3 position
    {
        get { return _objectPosion  ; }
        set { _objectPosion = value ; }
    }
	
	public Vector2 point
    {
        get { return _objectPoint  ; }
        set { _objectPoint = value ; }
    }
}

public class IGScores //: MonoBehaviour
{
	private int _HP ;
	private int _OriginalHP ;
	private int _GoldCoin ; 

	//private int _y ;
	//private GameObject _objectPrefab ;
	//private Rect _rect ;
	
	public IGScores()
	{
	}
	
	public int GoldCoin
	{
		get { return _GoldCoin; }
		set { _GoldCoin = value; }
	}

	public int HP
	{
		get { return _HP  ; }
		set { _HP = value ; }
	}
	
	public int OriginalHP
	{
		get { return _OriginalHP  ; }
		set { _OriginalHP = value ; }
	}
}


public class IGCommon : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
