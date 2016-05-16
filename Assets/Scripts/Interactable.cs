using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Kontiki
{
        
    public abstract class Interactable : MonoBehaviour {
                
        
        public InteractableIndicator indicator;
        public Sprite sprite;

        public abstract bool Interact(Character player);
    }
}
