
using UnityEngine;
using System.Collections;

public class DragRigidbodyCS : MonoBehaviour {
    
 	public float smoothTime= 0.3f;	//Smooth Time
	private Vector2 velocity;		//Velocity
	
public float spring = 50.0f;
public float damper = 5.0f;
public float drag = 10.0f;
public  float angularDrag = 5.0f;
public  float distance = 0.2f;
public  bool attachToCenterOfMass = false;
private SpringJoint springJoint ;
	
private Rigidbody  editingRigidbody ;
private	Transform  editingTransform ;
//private	Transform  editingTransform ;

public Transform newTransform ;
	
void Awake ()
{
// Make the game run as fast as possible in the web player
//Application.targetFrameRate = 300;
		//newTransform= transform ;
}
	
void Update ()
{
		
	//transform.position = new Vector3(transform.position.x,  Mathf.SmoothDamp( transform.position.y, transform.position.y-0.06f, ref velocity.y, smoothTime),transform.position.z);
    // Make sure the user pressed the mouse down
    if (!Input.GetMouseButtonDown (0))
        return;

    Camera mainCamera = FindCamera();
        
    // We need to actually hit an object
    RaycastHit hit ;
    if (!Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit, 100))
        return;
	
		
		
		return ;
		
	if( hit.transform.parent==null )
	{
		//MeshRenderer mr = hit.transform.GetComponent("MeshRenderer") as MeshRenderer ;
		hit.transform.renderer.enabled = false ;
		Destroy ( hit.transform.gameObject,0.0f ) ;
	}
	else
	{
		//MeshRenderer mr = hit.transform.GetComponent("MeshRenderer") as MeshRenderer ;
		//mr.isVisible = false ;	
		
		hit.transform.renderer.enabled = false ;
		Destroy ( hit.transform.parent.gameObject ) ;
	}
	return ;
		
	//print( "DragRigidbodyCS" );
		
    // We need to hit a rigidbody that is not kinematic
    if (!hit.rigidbody || hit.rigidbody.isKinematic)
        return;
	
	
	/*
	newTransform.position = new Vector3(  hit.transform.position.x,hit.transform.position.y+0.1f,2.0f) ;
	TweenTransform ttf   = hit.transform.GetComponent<TweenTransform>() as TweenTransform ;
	ttf.Reset();
	ttf.method = UITweener.Method.EaseIn;
	ttf.eventReceiver    = gameObject ;
	//ttf.callWhenFinished = "moveEffect";
	ttf.from = 	hit.transform ;
	ttf.to   =  newTransform ;
	ttf.duration = 0.50f ;
	ttf.Play( true ) ;
	return ;
	*/
		

	

	
	
		
	// hit.transform.gameObject

	// Rigidbody
	// hit.rigidbody
	//hit.transform
	editingTransform = hit.transform ;
	editingRigidbody = hit.rigidbody ;
	editingRigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY  |  RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionZ ;

		
	print( "DragRigidbodyCS if (!hit.rigidbody || hit.rigidbody.isKinematic)" );
    
    if (!springJoint)
    {
        GameObject go = new GameObject("Rigidbody dragger");
        Rigidbody body  = go.AddComponent ("Rigidbody") as Rigidbody;
        springJoint = go.AddComponent ("SpringJoint") as SpringJoint;
        body.isKinematic = true;
    }
    print(springJoint.transform.position);
    springJoint.transform.position = hit.point;
    if (attachToCenterOfMass)
    {
        Vector3 anchor = transform.TransformDirection(hit.rigidbody.centerOfMass) + hit.rigidbody.transform.position;
        anchor = springJoint.transform.InverseTransformPoint(anchor);
        springJoint.anchor = anchor;
    }
    else
    {
        springJoint.anchor = Vector3.zero;
    }
    
    springJoint.spring = spring;
    springJoint.damper = damper;
    springJoint.maxDistance = distance;
    springJoint.connectedBody = hit.rigidbody;
    
    //DragObject(hit.distance);
    StartCoroutine ("DragObject", hit.distance);
}
    
public IEnumerator DragObject (float distance )
{
    //print(distance); 
        
    //int i = 0 ;
    float oldDrag = springJoint.connectedBody.drag;
    float oldAngularDrag = springJoint.connectedBody.angularDrag;
    springJoint.connectedBody.drag = drag;
    springJoint.connectedBody.angularDrag = angularDrag;
    Camera mainCamera = FindCamera();
        
    //TouchPhase.Moved
    while (Input.GetMouseButton (0))
    {
        //i++ ;
        //print(i);
        Ray ray = mainCamera.ScreenPointToRay (Input.mousePosition);
        springJoint.transform.position = ray.GetPoint(distance);
        yield return 0;// WaitForSeconds(3.0f);
    }
        
    //return;
    if (springJoint.connectedBody)
    {
        springJoint.connectedBody.drag = oldDrag;
        springJoint.connectedBody.angularDrag = oldAngularDrag;
        springJoint.connectedBody = null;
		
		////////////////////////////////////////////////////////
		Vector3 screenPoint =  Camera.main.WorldToScreenPoint( editingTransform.position ) ;
		if(screenPoint.x > Screen.width || screenPoint.x < 0 )
		{
			 Destroy ( editingTransform.gameObject );
			// return ;
		}
		else
		{
			int _x = System.Convert.ToInt32( System.Convert.ToString( screenPoint.x / IGState._gridWidth ).Split('.')[0] ) ;
			int _y = System.Convert.ToInt32( System.Convert.ToString( (768- screenPoint.y ) / IGState._gridWidth ).Split('.')[0] ) ;
			
				if(_x >=21 || _y >=16 || _x < 0 || _y < 0 )
			{
				Destroy ( editingTransform.gameObject );
			}
			else
			{
				Debug.Log( string.Format( "_x={0},_y={1} ---  {2},{3}",_x,_y ,screenPoint.x,screenPoint.y) ) ;
				editingTransform.position  =IGState.Maps[_x,_y].position  ;
				////////////////////////////////////////////////////////
				editingRigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY  | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY |  RigidbodyConstraints.FreezePositionZ ;
			
			}
	
		}
    }
}

Camera FindCamera ()
{
    if (camera)
        return camera;
    else
        return Camera.main;
}
    
    
}
    
