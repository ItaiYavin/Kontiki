using UnityEngine;

/**
 * Singleton Template from http://clearcutgames.net/home/?p=437
 **/

namespace Kontiki { 
    public static class Settings
    {
        public static bool debugging                = true;
        /**
         * Settings & Attributes of AI
         **/
        public static float scanningRange           = 1;
        public static float pickupRange             = 1;
        public static int memoryCapacity            = 200;
        public static int knownAreaSize             = 100;
        public static float hungerIncrementPerSec   = 0.1f;
        public static RangeAttribute hungerRange    = new RangeAttribute(0, 100);
        public static RangeAttribute energyRange    = new RangeAttribute(0, 1);

    }
}