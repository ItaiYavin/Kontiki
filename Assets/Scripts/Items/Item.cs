using UnityEngine;

namespace Kontiki
{
    public abstract class Item : Interactable
    {
        public abstract bool UseItem(Character person);
        
       public override void Interact(Character character){
           UseItem(character);
       }
    }
}
