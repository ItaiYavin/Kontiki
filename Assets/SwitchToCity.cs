using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Kontiki { 
    public class SwitchToCity : MonoBehaviour
    {
        private int numOfNewUrl;

	    // Use this for initialization
	    void Awake ()
	    {
	        GetComponent<UWKWebView>().URLChanged += SwitchWhenWebsiteChange;
	        GetComponent<UWKWebView>().MaxHeight = Screen.height;
	        GetComponent<UWKWebView>().MaxWidth = Screen.width;
            GetComponent<UWKWebView>().InitialHeight = Screen.height;
            GetComponent<UWKWebView>().InitialWidth = Screen.width;
	        GetComponent<UWKWebView>().Width = Screen.width;
	        GetComponent<UWKWebView>().Height = Screen.height;

            numOfNewUrl = 0;
	    }

        void SwitchWhenWebsiteChange(UWKWebView view, string url)
        {
            numOfNewUrl++;

            if(numOfNewUrl >= 5){ // Magic number of amount of new url for the response to be submittet
                SceneManager.LoadScene("Scenes/City", LoadSceneMode.Single);
                
                Log.properExit = true;
            }
        }
    }
}