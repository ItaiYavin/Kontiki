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
        public GameObject questWindow;
        public GameObject infoWindow;
        public GameObject tradeWindow;

        [Header("Information")]
        public Text questText;

        // Private Variables & references
        private Character player;
        private GameObject basePanel;

        // Use this for initialization
        void Start()
        {
            basePanel = transform.GetChild(0).gameObject;
            
            SwitchWindow(Window.Start);

            SetVisibility(false);
        }

        // Update is called once per frame
        void Update()
        {
            switch (currentWindow)
            {
                case Window.Info:
                {
                    
                }
                break;

                case Window.Quest:
                {
                    
                }
                break;

                case Window.Start:
                {
                    
                }
                break;

                case Window.Trade:
                {
                    
                }
                break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void SwitchWindow(int i)
        {
            SwitchWindow((Window)i);
        }

        public void SwitchWindow(Window window)
        {
            // A bit brute-force-ish, sorry :(

            // Start Window
            if (window == Window.Start)
            {
                startWindow.SetActive(true); currentWindow = Window.Start;
            }
            else startWindow.SetActive(false);

            // Info Window
            if (window == Window.Info)
            {
                infoWindow.SetActive(true); currentWindow = Window.Info;
            }
            else infoWindow.SetActive(false);

            // Trade Window
            if (window == Window.Trade)
            {
                tradeWindow.SetActive(true); currentWindow = Window.Trade;
            }
            else tradeWindow.SetActive(false);

            // Quest Window
            if (window == Window.Quest)
            {
                questWindow.SetActive(true); currentWindow = Window.Quest;

                var q = QuestSystem.Instance.proposedQuest;

                if (q != null)
                {
                    if (q is Fetch)
                    {
                        var fetchQuest = q as Fetch;
                        questText.text = ("Fetch me " + fetchQuest.objective.gameObject.name + " and you will be rewarded!");
                    }
                }
                else
                {
                    
                }
                
            }
            else questWindow.SetActive(false);
        }

        public void SetVisibility(bool boolean)
        {
            basePanel.SetActive(boolean);
        }
    }
}
