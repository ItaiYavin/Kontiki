﻿using UnityEngine;
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
                bool target = false;
                bool selected = false;

                person.hunger = Mathf.Max(0f, person.hunger - saturation);

                if(transform == person.target){
                    target = true;
                }
                if(gameObject.GetComponent<Item>() == person.selectedItem){
                    selected = true;
                }

                DestroyImmediate(gameObject, true);
                person.GetInventory().Clean();
                person.CleanMemory();
                if(target) person.target = null;
                if(selected) person.selectedItem = null;

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
