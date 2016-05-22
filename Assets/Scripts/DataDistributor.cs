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
		
		if(none < high)
			chosen = UrgencyLevel.None;
		else if(high < none)
			chosen = UrgencyLevel.High;


		
		
		Settings.urgencyLevel = chosen;
	}
	
}

