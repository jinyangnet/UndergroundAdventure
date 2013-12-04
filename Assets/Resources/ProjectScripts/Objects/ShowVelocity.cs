    using UnityEngine;  
    using System.Collections;  
      
    public class ShowVelocity : MonoBehaviour {  
      
        // Use this for initialization  
        void Start () {  
          
        }  
          
        // Update is called once per frame  
        void Update1 () {  
            Debug.Log(gameObject.rigidbody.velocity.ToString() 
			+ gameObject.rigidbody.angularVelocity.ToString() 
			+ gameObject.rigidbody.drag.ToString()
			);  
        }
	
	
	
	 void OnCollisionEnterooo( Collision obj)
        {

           Debug.Log(obj.gameObject.name);
		
		Debug.Log("velocity="+gameObject.rigidbody.velocity.ToString() 
			+"angularVelocity="+ gameObject.rigidbody.angularVelocity.ToString() 
			+ "drag="+gameObject.rigidbody.drag.ToString()
			);
		

        }
	
	
	
	
	
	
	
    }  