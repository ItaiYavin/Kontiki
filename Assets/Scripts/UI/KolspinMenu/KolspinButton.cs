using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/***

Kolspin Button
Made by: @KasperHdL
Date: 1 Sep 2015
See KolspinMenu for information of use

***/
public class KolspinButton : MonoBehaviour {

	//transition toggles
	public bool fade_in_out = true;
	public bool collapse = true;
	public bool spin = true;

	//transition spin degrees
	public float spin_open_in_degrees = 360f;
	public float spin_close_in_degrees = -360f;

	//placement when open (if set to zero then position is used)
	public Vector3 open_position;

	//reference
	public Text text;
	CanvasGroup cgroup;

	// Use this for initialization
	void Start () {
		if(open_position.magnitude == 0)
			open_position = transform.localPosition;

		cgroup = GetComponent<CanvasGroup>();
	}

	public void fade_in(float time){
		StopAllCoroutines();
		StartCoroutine(routine_fade_to(time,1f));
	}
	public void fade_out(float time){
		StopAllCoroutines();
		StartCoroutine(routine_fade_to(time,0f));
	}


	IEnumerator routine_fade_to(float time,float value){
		float start_time = Time.time;
		float end_time = Time.time + time;

		float start_value = cgroup.alpha;

		while(Time.time < end_time){
			cgroup.alpha = Mathf.Lerp(start_value,value,(Time.time - start_time)/time);
			yield return null;
		}

		cgroup.alpha = value;
	}

}