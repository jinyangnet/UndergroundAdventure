using UnityEngine;
using System.Collections;

public class btnPauseShow : MonoBehaviour {
	
	public  GameObject gamePausePanel ;
	public  Transform _transfromPanel_from ;
	public  Transform _transfromPanel_to ;
	
	public bool isClick;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnClick()
	{
		print("onclick....");
		
		{
			isClick = false ;
			
	
			
		}
		
		
	}
}
