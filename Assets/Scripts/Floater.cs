using UnityEngine;
using System.Collections;

public class Floater : MonoBehaviour {
	public float waterLevel, floatHeight;
	public Vector3 buoyancyCentreOffset = Vector3.zero;
	public float bounceDamp;
	
	void Start(){
        float a = 0.01f;
        buoyancyCentreOffset += new Vector3(Random.Range(-a,a),Random.Range(-a,a),Random.Range(-a,a));
    }

	void FixedUpdate () {
		Vector3 actionPoint = transform.position + transform.TransformDirection(buoyancyCentreOffset);
		float forceFactor = 1f - ((actionPoint.y - waterLevel) / floatHeight);
		
		if (forceFactor > 0f) {
			Vector3 uplift = -Physics.gravity * (forceFactor - GetComponent<Rigidbody>().velocity.y * bounceDamp);
			GetComponent<Rigidbody>().AddForceAtPosition(uplift, actionPoint);
		}
	}
}
