using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Kontiki {
    public class SettingsInspector : MonoBehaviour
    {
        [Header("Debugging")]
        public bool debugging 			    = true;
		public bool debugQuestInfo 		    = false;
		public bool debugIconSystem	    = false;

        [Header("Language")]
        public List<IconType> iconTypes;
        public List<Sprite> iconSprites;
        public List<Color> languageColors;
        public float iconWidth;
        public float iconOffset;
        public Vector3 iconContainerOffset;
        
         
        [Header("AI Settings")]
        [Range(1, 100)] public float scanningRange = 1;
        [Range(1, 100)] public float pickupRange = 1;
        [Range(10, 500)] public int memoryCapacity = 100;
        [Range(100, 1000)] public int knownAreaSize = 200;
        [Range(0.01f, 0.5f)] public float hungerIncrementPerSec = 0.1f;
        [Range(0.01f, 0.5f)] public float energyIncrementPerSec = 0.3f;
        [Range(0.01f, 0.5f)] public float energyDecrementPerSec = 0.1f;
        [Range(0.01f, 0.5f)] public float socialIncrementPerSec = 0.3f;
        [Range(0.01f, 0.5f)] public float socialDecrementPerSec = 0.1f;
        [Range(0.1f,50f)] public float stopInteractingDistance = 1f;
        public RangeAttribute hungerRange = new RangeAttribute(0, 100);
        public RangeAttribute energyRange = new RangeAttribute(0, 1);
        public RangeAttribute socialRange = new RangeAttribute(0, 1);
        

        //[Header("World Setting")]


	    // Use this for initialization
	    void Awake ()
        {
            SetValues();
            
            //Only once at Start
            Settings.iconTypes = iconTypes;
            Settings.iconSprites = iconSprites;
            Settings.languageColors = languageColors;

            Debug.LogWarning("Debugging is " + (debugging ? "enabled" : "disabled"));
        }
	
	    void FixedUpdate ()
	    {
	        SetValues();
        }

        
        void SetValues(){
            Settings.debugging = debugging;
            Settings.debugIconSystem = debugIconSystem;
            Settings.debugQuestInfo = debugQuestInfo;
            
	        Settings.scanningRange = scanningRange;
            Settings.pickupRange = pickupRange;
            Settings.memoryCapacity = memoryCapacity;
	        Settings.knownAreaSize = knownAreaSize;
	        Settings.hungerIncrementPerSec = hungerIncrementPerSec;
            Settings.energyIncrementPerSec = energyIncrementPerSec;
            Settings.energyDecrementPerSec = energyDecrementPerSec;
            Settings.socialIncrementPerSec = socialIncrementPerSec;
            Settings.socialDecrementPerSec = socialDecrementPerSec;
	        Settings.hungerRange = hungerRange;
	        Settings.energyRange = energyRange;
            Settings.socialRange = socialRange;
            
            Settings.iconWidth = iconWidth;
            Settings.iconOffset = iconOffset;
            Settings.iconContainerOffset = iconContainerOffset;
            Settings.stopInteractingDistance = stopInteractingDistance;
        }
    }
}
