using UnityEngine;
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
        
        public static Character player;
        public static Transform[] homes;
        
        
        
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
        public static float socialIncrementPerSec;
        public static float socialDecrementPerSec;
        public static RangeAttribute socialRange    = new RangeAttribute(0, 1);
        public static RangeAttribute energyRange    = new RangeAttribute(0, 1);
        public static RangeAttribute hungerRange    = new RangeAttribute(0, 100);
        
        public static float stopInteractingDistance;
        
         /**
          * Jobs
          **/
        public static float dockingRange = 1f;
        
        public static Transform[] ports;
        
        public static Trader[] traders;
        
        
        
        /**
         * Language
         **/
         public static List<IconType> iconTypes;
         public static List<Sprite> iconSprites;
         public static List<Color> languageColors;
         
         public static float iconWidth;
         public static float iconOffset;
         public static Vector3 iconContainerOffset;

    }
}