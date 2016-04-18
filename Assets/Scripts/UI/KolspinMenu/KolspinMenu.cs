using UnityEngine;
using System.Collections;

/***

Kolspin Menu
Made by: @KasperHdL
Date: 1 Sep 2015

Description:
The Kolspin Menu can be put on canvas or an empty object under a canvas.

Menu that can collapse, spin and fade in and out, each element can be controlled invididual.
To set up make the menu with the Kolspin Button Prefab, Choose how it should transition by checking and unchecking each button's [Fade_in_out, Collapse, Spin].

To open and close the menu use Unity built in button function call to call open/close/toggle

***/

public class KolspinMenu : MonoBehaviour {

	//reference (if left empty then it is populated with children of this object)
	public KolspinButton[] buttons;

	//transition time
	public float transition_length_open = 1f;
	public float transition_length_close = 1f;

	//current state (decides if it should start open or closed as well)
	public bool is_open = false;

	public float transition_progress = 0f;


//--- Unity Methods ---//
	void Start () {
		if(buttons.Length == 0){
			buttons = new KolspinButton[transform.childCount];
			int i = 0;
			foreach(Transform child in transform)
				buttons[i++] = child.GetComponent<KolspinButton>();
		}
	}

//--- Public ---//
	///<summary>opens the menu</summary>
	public void open(){
		is_open = true;

		StopAllCoroutines();
		StartCoroutine(run_transition_open());

	}

	///<summary>closes the menu</summary>
	public void close(){
		is_open = false;

		StopAllCoroutines();
		StartCoroutine(run_transition_close());

	}

	///<summary>toggles open/close</summary>
	public void toggle(){
		if(is_open)
			close();
		else 
			open();
	}

//--- Private ---//
	IEnumerator run_transition_open(){
		float start_time = Time.time;
		float end_time = start_time + transition_length_open;

		//fade
		for(int i = 0;i<buttons.Length;i++){
			if(buttons[i].fade_in_out){
				buttons[i].fade_in(transition_length_open);
			}
		}

		while(Time.time < end_time){
			float step = (Time.time - start_time)/transition_length_open;
			
			transition_progress = step;

			//collapse and spin buttons
			for(int i = 0;i<buttons.Length;i++){
				Vector3 point = buttons[i].open_position;
				
				//Collapse
				if(buttons[i].collapse){
					point = Vector3.Lerp(Vector3.zero,point,step);
				}

				//Spin
				if(buttons[i].spin){
					float spin = buttons[i].spin_open_in_degrees * Mathf.Deg2Rad;

					float end_angle = Vector3.Angle(Vector3.right, buttons[i].open_position) * Mathf.Deg2Rad;
					float angle = Mathf.Lerp(end_angle+spin,end_angle,step);

					point = new Vector3(Mathf.Cos(angle) * point.magnitude, Mathf.Sin(angle) * point.magnitude,0);
				}
					
				buttons[i].transform.localPosition = point;
				
			}
			
			yield return null;
		}

		transition_progress = 1;

		//ensure correct placement
		for(int i = 0;i<buttons.Length;i++){
			if(buttons[i].collapse || buttons[i].spin){
				buttons[i].transform.localPosition = buttons[i].open_position;
			}
		}
		
		
	}

	IEnumerator run_transition_close(){
		float start_time = Time.time;
		float end_time = start_time + transition_length_close;

		//fade
		for(int i = 0;i<buttons.Length;i++){
			if(buttons[i].fade_in_out){
				buttons[i].fade_out(transition_length_close);
			}
		}

		while(Time.time < end_time){
			//collapse and spin buttons
			float step = (Time.time - start_time)/transition_length_close;
			transition_progress = 1 - step;
			
			for(int i = 0;i<buttons.Length;i++){
				Vector3 point = buttons[i].open_position;
				
				//Collapse
				if(buttons[i].collapse){
					point = Vector3.Lerp(point,Vector3.zero,step);
				}

				//Spin
				if(buttons[i].spin){
					float spin = buttons[i].spin_close_in_degrees * Mathf.Deg2Rad;

					float start_angle = Vector3.Angle(Vector3.right, buttons[i].open_position) * Mathf.Deg2Rad;
					float angle = Mathf.Lerp(start_angle,start_angle+spin,step);

					point = new Vector3(Mathf.Cos(angle) * point.magnitude, Mathf.Sin(angle) * point.magnitude,0);
				}
					
				buttons[i].transform.localPosition = point;
				
			}
			
			yield return null;
		}
		
		transition_progress = 0;

		//ensure correct placement
		for(int i = 0;i<buttons.Length;i++){
			if(buttons[i].collapse || buttons[i].spin){
				buttons[i].transform.localPosition = Vector3.zero;
			}
		}
		
		
	}
}