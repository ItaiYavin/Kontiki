using UnityEngine;

namespace Kontiki
{
    public abstract class Item : Interactable
    { 
        public abstract bool UseItem(Character character);     // Used by Player
        
        public override bool Interact(Character character){
            return UseItem(character);
        }
    }
}
