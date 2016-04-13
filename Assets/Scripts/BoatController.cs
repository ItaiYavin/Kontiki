using UnityEngine;
using System.Collections;

public class BoatController : MonoBehaviour {
    
    
    [SerializeField]
    private bool isControlling = false;
    
    
    public float forwardSpeed = 3;
    public float backwardSpeed = 1;
    public float turnSpeed = 1;
    
    public float drag = 10;
    
    private Rigidbody body;
    
    private float angle = 0;
    
	// Use this for initializatation
	void Start () {
        
        body = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(isControlling){
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            
            h *= turnSpeed;
            v *= (v>0) ? forwardSpeed : backwardSpeed;
            
         //   body.AddForce(Vector3.forward * v * Time.deltaTime);
           // body.AddTorque(0,h * Time.deltaTime,0);
           
           angle += h * Time.deltaTime;
            
            transform.position += (transform.forward * v * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0,angle,0);
            
        }
	}
    
    public void StartControl(){
        isControlling = true;
    }
    
    public void StopControl(){
        isControlling = false;
    }
}
