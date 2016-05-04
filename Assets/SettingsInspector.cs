using UnityEngine;
using System.Collections;

namespace Kontiki {
    public class SettingsInspector : MonoBehaviour
    {
        [Header("Debugging")]
        public bool debugging;

        [Header("AI Settings")]
        [Range(1, 100)] public float scanningRange;
        [Range(1, 100)] public float pickupRange;
        [Range(10, 500)] public int memoryCapacity;
        [Range(100, 1000)] public int knownAreaSize;
        [Range(0.01f, 0.5f)] public float hungerIncrementPerSec;
        public RangeAttribute hungerRange;
        public RangeAttribute energyRange;

        //[Header("World Setting")]


	    // Use this for initialization
	    void Start ()
        {
            debugging = Settings.debugging;
            scanningRange = Settings.scanningRange;
            memoryCapacity = Settings.memoryCapacity;
            knownAreaSize = Settings.knownAreaSize;
            hungerIncrementPerSec = Settings.hungerIncrementPerSec;
            hungerRange = Settings.hungerRange;
            energyRange = Settings.energyRange;
        }
	
	    // Update is called once per frame
	    void Update ()
	    {
	        Settings.debugging = debugging;
	        Settings.scanningRange = scanningRange;
	        Settings.memoryCapacity = memoryCapacity;
	        Settings.knownAreaSize = knownAreaSize;
	        Settings.hungerIncrementPerSec = hungerIncrementPerSec;
	        Settings.hungerRange = hungerRange;
	        Settings.energyRange = energyRange;
	    }

        void LateUpdate()
        {
            debugging = Settings.debugging;
            scanningRange = Settings.scanningRange;
            memoryCapacity = Settings.memoryCapacity;
            knownAreaSize = Settings.knownAreaSize;
            hungerIncrementPerSec = Settings.hungerIncrementPerSec;
            hungerRange = Settings.hungerRange;
            energyRange = Settings.energyRange;
        }
    }
}
