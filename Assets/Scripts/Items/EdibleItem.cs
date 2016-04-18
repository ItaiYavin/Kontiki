using UnityEngine;
using System.Collections;

namespace Kontiki
{
    public class EdibleItem : Item
    {
        [Range(0, 1)]
        public float saturation = 0.1f;

        override public bool UseItem(Character person)
        {
            if (person.hunger > 0)
            {
                Debug.Log(name + " have consumed " + name + " and lost " + saturation + " in hunger");

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
