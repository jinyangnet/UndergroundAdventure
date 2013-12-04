using UnityEngine;
using System.Collections;

public class DragRigidbody : MonoBehaviour {
    
    
public float spring = 50.0f;
public float damper = 5.0f;
public float drag = 10.0f;
public  float angularDrag = 5.0f;
public  float distance = 0.2f;
public  bool attachToCenterOfMass = false;	
float SliderScaleVaule=1.0f;
private SpringJoint springJoint ;
   
public GameObject particle;
public GameObject bloodparticle;
	
public GameObject particleZoomIn;
public GameObject particleZoomOut;

void Awake ()
{
	// Make the game run as fast as possible in the web player
	//Application.targetFrameRate = 300;
}

	void OnMouseDown()
	{
		RaycastHit hit = new RaycastHit();
		if(this.collider.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 9999f))
		{
			GameObject particle = spawnParticle();
			particle.transform.position = hit.point + particle.transform.position;
		}
	}
	
	private GameObject spawnParticle()
	{
		GameObject particles = (GameObject)Instantiate(particleZoomOut);
		//float Y = 0.0f;
		//particles.transform.localScale =new Vector3(0,0,0);
		particles.transform.position = new Vector3(0,0,0);
		
		return particles;
	}
	
	
	
void Update ()
{
    // Make sure the user pressed the mouse down
    if (!Input.GetMouseButtonDown (0))
        return;
    Camera mainCamera = FindCamera();
	GameObject o = GameObject.Find("triangle_wood_a");
	if(o){
		//SliderScaleVaule=o.transform.localScale.x;
		//SliderScaleVaule=SliderScaleVaule+0.1f ;
		//o.transform.localScale =new Vector3(SliderScaleVaule,SliderScaleVaule,SliderScaleVaule); 
	}
		
    // We need to actually hit an object
    RaycastHit hit ;
	
    if (!Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit, 600))
        return;
		
    // We need to hit a rigidbody that is not kinematic
    if (!hit.rigidbody || hit.rigidbody.isKinematic)
        return;
	
		GameObject particle ;//= (GameObject)Instantiate(particleZoomOut);
		if(hit.transform.name.StartsWith("wood"))
		{
		  particle = (GameObject)Instantiate(particleZoomOut);
		}
		else{
		 particle = (GameObject)Instantiate(particleZoomIn);
		}
		particle.transform.position = new Vector3(0,0,5);
			//spawnParticle();
		particle.transform.localScale =new Vector3 (0.2f,0.2f,0.2f);
		particle.transform.position = new Vector3(hit.transform.localPosition.x,hit.transform.localPosition.y,5) ; //.point + particle.transform.position;
		
		print("222");
		
		print(hit.transform.name);
		hit.transform.rigidbody.WakeUp() ;//=false;
		//float xSize=hit.transform.GetComponent<MeshFilter>().mesh.bounds.size.x*hit.transform.localScale.x;
		
		print("///------"+ hit.collider.bounds.size.y ) ;
		//print(xSize ) ; //transform.localPosition);
		
		Vector3 screenpos=mainCamera.WorldToScreenPoint(hit.transform.position);
		
		print(screenpos);
		SliderScaleVaule=hit.transform.localScale.x;
		if(IGState.IsZoonOut)
		{
			SliderScaleVaule=SliderScaleVaule-0.05f ;
		}
		else
		{
			SliderScaleVaule=SliderScaleVaule+0.05f ;
		}

		
		hit.transform.localScale =new Vector3(SliderScaleVaule,SliderScaleVaule,SliderScaleVaule); 
  
		tk2dAnimatedSprite _sprite = hit.transform.GetComponent<tk2dAnimatedSprite>() ;
		if(_sprite)
		{
			_sprite.Play();
			//_sprite.playAutomatically= true ;
			//_sprite.Play("Clip0");
			//print("_sprite.Play();");
		}
		
		
		Vector3 screenPos = mainCamera.WorldToScreenPoint (hit.transform.position);  
		print ("target is " + screenPos.x + " pixels from the left");  
			
	//return;
		
		
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
        print(distance); 
        
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