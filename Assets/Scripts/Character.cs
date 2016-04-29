
using UnityEngine;
using System;
using Apex.AI.Components;
using System.Collections.Generic;

namespace Kontiki {
    public class Character : MonoBehaviour
    {
        public Gender gender;

        [Range(0f, 1f)]
        public float energy = 1;
        [Range(0, 100)]
        public float hunger = 0;

        public Inventory inventory;

        /**
        ** Inventory stats & Objects
        **/
        public Item selectedItem;

        void Start()
        {
            inventory = GetComponent<Inventory>();
        }


        void FixedUpdate()
        {

            if (hunger < SettingsSingleton.Instance.hungerRange.max)
            {
                hunger += SettingsSingleton.Instance.hungerIncrementPerSec * Time.deltaTime;
            }
            else
            {
                hunger = SettingsSingleton.Instance.hungerRange.max;
            }

            if (selectedItem != null)
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    selectedItem.UseItem(this);
                }
            }
        }

        public bool HasSelectedResource()
        {
            return selectedItem != null;
        }

        public void UseSelectedItem()
        {
            if (selectedItem != null)
            {
                selectedItem.UseItem(this);
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (selectedItem != null)
            {
                Gizmos.DrawLine(transform.position, selectedItem.transform.position);
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(selectedItem.transform.position, 0.25f);
            }
        }
    }
}
