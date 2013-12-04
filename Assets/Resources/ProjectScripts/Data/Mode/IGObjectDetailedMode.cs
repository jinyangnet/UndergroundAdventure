
//CREATE TABLE "GAMELEVELDETAILED" 
//("id" INTEGER PRIMARY KEY ,"prefabName" TEXT,"levelID" INT,"bodyType" INT,"transformZ" FLOAT,"transformX" FLOAT,"transformY" FLOAT,
//"rotateX" FLOAT,"rotateY" FLOAT,"rotateZ" FLOAT,"scaleX" FLOAT,"scaleY" FLOAT,"scaleZ" FLOAT,"createDate" DateTime)

using UnityEngine;
using System.Collections;

    public class IGObjectDetailedMode
    {
    
	private int  _id ;
	private string _prefabName;
	private int _levelID;
	private int _bodyType ;
	
	private float _transformX;
	private float _transformY;
	private float _transformZ;
	
	private float _rotateX;
	private float _rotateY;
	private float _rotateZ;
	private float _rotateW;
	
	private float _scaleX;
	private float _scaleY;
	private float _scaleZ;
	
	private System.DateTime _createDate;

     public IGObjectDetailedMode()
     {
     }
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public string  prefabName
        {
            get { return _prefabName; }
            set { _prefabName = value; }
        }

        public int levelID
        {
            get { return _levelID; }
            set { _levelID = value; }
        }

        public int sence
        {
            get { return _bodyType; }
            set { _bodyType = value; }
        }
	
	#region transform
	   public float transformX
       {
            get { return _transformX; }
            set { _transformX = value; }
       }
	
	   public float transformY
       {
            get { return _transformY; }
            set { _transformY = value; }
       }
	
        public float transformZ
        {
            get { return _transformZ; }
            set { _transformZ = value; }
        }
    #endregion
	
	#region rotate
	   public float rotateX
       {
            get { return _rotateX; }
            set { _rotateX = value; }
       }
	
	   public float rotateY
       {
            get { return _rotateY; }
            set { _rotateY = value; }
       }
	
        public float rotateZ
        {
            get { return _rotateZ; }
            set { _rotateZ = value; }
        }
	
	    public float rotateW
        {
            get { return _rotateW; }
            set { _rotateW = value; }
        }
	
	//
    #endregion
	
	
	#region scale
	   public float scaleX
       {
            get { return _scaleX; }
            set { _scaleX = value; }
       }
	
	   public float scaleY
       {
            get { return _scaleY; }
            set { _scaleY = value; }
       }
	
        public float scaleZ
        {
            get { return _scaleZ; }
            set { _scaleZ = value; }
        }
    #endregion
	
	
    }
