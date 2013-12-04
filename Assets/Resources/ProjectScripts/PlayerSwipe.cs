using UnityEngine;
using System.Collections;

	public class PlayerSwipe : MonoBehaviour {

	private TextMesh textMesh;
	private GameObject trail;
	private GameObject _Player ;
	
	// Subscribe to events
	void OnEnable(){
		EasyTouch.On_SwipeStart += On_SwipeStart;
		EasyTouch.On_Swipe += On_Swipe;
		EasyTouch.On_SwipeEnd += On_SwipeEnd;		
	}

	void OnDisable(){
		UnsubscribeEvent();
		
	}
	
	void OnDestroy(){
		UnsubscribeEvent();
	}
	
	void UnsubscribeEvent(){
		EasyTouch.On_SwipeStart -= On_SwipeStart;
		EasyTouch.On_Swipe -= On_Swipe;
		EasyTouch.On_SwipeEnd -= On_SwipeEnd;	
	}
	
	void Start(){
		textMesh = GameObject.Find("LastSwipeText").transform.gameObject.GetComponent("TextMesh") as TextMesh;
		_Player = GameObject.Find ("clicken") ;
	}
	
	// At the swipe beginning 
	private void On_SwipeStart( Gesture gesture){
		
		// Only for the first finger
		if (gesture.fingerIndex==0 && trail==null){ 
			
			// the world coordinate from touch for z=5
			Vector3 position = gesture.GetTouchToWordlPoint(5);
			trail = Instantiate( Resources.Load("Trail"),position,Quaternion.identity) as GameObject;
		}
	}
	
	// During the swipe
	private void On_Swipe(Gesture gesture){
		
		if (trail!=null){
			
			// the world coordinate from touch for z=5
			Vector3 position = gesture.GetTouchToWordlPoint(5);
			trail.transform.position = position;
		}
	}
	
	// At the swipe end 
	private void On_SwipeEnd(Gesture gesture){
		
		if (trail!=null){
			Destroy(trail);
			
			// Get the swipe angle
			float angles = gesture.GetSwipeOrDragAngle();
			string s = "Last swipe : " + gesture.swipe.ToString() + " /  vector : " + gesture.swipeVector.normalized + " / angle : " + angles.ToString("f2") + " / " + gesture.deltaPosition.x.ToString("f5");
		
			string f =  gesture.swipe.ToString().ToLower();
			_Player = GameObject.Find ("clicken") ;
			if ( _Player ) 
			{
				print ("rrrrrrrrr") ;
				if (  f == "right" )
					_Player.rigidbody.AddForce( new Vector3(6.0f,8.0f,0.0f),ForceMode.Impulse) ;
				else
					_Player.rigidbody.AddForce( new Vector3(-2.0f,0.60f,0.0f),ForceMode.Impulse) ;
			}
			print ( s ) ;
		}
				
	}
	

	
	
}
