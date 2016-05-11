using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Text;
using System.Collections;
using Kontiki;

public class ContinuationDesire : MonoBehaviour {

	

	public float continuationDesireDelay;
	private float nextPrompt;
	public GameObject yesNoPanel;
	public GameObject whyPanel;
	public InputField inputField;

	public GameObject[] OtherCanvases;

	private bool notCurrentlyAsking;
	private string path;
	private DateTime startDate;
	private bool hasWrittenStart = false;
	
	
	private string urgencyString;
	private string newParticipantString = "#!#";
	
	private string doYouWantToContinueString;

	// Use this for initialization
	void Start () {
		
		int none = 0;
		int med  = 0;
		int high = 0;
		
		path = Application.dataPath;
		path = path + "/Continuation_Desire.txt";
		bool error = false;
		string readText = "";
		urgencyString = "";
		UrgencyLevel chosen = UrgencyLevel.None;
		
		try{
        	readText = File.ReadAllText(path);
		}catch(Exception e){
			File.Create(path);
        	readText = File.ReadAllText(path);
		
			
		}
		
		if(readText.IndexOf(newParticipantString) != -1){
			while(readText.IndexOf(newParticipantString) != -1){
				int startIndex = readText.IndexOf(newParticipantString);
				string s = readText.Substring(startIndex + newParticipantString.Length, 1);
				if(s == "H") high++;
				if(s == "M") med++;
				if(s == "N") none++;
				readText = readText.Substring(startIndex + newParticipantString.Length + 1);
			}
			
			int lead = 2;
			//pick at random if no one has a two point lead
			if(high - lead >= med || high - lead >= none){
				if(med == none)
					chosen = UnityEngine.Random.Range(0f,1f) > .5f ? UrgencyLevel.None : UrgencyLevel.Med;
				else if(med > none)
					chosen = UrgencyLevel.None;
				else
					chosen = UrgencyLevel.Med;
			}else if(none - lead >= med || none - lead >= high){
				if(med == high)
					chosen = UnityEngine.Random.Range(0f,1f) > .5f ? UrgencyLevel.High : UrgencyLevel.Med;
				else if(med > high)
					chosen = UrgencyLevel.High;
				else
					chosen = UrgencyLevel.Med;
			}else if(med - lead >= high || med - lead >= none){
				if(high == none)
					chosen = UnityEngine.Random.Range(0f,1f) > .5f ? UrgencyLevel.None : UrgencyLevel.High;
				else if(high > none)
					chosen = UrgencyLevel.None;
				else
					chosen = UrgencyLevel.High;
			}else{
				float r = UnityEngine.Random.Range(0f,3f);
				if(r < 1f)
					chosen = UrgencyLevel.None;
				else if(r < 2f)
					chosen = UrgencyLevel.Med;
				else if(r < 3f)
					chosen = UrgencyLevel.High;
			}
			
		}else{
			
			float r = UnityEngine.Random.Range(0f,3f);
			if(r < 1f)
				chosen = UrgencyLevel.None;
			else if(r < 2f)
				chosen = UrgencyLevel.Med;
			else if(r < 3f)
				chosen = UrgencyLevel.High;
		}
		
		Debug.Log("h: " + high + " m: " + med + " n: " + none + " => " + chosen);
		
		switch (chosen)
		{
			case UrgencyLevel.High: urgencyString = "H"; break;
			case UrgencyLevel.Med: urgencyString = "M"; break;
			case UrgencyLevel.None: urgencyString = "N"; break;
		}
		Settings.urgencyLevel = chosen;
		notCurrentlyAsking = true;
		whyPanel.SetActive(false);
		yesNoPanel.SetActive(false);
		nextPrompt = Time.time + continuationDesireDelay;
		startDate = DateTime.Now;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time > nextPrompt && notCurrentlyAsking){
			// Pop up for continuation desire
			Settings.player.GetComponent<InteractionSystem>().menuOpen = true;
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			notCurrentlyAsking = false;
			yesNoPanel.SetActive(true);
			SetActiveOtherCanvases(false);
			Time.timeScale = 0;
			
		}
	}

	public void ButtonYes(){
		doYouWantToContinueString = "1";
		StartWhyPanel();
	}

	public void ButtonNo(){
		doYouWantToContinueString = "0";
		StartWhyPanel(); 
	}

	public void ButtonSubmitText(){
		
		if(!hasWrittenStart && inputField.text != ""){
			WriteToFile(Environment.NewLine + "#!#" + urgencyString + " - start time: " + startDate +  Environment.NewLine);
			hasWrittenStart = true;
		}
		if(inputField.text != "")
			WriteToFile(doYouWantToContinueString + " - " + DateTime.Now + " : "  + Environment.NewLine + inputField.text + Environment.NewLine);

		notCurrentlyAsking = true;
		
		whyPanel.SetActive(false);
		yesNoPanel.SetActive(false);
		inputField.text = "";
		SetActiveOtherCanvases(true);
		Time.timeScale = 1f;
		nextPrompt = Time.time + continuationDesireDelay;
		
		Settings.player.GetComponent<InteractionSystem>().menuOpen = false;
	}

	public void StartWhyPanel(){
		yesNoPanel.SetActive(false);
		whyPanel.SetActive(true);
	}

	public void WriteToFile(string text){
        string appendText = "" + text;
        File.AppendAllText(path, appendText);
	}

	public void SetActiveOtherCanvases(bool b){
		for(int i = 0; i < OtherCanvases.Length; i++){
			OtherCanvases[i].SetActive(b);
			Settings.player.GetComponent<InteractionSystem>().menuOpen = true;
		}
	}
}
