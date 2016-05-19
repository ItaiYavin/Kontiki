using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Kontiki { 
    public class SwitchToCity : MonoBehaviour
    {
        public string url;
        public string entry;

        private int foundId;
        private int numOfNewUrl;

	    // Use this for initialization
	    void Start ()
	    {
            numOfNewUrl = 0;

	        StartCoroutine(LookForID());
	    }

        IEnumerator LookForID()
        {
            while (foundId == 0)
            {
                foundId = DataDistributor.id;
                yield return new WaitForEndOfFrame();
            }

            UWKWebView view = gameObject.AddComponent<UWKWebView>();
            string parsedUrl = url + entry + foundId;
            view.URL = parsedUrl;

            view.URLChanged += SwitchWhenWebsiteChange;

            view.MaxHeight = Screen.height;
            view.MaxWidth = Screen.width;
            view.InitialHeight = Screen.height;
            view.InitialWidth = Screen.width;
            view.Width = Screen.width;
            view.Height = Screen.height;
            view.ConnectToUrl(parsedUrl);
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