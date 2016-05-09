using UnityEngine;
using System.Collections;

namespace Kontiki
{
    public class EdibleItem : Item
    {
        [Range(0, 25)]
        public float saturation = 25f;

        override public bool UseItem(Character person)
        {
            if (person.hunger > 0)
            {
                person.hunger -= saturation;

                person.inventory.RemoveItem(this);
                Destroy(gameObject);
                
                
                return true;
            }
            else
            {
                Debug.Log(person.name + " is not hungry, " + name + " is not consumed");

                return false;
            }
        }
    }
}
