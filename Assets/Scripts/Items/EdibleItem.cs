using UnityEngine;
using System.Collections;

namespace Kontiki
{
    public class EdibleItem : Item
    {
        [Range(0, 25)]
        public float saturation = 0.1f;

        override public bool UseItem(Character person)
        {
            if (person.hunger > 0)
            {

                person.hunger = Mathf.Max(0f, person.hunger - saturation);

                DestroyImmediate(gameObject, true);

                return true;
            }
            else
            {
                Debug.Log(name + " is not hungry, " + name + " is not consumed");

                return false;
            }
        }
    }
}
