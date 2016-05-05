using UnityEngine;
using System.Collections;

namespace Kontiki {
    public class SettingsInspector : MonoBehaviour
    {
        [Header("Debugging")]
        public bool debugging 			    = true;
		public bool debugQuestInfo 		    = false;
		public bool debugInteractionInfo 	= false;

        [Header("AI Settings")]
        [Range(1, 100)] public float scanningRange = 1;
        [Range(1, 100)] public float pickupRange = 1;
        [Range(10, 500)] public int memoryCapacity = 100;
        [Range(100, 1000)] public int knownAreaSize = 200;
        [Range(0.01f, 0.5f)] public float hungerIncrementPerSec = 0.1f;
        public RangeAttribute hungerRange = new RangeAttribute(0, 100);
        public RangeAttribute energyRange = new RangeAttribute(0, 1);

        //[Header("World Setting")]


	    // Use this for initialization
	    void Start ()
        {
            Settings.debugging = debugging;
            Settings.scanningRange = scanningRange;
	        Settings.pickupRange = pickupRange;
            Settings.memoryCapacity = memoryCapacity;
            Settings.knownAreaSize = knownAreaSize;
            Settings.hungerIncrementPerSec = hungerIncrementPerSec;
            Settings.hungerRange = hungerRange;
            Settings.energyRange = energyRange;
	        Settings.debugInteractionInfo = debugInteractionInfo;
	        Settings.debugQuestInfo = debugQuestInfo;

            Debug.LogWarning("Debugging is " + (debugging ? "enabled" : "disabled"));
        }
	
	    // Update is called once per frame
	    void Update ()
	    {
	        Settings.debugging = debugging;
	        Settings.scanningRange = scanningRange;
            Settings.pickupRange = pickupRange;
            Settings.memoryCapacity = memoryCapacity;
	        Settings.knownAreaSize = knownAreaSize;
	        Settings.hungerIncrementPerSec = hungerIncrementPerSec;
	        Settings.hungerRange = hungerRange;
	        Settings.energyRange = energyRange;
            Settings.debugInteractionInfo = debugInteractionInfo;
            Settings.debugQuestInfo = debugQuestInfo;
        }

        void LateUpdate()
        {
            debugging = Settings.debugging;
            scanningRange = Settings.scanningRange;
            pickupRange = Settings.pickupRange;
            memoryCapacity = Settings.memoryCapacity;
            knownAreaSize = Settings.knownAreaSize;
            hungerIncrementPerSec = Settings.hungerIncrementPerSec;
            hungerRange = Settings.hungerRange;
            energyRange = Settings.energyRange;
            debugQuestInfo = Settings.debugQuestInfo;
            debugInteractionInfo = Settings.debugInteractionInfo;
        }
    }
}
