using UnityEngine;

namespace Kontiki { 
    public static class Settings
    {
        public static bool debugging;
        /**
         * Settings & Attributes of AI
         **/
        public static float scanningRange;
        public static float pickupRange;
        public static int memoryCapacity;
        public static int knownAreaSize;
        public static float hungerIncrementPerSec;
        public static RangeAttribute hungerRange    = new RangeAttribute(0, 100);
        public static RangeAttribute energyRange    = new RangeAttribute(0, 1);
         /**
          * Jobs
          **/
        public static float dockingRange = 1f;

    }
}