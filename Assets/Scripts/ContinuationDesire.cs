using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Text;
using System.Collections;
using Kontiki;
using UnityEngine.SceneManagement;

public class ContinuationDesire : MonoBehaviour {

	public float continuationDesireDelay;
	private float nextPrompt;
	public GameObject yesNoPanel;
	public GameObject whyPanel;
	public InputField inputField;

	public GameObject[] OtherCanvases;

	private bool notCurrentlyAsking;

    public int amountOfPrompts;
    public int maxPromptsUntilExit;
	
	private bool wantsToContinue;

    // Use this for initialization
	void Start () {
		
		UrgencyLevel chosen;
		
		float r = UnityEngine.Random.Range(0f,3f);
		if(r < 1f)
			chosen = UrgencyLevel.None;
		else if(r < 2f)
			chosen = UrgencyLevel.Med;
		else
			chosen = UrgencyLevel.High;
	
		Settings.urgencyLevel = chosen;
		notCurrentlyAsking = true;
		whyPanel.SetActive(false);
		yesNoPanel.SetActive(false);
		
		Log.UrgencyLevel(chosen);
		
		nextPrompt = Time.time + continuationDesireDelay;
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
		wantsToContinue = true;
		StartWhyPanel();
	}

	public void ButtonNo(){
		wantsToContinue = false;
		StartWhyPanel();
	}

	public void ButtonSubmitText(){
		
		Log.CD(wantsToContinue, inputField.text);
		
		notCurrentlyAsking = true;
		
		whyPanel.SetActive(false);
		yesNoPanel.SetActive(false);
		inputField.text = "";
		SetActiveOtherCanvases(true);
		Time.timeScale = 1f;
		nextPrompt = Time.time + continuationDesireDelay;
		
		Settings.player.GetComponent<InteractionSystem>().menuOpen = false;

        amountOfPrompts++;
	    if (amountOfPrompts >= maxPromptsUntilExit || !wantsToContinue)
	    {
	        SceneManager.LoadScene("Scenes/Post-Questionaire", LoadSceneMode.Single);
	    }
    }

	public void StartWhyPanel(){
		yesNoPanel.SetActive(false);
		whyPanel.SetActive(true);
	}

	public void SetActiveOtherCanvases(bool b){
		for(int i = 0; i < OtherCanvases.Length; i++){
			OtherCanvases[i].SetActive(b);
			Settings.player.GetComponent<InteractionSystem>().menuOpen = true;
		}
	}
}
