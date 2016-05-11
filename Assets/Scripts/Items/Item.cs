using UnityEngine;
using UnityEngine.UI;

namespace Kontiki
{
    public abstract class Item : Interactable
    { 
        public abstract bool UseItem(Character character);     // Used by Player
        
        public override bool Interact(Character player){
            return UseItem(player);
        }
    }
}
