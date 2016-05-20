using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Kontiki;
public class FinishedGame : MonoBehaviour
{
    public string url;
    public string entry;

    private int numOfNewUrl;

	// Use this for initialization
	void Start ()
	{
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        UWKWebView view = gameObject.AddComponent<UWKWebView>();
        string parsedUrl = url + entry + DataDistributor.id + "_" + Log.pcID;
        view.URL = parsedUrl;

        view.URLChanged += SwitchWhenWebsiteChange;

        view.MaxHeight = Screen.height;
        view.MaxWidth = Screen.width;
        view.InitialHeight = Screen.height;
        view.InitialWidth = Screen.width;
        view.Width = Screen.width;
        view.Height = Screen.height;
        view.ConnectToUrl(parsedUrl);

        numOfNewUrl = 0;
    }

    void SwitchWhenWebsiteChange(UWKWebView view, string url)
    {
        numOfNewUrl++;

        if (numOfNewUrl >= 5) {// Magic number of amount of new url for the response to be submittet
            PlayerPrefs.SetInt("hasSubmitted", 1);
            PlayerPrefs.Save();
            Log.properExit = true;
            Application.Quit();
        }
    }
}
