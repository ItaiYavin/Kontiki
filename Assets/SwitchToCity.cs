﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Kontiki { 
    public class SwitchToCity : MonoBehaviour
    {
        private int numOfNewUrl;

	    // Use this for initialization
	    void Start ()
	    {
	        GetComponent<UWKWebView>().URLChanged += SwitchWhenWebsiteChange;

	        numOfNewUrl = 0;
	    }

        void SwitchWhenWebsiteChange(UWKWebView view, string url)
        {
            numOfNewUrl++;

            if(numOfNewUrl >= 5) // Magic number of amount of new url for the response to be submittet
                SceneManager.LoadScene("Scenes/City", LoadSceneMode.Single);
        }
    }
}