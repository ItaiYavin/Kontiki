using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Kontiki.AI;
namespace Kontiki { 
    public static class Settings
    {
        /**
         * Debug Configuration
         **/
        public static bool debugging = true;
        public static bool debugQuestInfo = false;
        public static bool debugIconSystem = false;
        
        /**
         * References
         **/
        
        public static sui_demo_ControllerMaster controller;
        public static Transform interactableIndicatorContainer;
        public static WindowsHandler windowsHandler;
        public static Character player;
        public static List<Transform> homes;
        
        
        
        /**
         * Settings & Attributes of AI
         **/
        public static float scanningRange;
        public static float pickupRange;
        public static int memoryCapacity;
        public static int knownAreaSize;
        public static float hungerIncrementPerSec;
        public static float energyIncrementPerSec;
        public static float energyDecrementPerSec;
        public static RangeAttribute hungerRange    = new RangeAttribute(0, 100);
        public static RangeAttribute energyRange    = new RangeAttribute(0, 1);
        
        public static float stopInteractingDistance;
        
         /**
          * Jobs
          **/
        public static float dockingRange = 1f;
        
        public static List<Transform> ports;
        
        public static List<Trader> traders;
        
        public static List<Transform> fishingSpots;
        public static List<Transform> scavengingSpots;
        
        
        
        /**
         * Language
         **/
         public static List<IconType> iconTypes;
         public static List<Sprite> iconSprites;
         public static List<Color> languageColors;
         
         public static float iconWidth;
         public static float iconOffset;
         public static Vector3 iconContainerOffset;
         
         
         
         
         /**
          * Private !
          **/
         private static int portIndex = 0;
         private static int homeIndex = 0;
        
        
         /**
          * Methods
          **/
         
         public static Trader GetTrader(){
             return traders[Random.Range(0,traders.Count)];
         }
         public static Transform GetHome(){
             return homes[homeIndex++];
         }
         public static Transform GetPort(){
             return ports[portIndex++];
         }

    }
}