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
        
        
        [Header("Button that can be disabled")]
        public GameObject infoWindowButton;
        
        [Header("Prefabs")]

        public GameObject questInfoPrefab;
        private List<ButtonInfo> questInfoBoxes;

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
            questInfoBoxes = new List<ButtonInfo>();
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
                            ButtonInfo b;
                            Quest q;
                            
                            for (int i = 0; i < quests.Length; i++){
                                q = quests[i];
                                if(questInfoBoxes.Count <= i){
                                    GameObject g = Instantiate(questInfoPrefab, Vector3.zero, Quaternion.identity) as GameObject;
                                    
                                    g.transform.SetParent(infoWindow.transform, false);
                                    
                                    b = g.GetComponent<ButtonInfo>();
                                    b.windowHandler = this;
                                    
                                    questInfoBoxes.Add(b); 
                                
                                }else{
                                    b = questInfoBoxes[i];
                                    b.gameObject.SetActive(true);
                                }  
                                
                                b.index = i;
                                
                                Debug.Log(q.HasCharacterBeenAsked(playerLang.speakingTo.character));
                                b.button.interactable = !q.HasCharacterBeenAsked(playerLang.speakingTo.character);
                                                                
                                position = new Vector3(i * 100 - 200, 0, 0);
                                b.GetComponent<RectTransform>().anchoredPosition = position;
                                
                                
                                if(q is Fetch){
                                    b.icon.sprite = Settings.iconSprites[Settings.iconTypes.IndexOf(IconType.QuestObjective)];
                                    b.icon.color = ((Fetch)q).colorObjective;     
                                   
                                }               
                            }
                            
                            for (int i = quests.Length; i < questInfoBoxes.Count; i++)
                            {
                                questInfoBoxes[i].gameObject.SetActive(false);
                            }
                            
                            SwitchWindow(Window.Info);
                        }break;                     
                        case 2:{
                           //Trade Button
                           
                        }break;                     
                    }
                }break;
                case Window.Info:{
                    if(buttonIndex != -1){
                        Quest[] acceptedQuest = QuestSystem.Instance.GetAcceptedQuests();
                        if(acceptedQuest.Length > 0){
                            Quest quest = acceptedQuest[buttonIndex];
                            Language.DoYouHaveInfoAboutQuest(playerLang, playerLang.speakingTo, quest);
                            questInfoBoxes[buttonIndex].button.interactable = false;
                            bool b = false;
                            for (int i = 0; i < acceptedQuest.Length; i++)
                            {
                                if(!acceptedQuest[i].HasCharacterBeenAsked(playerLang.speakingTo.character)){
                                    b = true;
                                    break;
                                }
                                
                            }
                            
                            if(!b)
                                SetVisibility(false);
                        }
                    }
                    
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
                            Quest q = playerLang.speakingTo.ai.baseRoutine.questOffer;
                            Language.DeclineQuest(playerLang, playerLang.speakingTo, q);
                            
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
                            Quest q = playerLang.speakingTo.ai.baseRoutine.questOffer;
                            Language.DeclineQuest(playerLang, playerLang.speakingTo, q);
                            
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
                    if(QuestSystem.Instance.acceptedQuests != null)
                        infoWindowButton.GetComponent<Button>().interactable = QuestSystem.Instance.acceptedQuests.Count != 0;
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
