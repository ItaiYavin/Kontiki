using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Kontiki
{
    public class WindowsHandler : MonoBehaviour
    {
        // Inspector Objects & Variables
        [Header("Windows")]
        public Window currentWindow;
        public GameObject startWindow;
        public GameObject questWindow1;
        public GameObject questWindow2;
        public GameObject infoWindow;
        public GameObject tradeWindow;
        
        [Header("Prefabs")]

        public GameObject questInfoPrefab;
        private List<GameObject> questInfoBoxes;

        [Header("Information")]
        public Text questText;

        // Private Variables & references
        public LanguageExchanger playerLang;
        private GameObject basePanel;
        
        public static WindowsHandler Instance { get; private set; }

		void Awake(){
			// First we check if there are any other instances conflicting
            if (Instance != null && Instance != this)
            {
                // If that is the case, we destroy other instances
                Destroy(gameObject);
            }

            // Here we save our singleton instance
            Instance = this;

            // Furthermore we make sure that we don't destroy between scenes (this is optional)
            DontDestroyOnLoad(gameObject);
		}
 
        
        // Use this for initialization
        void Start()
        {
            basePanel = transform.GetChild(0).gameObject;
            questInfoBoxes = new List<GameObject>();
            
            
            SwitchWindow(Window.Start);

            SetVisibility(false);
        }
        
        public void OnButtonClick(int buttonIndex){
            switch (currentWindow)
            {
                case Window.Start:{
                    switch (buttonIndex)
                    {
                        case 0:{
                            //Quest Button
                            Language.DoYouHaveQuest(playerLang, playerLang.speakingTo);
                            
                        }break;                     
                        case 1:{
                            //Info Button
                            Quest[] quests = QuestSystem.Instance.GetAcceptedQuests();
                            
                            Vector3 position = Vector3.zero;
                            GameObject g;
                            Quest q;
                            
                            for (int i = 0; i < quests.Length; i++){
                                q = quests[i];
                                if(questInfoBoxes.Count >= i){
                                    g = Instantiate(questInfoPrefab, Vector3.zero, Quaternion.identity) as GameObject;
                                    g.transform.SetParent(infoWindow.transform, false);
                                    g.GetComponent<ButtonInfo>().windowHandler = this;
                                    
                                    questInfoBoxes.Add(g);      
                                
                                }else{
                                    g = questInfoBoxes[i];
                                }  
                                
                                
                                ButtonInfo buttonInfo = g.GetComponent<ButtonInfo>();   
                                buttonInfo.index = buttonIndex;
                                
                                position = new Vector3(i * 100 - 300, 0, 0);
                                g.GetComponent<RectTransform>().anchoredPosition = position;
                                
                                buttonInfo.index = i;
                                
                                if(q is Fetch){
                                    buttonInfo.icon.sprite = Settings.iconSprites[Settings.iconTypes.IndexOf(IconType.QuestObjective)];
                                    buttonInfo.icon.color = ((Fetch)q).colorObjective;     
                                   
                                }               
                            }
                            SwitchWindow(Window.Info);
                        }break;                     
                        case 2:{
                           //Trade Button
                           
                        }break;                     
                    }
                }break;
                case Window.Info:{
                    Quest quest = QuestSystem.Instance.GetAcceptedQuests()[buttonIndex];
                    Language.DoYouHaveInfoAboutQuest(playerLang, playerLang.speakingTo, quest);
                }break;
                case Window.Trade:{
                    
                }break;
                case Window.Quest1:{
                    
                    switch (buttonIndex)
                    {
                        case 0:{
                            //Trade Button
                            
                            Quest q = playerLang.speakingTo.ai.baseRoutine.questOffer;
                            Language.WhatDoIGetForQuest(playerLang, playerLang.speakingTo, q);
                            
                        }break;                     
                        case 1:{
                            //Decline Button
                            Language.DeclineQuest(playerLang, playerLang.speakingTo);
                            
                            SetVisibility(false);
                        }break;                     
                    }
                }break; 
                case Window.Quest2:{
                    
                    switch (buttonIndex)
                    {
                        case 0:{
                            //Accept Button
                            Quest q = playerLang.speakingTo.ai.baseRoutine.questOffer;
                            Language.AcceptQuest(playerLang, playerLang.speakingTo,q);
                            SetVisibility(false);
                            
                        }break;                     
                        case 1:{
                            //Decline Button
                            Language.DeclineQuest(playerLang, playerLang.speakingTo);
                            
                            SetVisibility(false);
                        }break;                     
                    }
                }break;
            }
        }

        public void SwitchWindow(int i)
        {
            SwitchWindow((Window)i);
        }

        public void SwitchWindow(Window window)
        {

            startWindow.SetActive(false);
            infoWindow.SetActive(false);
            tradeWindow.SetActive(false);
            questWindow1.SetActive(false);
            questWindow2.SetActive(false);

            currentWindow = window;
            switch (window)
            {
                case Window.Start:{
                    startWindow.SetActive(true); 
                }break;
                case Window.Info:{
                    infoWindow.SetActive(true);
                }break;
                case Window.Trade:{
                    tradeWindow.SetActive(true);
                }break;
                case Window.Quest1:{
                    questWindow1.SetActive(true);
                }break;
                case Window.Quest2:{
                    questWindow2.SetActive(true);
                }break;
            }
        }

        public void SetVisibility(bool boolean)
        {
            basePanel.SetActive(boolean);
            SwitchWindow(Window.Start);
        }
    }
}
