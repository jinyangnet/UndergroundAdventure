using UnityEngine;
using System.Collections;
public class ObjectsZoon : MonoBehaviour {
	
	//float SliderScaleVaule=1.0f ;
	
	float localScaleX = 1.0f ;
	float localScaleY = 1.0f ;
	float localScaleZ = 1.0f ;
	
	public GameObject particleZoomIn ;
	public GameObject particleZoomOut ;
	public GameObject panelGameOver;

	void Awake()
	{
		/*
	        Object[] objs = FindObjectsOfType(typeof(GameObject));
	        foreach (GameObject go in objs)
	        { 
	            // print("objname: "+go.name +"- position = "+go.transform.position);
			 	// print(  Camera.main.ViewportToScreenPoint(go.transform.position )  )  ;
	        }
	        */
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
		
	return ;
		
	if(IGState.IsGameOver)
		return;
		
    // Make sure the user pressed the mouse down
    if (!Input.GetMouseButtonDown (0))
        return;
    Camera mainCamera = FindCamera();
	
	/*
	Object[] objs = GameObject.FindGameObjectsWithTag("objects") ; //  FindObjectsOfType(typeof(GameObject));
	foreach (GameObject go in objs)
	{  
		
    }
    
    */
		
    RaycastHit hit ;
    if (!Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit, 600))
        return;
		
    // We need to hit a rigidbody that is not kinematic
    if (!hit.rigidbody || hit.rigidbody.isKinematic)
        return;
	
		//print ("2");
	    GameObject particle ;

		localScaleX = hit.transform.localScale.x ;
		localScaleY = hit.transform.localScale.y ;
		localScaleZ = hit.transform.localScale.z ;
		
		if(IGState.IsZoonOut)
		{
			localScaleX = localScaleX - 0.05f ;
			localScaleY = localScaleY - 0.05f ;
			localScaleZ = localScaleZ - 0.05f ;
			
			particle = (GameObject)Instantiate(particleZoomOut);
		}
		else
		{
			localScaleX = localScaleX + 0.05f ;
			localScaleY = localScaleY + 0.05f ;
			localScaleZ = localScaleZ + 0.05f ;
			
			particle = (GameObject)Instantiate(particleZoomIn);
		}
		//////////////////////////////////////////////
		particle.transform.position = new Vector3(hit.transform.localPosition.x,hit.transform.localPosition.y,5) ; //.point + particle.transform.position;
		hit.transform.rigidbody.WakeUp() ;
		//print("///------"+ hit.collider.bounds.size ) ;
		//print ("3 ");
		//Vector3 screenpos=mainCamera.WorldToScreenPoint(hit.transform.position);
		//print(screenpos) ;
		//////////////////////////////////////////////
		hit.transform.localScale =new Vector3(localScaleX,localScaleY,localScaleZ); 
		
		if(localScaleX<0.1  || localScaleY<0.1)
		{
			hit.transform.rigidbody.AddExplosionForce(100.0f,hit.transform.position,0.5f,1f);
			print("hit.transform.rigidbody.AddExplosionForce(100.0f,hit.transform.position,0.5f,1f);");
			
			//Destroy();
		}
		
		
		tk2dAnimatedSprite _sprite = hit.transform.GetComponent<tk2dAnimatedSprite>() ;
		if(_sprite)
		{
			_sprite.Play() ;
		}
		
		//Vector3 screenPos = mainCamera.WorldToScreenPoint (hit.transform.position);  
		//print ("target is " + screenPos.x + " pixels from the left");
		
	 //SQLHelper.insert("momo", new string[]{ "'0000'",  "'112233332'"   ,"'111@gmail.com'"  , "'www.xuanyusong.com'"   });
		//print("i====="+i);
	//**********************************
		//objectscicle
		//objectstriangle
		//objectsrectangle
		
	/*	
	Object[] objs = GameObject.FindGameObjectsWithTag("objectscicle") ; //  FindObjectsOfType(typeof(GameObject));
	foreach (GameObject go in objs)
	{  
	   //float area = ComputCircleArea(go.collider.bounds.size.x);
	   //print("objname: "+go.name +"- size = "+go.collider.bounds.size +"area="+ area ) ;
			
    }
    */
		
	//**********************************

}
    
	



Camera FindCamera ()
{
    if (camera)
        return camera;
    else
        return Camera.main;
}
    
    
}
