using UnityEngine;
using System.Collections;
using LitJson;
using Kontiki;

public class DataDistributor : MonoBehaviour
{
	public static int id;
	
	public int none, med, high, lead;
	private UrgencyLevel chosen;
	
	public UnityDataConnector unityConnector;
	
	
	void Update(){
		if(!Input.GetKey(KeyCode.F8))
			return;
			
		if(Input.GetKey(KeyCode.F10))
			chosen = Settings.urgencyLevel = UrgencyLevel.None;
		
		if(Input.GetKey(KeyCode.F11))
			chosen = Settings.urgencyLevel = UrgencyLevel.Med;
		
		if(Input.GetKey(KeyCode.F12))
			chosen = Settings.urgencyLevel = UrgencyLevel.High;
		
		Debug.Log(chosen);
	}
	
	public void DoSomethingWithTheData(JsonData[] ssObjects)
	{
		
		for (int i = 0; i < ssObjects.Length; i++) 
		{	
			if (ssObjects[i].Keys.Contains("NextID"))
				id = int.Parse(ssObjects[i]["NextID"].ToString());
				
			if (ssObjects[i].Keys.Contains("None"))
				none = int.Parse(ssObjects[i]["None"].ToString());

			if (ssObjects[i].Keys.Contains("Med"))
				med = int.Parse(ssObjects[i]["Med"].ToString());

			if (ssObjects[i].Keys.Contains("High"))
				high = int.Parse(ssObjects[i]["High"].ToString());
				
			if (ssObjects[i].Keys.Contains("Lead"))
				lead = int.Parse(ssObjects[i]["Lead"].ToString());

		}	
		Log.SendID();
		pickCD();
		Debug.Log(chosen);
	}
	
	private void pickCD(){
		//pick at random if no one has a two point lead
		if(high - lead >= med || high - lead >= none){
		Debug.Log("h");
			if(med == none)
				chosen = UnityEngine.Random.Range(0f,1f) > .5f ? UrgencyLevel.None : UrgencyLevel.Med;
			else if(med > none)
				chosen = UrgencyLevel.None;
			else
				chosen = UrgencyLevel.Med;
		}else if(none - lead >= med || none - lead >= high){
		Debug.Log("n");
			if(med == high)
				chosen = UnityEngine.Random.Range(0f,1f) > .5f ? UrgencyLevel.High : UrgencyLevel.Med;
			else if(med > high)
				chosen = UrgencyLevel.High;
			else
				chosen = UrgencyLevel.Med;
		}else if(med - lead >= high || med - lead >= none){
		Debug.Log("m");
			if(high == none)
				chosen = UnityEngine.Random.Range(0f,1f) > .5f ? UrgencyLevel.None : UrgencyLevel.High;
			else if(high > none)
				chosen = UrgencyLevel.None;
			else
				chosen = UrgencyLevel.High;
		}else{
			float r = UnityEngine.Random.Range(0f,3f);
		Debug.Log("r");
			if(r < 1f)
				chosen = UrgencyLevel.None;
			else if(r < 2f)
				chosen = UrgencyLevel.Med;
			else if(r < 3f)
				chosen = UrgencyLevel.High;
		}
		
		Settings.urgencyLevel = chosen;
	}
	
}

