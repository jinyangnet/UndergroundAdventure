using UnityEngine;
using System.Collections;

/*
public class LevelMode : MonoBehaviour {
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}
}
*/


//CREATE TABLE "GAMELEVEL" 
//("id" INTEGER PRIMARY KEY ,"pic" TEXT,"score" INT,"sence" INT,"starCount" INT,"isLocked" BOOL,"createDate" DATETIME)
	
    public class IGLevelMode
    {
		private int  _id ;
		private string _pic;
		private int _score;
		private int _sence ;
		private int _starCount;
		private bool _isLocked ;
		private System.DateTime _createDate;

        public IGLevelMode()
        {
        }

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public string  pic
        {
            get { return _pic; }
            set { _pic = value; }
        }

        public int score
        {
            get { return _score; }
            set { _score = value; }
        }

        public int sence
        {
            get { return _sence; }
            set { _sence = value; }
        }

        public int starCount
        {
            get { return _starCount; }
            set { _starCount = value; }
        }

	  //public bool _isLocked
	  public bool isLocked
      {
          get { return _isLocked; }
          set { _isLocked = value; }
      }
	
    }
