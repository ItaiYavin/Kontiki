using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Kontiki.AI;

namespace Kontiki {
    [ExecuteInEditMode]
    public class SettingsInspector : MonoBehaviour
    {
        public bool updateValuesEveryFrame = false;

        
        [Header("Debugging")]
        public bool debugging 			    = true;
		public bool debugQuestInfo 		    = false;
		public bool debugIconSystem	    = false;
        public bool countEverything = false;


        [Header("References")]
        public Character player;
        public sui_demo_ControllerMaster controller;
        public Transform interactableIndicatorContainer;
        public WindowsHandler windowsHandler;

        

        [Header("Language")]
        public List<IconType> iconTypes;
        public List<Sprite> iconSprites;
        public List<Color> languageColors;
        public float iconWidth;
        public float iconOffset;
        public Vector3 iconContainerOffset;
        
        public float speechDelay = 1f;
        
         
        [Header("AI Settings")]
        [Range(1, 100)] public float scanningRange = 1;
        [Range(1, 100)] public float pickupRange = 1;
        [Range(10, 500)] public int memoryCapacity = 100;
        [Range(100, 1000)] public int knownAreaSize = 200;
        [Range(0.0001f, 0.5f)] public float hungerIncrementPerSec = 0.1f;
        [Range(0.0001f, 0.1f)] public float energyIncrementPerSec = 0.3f;
        [Range(0.0001f, 0.1f)] public float energyDecrementPerSec = 0.001f;
        [Range(0.0001f, 0.1f)] public float socialDecrementPerSec = 0.001f;
        [Range(0.0001f, 0.1f)] public float socialIncrementPerSec = 0.001f;
        [Range(0.1f,50f)] public float stopInteractingDistance = 1f;
        [Range(5f,50f)] public float minDeliveryRouteRange = 10f;
        [Range(50f,500f)] public float deliveryScanRange = 200f;
        [Range(0.1f,20f)] public float npcIconDuration = 10f;
        [Range(0.1f,20f)] public float playerIconDuration = 2f;
        
        
        
        public RangeAttribute hungerRange = new RangeAttribute(0, 100);
        public RangeAttribute energyRange = new RangeAttribute(0, 1);
        public RangeAttribute socialRange = new RangeAttribute(0, 1);
        
        
        [Header("Job Settings")]
        public float dockingRange = 1f;
        public List<Trader> traders;
        
        
        public Transform portContainer;
        public Transform fishingSpotContainer;
        public Transform scavengingSpotContainer;
        public Transform homeContainer;
        public Transform plazaContainer;

        private List<Transform> ports           = new List<Transform>();
        private List<Transform> fishingSpots    = new List<Transform>();
        private List<Transform> scavengingSpots = new List<Transform>();
        private List<Transform> homes           = new List<Transform>();
        private List<Transform> plazas          = new List<Transform>();
	    
        void Awake(){
            Init();
            
          
            
        }
        
        void Start(){
              if(countEverything){
                AIComponentContainer[] ais = GameObject.FindObjectsOfType<AIComponentContainer>();
                int total = ais.Length;
                int numTraders = 0;
                int numScavengers = 0;
                int numFishers = 0;
                int numDeliveryMans = 0;
                int numCitizens = 0;
                
                int numHomes = homes.Count;
                int numPorts = ports.Count;
                int numFishingSpots = fishingSpots.Count;
                int numScavengingSpots = scavengingSpots.Count;
                
                //count stuff
                
                for (int i = 0; i < ais.Length; i++)
                {
                    AIComponentContainer ai = ais[i];
                    if(ai.GetComponent<DeliveryMan>() != null)
                        numDeliveryMans++;
                    else if(ai.GetComponent<Fisher>()){
                        if(ai.GetComponent<Fisher>().isScavenger)
                            numScavengers++;
                        else
                            numFishers++;
                    }else if(ai.GetComponent<Trader>() != null)
                        numTraders++;
                    else
                        numCitizens++;
                }
                
                Debug.Log("total: " + total + ", traders: " + numTraders + ", Scavengers: " + numScavengers + ", Fishers: " + numFishers + ", DeliveryMans: " + numDeliveryMans + ", Citizen: " + numCitizens);
                Debug.Log("homes: " + (total - numTraders) + "/" + numHomes + ", ports: " + (numFishers + numScavengers) + "/" + (numPorts) + ", fishingSpots: " + (numFishers) + "/" + (numFishingSpots) + ", scavengingSpots: " + (numScavengers) + "/" + (numScavengingSpots));
            }   
        }
        
        void Init(){
        
            if(player == null){
                player = GameObject.FindWithTag("Player").GetComponent<Character>();
                if(player == null)
                    Debug.LogError("no Player found in Settings or tagged with Player");
                else
                    player.isPlayer = true;
            }
            
            
            
           foreach (Transform transform in homeContainer)
               homes.Add(transform);
            foreach (Transform transform in fishingSpotContainer)
                fishingSpots.Add(transform);
            foreach (Transform transform in scavengingSpotContainer)
                scavengingSpots.Add(transform);
            foreach (Transform transform in portContainer)
                ports.Add(transform);
            foreach (Transform transform in plazaContainer)
                plazas.Add(transform);
            
            if(ports.Count == 0)
                Debug.LogError("no Ports found in Settings");
            if(homes.Count == 0)
                Debug.LogError("no Homes found in Settings");

            Debug.LogWarning("Debugging is " + (debugging ? "enabled" : "disabled"));
            
            SetValues();
        }
        
        void Update ()
        {
            if (Application.isEditor && !Application.isPlaying) {
                Init();
            }
            if(updateValuesEveryFrame)
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
	        Settings.socialRange = socialRange;
	        Settings.energyRange = energyRange;
            
            Settings.iconWidth = iconWidth;
            Settings.iconOffset = iconOffset;
            Settings.iconContainerOffset = iconContainerOffset;
            Settings.stopInteractingDistance = stopInteractingDistance;
            Settings.minDeliveryRouteRange = minDeliveryRouteRange;
            Settings.deliveryScanRange = deliveryScanRange;
            
            Settings.iconTypes = iconTypes;
            Settings.iconSprites = iconSprites;
            Settings.languageColors = languageColors;
            
            Settings.playerIconDuration = playerIconDuration;
            Settings.npcIconDuration = npcIconDuration;
            
            Settings.controller = controller;
            Settings.ports = ports;
            Settings.traders = traders;
            Settings.homes = homes;
            Settings.fishingSpots = fishingSpots;
            Settings.scavengingSpots = scavengingSpots;
            Settings.plazas = plazas;
            Settings.player = player;
            Settings.interactableIndicatorContainer = interactableIndicatorContainer;
            Settings.windowsHandler = windowsHandler;
        }
    }
}
