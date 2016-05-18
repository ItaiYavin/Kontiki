using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using Kontiki;

using System;

public class UnityDataConnector : MonoBehaviour
{

    static public UnityDataConnector Instance;

    void Awake(){
        // First we check if there are any other instances conflicting
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        
        Instance = this;

        DontDestroyOnLoad(gameObject);
    }
	public int logsSend = 0;
	public string webServiceUrl = "";
	public string spreadsheetId = "";
	public string worksheetName = "";
	public string password = "";
	public float maxWaitTime = 10f;
	public GameObject dataDestinationObject;
	public string logWorksheet = "Log";
	public string urgencyWorksheet = "Urgency";
	public bool debugMode;

	bool updating = false;
	string currentStatus;
	JsonData[] ssObjects;
	bool saveToGS; 
	
	bool isSendingLog = false;
	List<String[]> futureLogs = new List<String[]>();
	
	void Start ()
	{
		Time.timeScale = 0f;
		Connect();
	}
	
	
	void Connect()
	{
		if (updating)
			return;
		
		updating = true;
		StartCoroutine(GetData());  
	}
	
	IEnumerator GetData()
	{
		string connectionString = webServiceUrl + "?ssid=" + spreadsheetId + "&sheet=" + worksheetName + "&pass=" + password + "&action=GetData";
		if (debugMode)
			Debug.Log("Connecting to webservice on " + connectionString);

		WWW www = new WWW(connectionString);
		
		float elapsedTime = 0.0f;
		currentStatus = "Stablishing Connection... ";
		
		while (!www.isDone)
		{
			elapsedTime += Time.deltaTime;			
			if (elapsedTime >= maxWaitTime)
			{
				currentStatus = "Max wait time reached, connection aborted.";
				Debug.Log(currentStatus);
				updating = false;
				break;
			}
			
			yield return null;  
		}
	
		if (!www.isDone || !string.IsNullOrEmpty(www.error))
		{
			currentStatus = "Connection error after" + elapsedTime.ToString() + "seconds: " + www.error;
			Debug.LogError(currentStatus);
			updating = false;
			yield break;
		}
	
		string response = www.text;
		Debug.Log(elapsedTime + " : " + response);
		currentStatus = "Connection stablished, parsing data...";

		if (response == "\"Incorrect Password.\"")
		{
			currentStatus = "Connection error: Incorrect Password.";
			Debug.LogError(currentStatus);
			updating = false;
			yield break;
		}

		try 
		{
			ssObjects = JsonMapper.ToObject<JsonData[]>(response);
		}
		catch
		{
			currentStatus = "Data error: could not parse retrieved data as json.";
			Debug.LogError(currentStatus);
			updating = false;
			yield break;
		}

		currentStatus = "Data Successfully Retrieved!";
		updating = false;
		
		// Finally use the retrieved data as you wish.
		dataDestinationObject.SendMessage("DoSomethingWithTheData", ssObjects);
		Time.timeScale = 1f;
	}

	public void SaveID(int id, UrgencyLevel level){
		StartCoroutine(SendID(id, level)); 
	}



	IEnumerator SendID(int id, UrgencyLevel level)
	{
		
		float time = Time.time;
		float rTime = Time.realtimeSinceStartup;
		Debug.Log(rTime);
		
		string n = "0", m = "0", h = "0";
		
		switch (level)
		{
			case UrgencyLevel.None: n = "1"; break;
			case UrgencyLevel.Med: 	m = "1"; break;
			case UrgencyLevel.High: h = "1"; break;
		}

		string connectionString = 	webServiceUrl +
									"?ssid=" + spreadsheetId +
									"&sheet=" + urgencyWorksheet +
									"&pass=" + password +
									"&val1=" + id +
									"&val2=" + level.ToString();
									
		for (int i = 3; i < 20; i++)
		{
			connectionString += "&val" + i + "=" + "";
		}
									
		connectionString += "&action=SetData";
									
		if (debugMode)
			Debug.Log("Connection String: " + connectionString);
		WWW www = new WWW(connectionString);
		float elapsedTime = 0.0f;

		while (!www.isDone)
		{
			elapsedTime += Time.deltaTime;			
			if (elapsedTime >= maxWaitTime)
			{
				// Error handling here.
				break;
			}

			yield return null;  
		}
		
		if (!www.isDone || !string.IsNullOrEmpty(www.error))
		{
			// Error handling here.
			Debug.Log("error " + www.error);
			yield break;
		}
		
		string response = www.text;

		if (response.Contains("Incorrect Password"))
		{
			// Error handling here.
			Debug.Log("incorrect password");
			yield break;
		}

		if (response.Contains("RCVD OK"))
		{
			// Data correctly sent!
			Debug.Log("correctly sent");
			yield break;
		}
	}


	public void SendLogData(String[] data){
		logsSend++;
		data[19] = logsSend.ToString();
		futureLogs.Add(data);
		StartSendingLogs();
	}
	
	public void StartSendingLogs(){
		if(!isSendingLog){
			Time.timeScale = 0f;
			StartCoroutine(SendData());
		}
		isSendingLog = true;
	}
	
	IEnumerator SendData()
	{
		string[] data = futureLogs[0];
		futureLogs.RemoveAt(0);
		
		string connectionString = 	webServiceUrl +
									"?ssid=" + spreadsheetId +
									"&sheet=" + logWorksheet +
									"&pass=" + password;
		
		for (int i = 1; i < data.Length; i++)
		{
			connectionString += "&val" + i + "=" + (data[i] == null ? "" : data[i]);
		}
		
		for (int i = data.Length; i < 20; i++)
		{
			connectionString += "&val" + i + "=" + "";
		}
									
		connectionString += "&action=SetData";
		
		connectionString = connectionString.Replace(" ", "%20");
		if (debugMode)
			Debug.Log("Connection String: " + connectionString);
		WWW www = new WWW(connectionString);
		float elapsedTime = 0.0f;

		while (!www.isDone)
		{
			elapsedTime += Time.deltaTime;			
			if (elapsedTime >= maxWaitTime)
			{
				// Error handling here.
				break;
			}

			yield return null;  
		}
		if (!www.isDone || !string.IsNullOrEmpty(www.error))
		{
			// Error handling here.
			Debug.Log("error " + data[4] + " - " + www.error);
			yield break;
		}
		
		string response = www.text;

		if (response.Contains("Incorrect Password"))
		{
			// Error handling here.
			Debug.Log("incorrect password");
			yield break;
		}

		if (response.Contains("RCVD OK"))
		{
			// Data correctly sent!
			Debug.Log("correctly sent");
			SendNextLog();
			yield break;
		}
			
	}
	
	void SendNextLog(){
		if(futureLogs.Count > 0)
			StartCoroutine(SendData());
		else
			Time.timeScale = 1f;
	}
}
	
	