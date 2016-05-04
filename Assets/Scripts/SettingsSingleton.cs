using UnityEngine;

/**
 * Singleton Template from http://clearcutgames.net/home/?p=437
 **/

namespace Kontiki { 
    public class SettingsSingleton : MonoBehaviour
    {
        /**
         * Settings & Attributes of AI
         **/
        [Range(0, 100)]
        public float scanningRange = 1;
        [Range(0, 100)]
        public float pickupRange = 1;
        [Range(0, 200)]
        public int memoryCapacity = 200;
        [Range(0, 1000)]
        public int knownAreaSize;
        [Range(0.01f, 1f)]
        public float hungerIncrementPerSec = 0.1f;
        public RangeAttribute hungerRange = new RangeAttribute(0, 100);
        public RangeAttribute energyRange = new RangeAttribute(0, 1);
        public bool debugging;



        // Static singleton property
        public static SettingsSingleton Instance { get; private set; }

        void Start()
        {
            if (debugging)
                Debug.LogWarning("Debugging Enabled");
        }

        void Awake()
        {
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
    }
}