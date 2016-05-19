using UnityEngine;
using System.Collections;

public class BounceCounter : MonoBehaviour
{
	public UnityDataConnector conn;
	public bool trackBounces = true;
	
	void OnCollisionEnter(Collision c)
	{
	//	if (trackBounces)
//1			conn.SaveDataOnTheCloud(gameObject.name, c.relativeVelocity.magnitude);
	}
}
