using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Text;
using System.Collections;

public class ContinuationDesire : MonoBehaviour {

	public float timeUntilContinuationDesire;
	public GameObject yesNoPanel;
	public GameObject whyPanel;
	public InputField inputField;

	public GameObject[] OtherCanvases;

	private bool notCurrentlyAsking;
	private string path;

	// Use this for initialization
	void Start () {
		path = Application.dataPath;
		path = path + "/Continuation_Desire";
		notCurrentlyAsking = true;
		whyPanel.SetActive(false);
		yesNoPanel.SetActive(false);
		WriteToFile("----------| New Participant |----------" + Environment.NewLine);
		Debug.Log(path);
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time > timeUntilContinuationDesire && notCurrentlyAsking){
			// Pop up for continuation desire

			notCurrentlyAsking = false;
			yesNoPanel.SetActive(true);
			SetActiveOtherCanvases(false);
		}
	}

	public void ButtonYes(){
		string text = "- 1" + " - " + DateTime.Now;
		WriteToFile(text);
		StartWhyPanel();
	}

	public void ButtonNo(){
		string text = "- 0" + " - " + DateTime.Now;
		WriteToFile(text);
		StartWhyPanel(); 
	}

	public void ButtonSubmitText(){
		WriteToFile(": " + inputField.text + Environment.NewLine);

		notCurrentlyAsking = true;
		
		whyPanel.SetActive(false);
		yesNoPanel.SetActive(false);
		inputField.text = "";
		SetActiveOtherCanvases(true);
		timeUntilContinuationDesire += Time.time;

	}

	public void StartWhyPanel(){
		yesNoPanel.SetActive(false);
		whyPanel.SetActive(true);
	}

	public void WriteToFile(string text){
        string appendText = "" + text;
        File.AppendAllText(path, appendText);

        // Open the file to read from.
        string readText = File.ReadAllText(path);
        Console.WriteLine(readText);
	}

	public void SetActiveOtherCanvases(bool b){
		for(int i = 0; i < OtherCanvases.Length; i++){
			OtherCanvases[i].SetActive(b);
		}
	}
}
