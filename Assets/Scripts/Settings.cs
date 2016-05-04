using UnityEngine;

/**
 * Singleton Template from http://clearcutgames.net/home/?p=437
 **/

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
        Job
        **/
        public static float dockingRange = 1f;

    }
}