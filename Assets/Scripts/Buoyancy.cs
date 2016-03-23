using UnityEngine;
using System.Collections;

public class Buoyancy : MonoBehaviour {
	public bool debug = false;
	public float buoyancy = 8;

	private float currentForce = 0;
	private int layerMask = 1 << 4; //Waterlayer
	private bool inWater;
	
	private Rigidbody parentRigidbody;

	private float timeStamp;
	// Use this for initialization
	void Start () {
		inWater = false;
		if(transform.parent != null) parentRigidbody = transform.parent.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		inWater = checkWater();
		Debug.Log(currentForce);
		
		if(currentForce <= buoyancy)
			currentForce = buoyancy*Mathf.Pow((Time.time-timeStamp),2);

		if(parentRigidbody != null)
			if(inWater){
				parentRigidbody.AddForceAtPosition(Vector3.up * currentForce, transform.position);
			}
	}

	public bool checkWater(){
		if(debug == true) Debug.DrawRay (transform.position, Vector3.up*1000, Color.white);
		
		if(Physics.Raycast(transform.position, Vector3.up, Mathf.Infinity, layerMask)){
			if(!inWater){
				timeStamp = Time.time;
			}
			return true;
		} else {
			return false;
		}
	}
}