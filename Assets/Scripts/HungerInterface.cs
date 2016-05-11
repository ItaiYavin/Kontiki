using UnityEngine;
using System.Collections;

namespace Kontiki
{
    public class HungerInterface : MonoBehaviour
    {
        [HideInInspector] public Character player;

        [Header("Graphical Interface")]
        public RectTransform background;
        public RectTransform hungerProgress;

        private Vector2 originalSizeDelta;
        private Vector2 originalPosition;
        private float offsetPosY;
        
	    // Use this for initialization
	    void Start ()
	    {
            player = Settings.player;
	        originalSizeDelta = hungerProgress.sizeDelta;
	        originalPosition = hungerProgress.localPosition;

	        offsetPosY = Mathf.Abs(originalSizeDelta.y - originalPosition.y);
	    }
	
	    // Update is called once per frame
	    void Update ()
	    {
            // Set Size
	        Vector2 size = originalSizeDelta;
	        size.y = remap(player.hunger, 100, 0, 0, originalSizeDelta.y);
            hungerProgress.sizeDelta = size;
        
            // Set Position
	        Vector2 pos = originalPosition;
	        pos.y = ((size.y/2) + 30)-originalSizeDelta.y;
	        hungerProgress.localPosition = pos;
	    }

        public static float remap(float value,float low1, float high1, float low2, float high2)
        {
            return low2 + (high2 - low2) * (value - low1) / (high1 - low1);
        }
    }
}