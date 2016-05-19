using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class AgreeDisclaimer : MonoBehaviour
{

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void Agree()
    {
        SceneManager.LoadScene("Scenes/Pre-Questionaire");
    }

    public void Disagree()
    {
        Application.Quit();
    }
}
