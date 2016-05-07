using System;
using UnityEngine;
using System.Collections;
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
                            
                        }break;                     
                        case 2:{
                           //Trade Button
                           
                        }break;                     
                    }
                }break;
                case Window.Info:{
                    
                }break;
                case Window.Trade:{
                    
                }break;
                case Window.Quest1:{
                    
                    switch (buttonIndex)
                    {
                        case 0:{
                            //Trade Button
                            Language.WhatDoIGetForQuest(playerLang, playerLang.speakingTo);
                            
                        }break;                     
                        case 1:{
                            //Decline Button
                            Language.DeclineQuest(playerLang, playerLang.speakingTo);
                            
                            SetVisibility(false);
                            SwitchWindow(Window.Start);
                        }break;                     
                    }
                }break; 
                case Window.Quest2:{
                    
                    switch (buttonIndex)
                    {
                        case 0:{
                            //Accept Button
                            Language.AcceptQuest(playerLang, playerLang.speakingTo);
                            SetVisibility(false);
                            SwitchWindow(Window.Start);
                            
                        }break;                     
                        case 1:{
                            //Decline Button
                            Language.DeclineQuest(playerLang, playerLang.speakingTo);
                            
                            SetVisibility(false);
                            SwitchWindow(Window.Start);
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
        }
    }
}
