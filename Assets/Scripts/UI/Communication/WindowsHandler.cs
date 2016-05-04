using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Kontiki
{
    public class WindowsHandler : MonoBehaviour
    {
        // Insepector Objects & Variables
        public Window currentWindow;
        public GameObject startWindow;
        public GameObject questWindow;
        public GameObject infoWindow;
        public GameObject tradeWindow;


        // Private Variables & references
        private Character player;
        private Canvas baseCanvas;
        private CanvasScaler baseCanvasScaler;
        private GraphicRaycaster baseGraphicRaycaster;

        // Use this for initialization
        void Start()
        {
            baseCanvas = GetComponent<Canvas>();
            baseCanvasScaler = GetComponent<CanvasScaler>();
            baseGraphicRaycaster = GetComponent<GraphicRaycaster>();
            
           SwitchWindow(Window.Start);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                SetVisibility(false);
            }

            if(Input.GetKeyDown(KeyCode.Alpha1))
                SwitchWindow(Window.Start);
            if (Input.GetKeyDown(KeyCode.Alpha2))
                SwitchWindow(Window.Quest);
            if (Input.GetKeyDown(KeyCode.Alpha3))
                SwitchWindow(Window.Info);
            if (Input.GetKeyDown(KeyCode.Alpha4))
                SwitchWindow(Window.Trade);

        }

        public void SwitchWindow(int i)
        {
            SwitchWindow((Window)i);
        }

        public void SwitchWindow(Window window)
        {
            // A bit brute-force-ish, sorry :(

            // Start Window
            if (window == Window.Start) { startWindow.SetActive(true); currentWindow = Window.Start; }
            else startWindow.SetActive(false);

            // Info Window
            if (window == Window.Info) { infoWindow.SetActive(true); currentWindow = Window.Info; }
            else infoWindow.SetActive(false);

            // Trade Window
            if (window == Window.Trade) { tradeWindow.SetActive(true); currentWindow = Window.Trade; }
            else tradeWindow.SetActive(false);

            // Quest Window
            if (window == Window.Quest) { questWindow.SetActive(true); currentWindow = Window.Quest; }
            else questWindow.SetActive(false);
        }

        public void SetVisibility(bool boolean)
        {
            baseCanvas.enabled = boolean;
            baseCanvasScaler.enabled = boolean;
            baseGraphicRaycaster.enabled = boolean;
        }
    }
}
