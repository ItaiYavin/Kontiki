using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace Kontiki {
    public class Inventory : MonoBehaviour {
        [Range(4, 9)]
        public int inventorySize = 4;

        [SerializeField]
        private EdibleItem[] _inventoryItems;
        private Character _character;

	    void Start () {
            _inventoryItems = new EdibleItem[inventorySize];
            _character = GetComponent<Character>();
        }

	    void Update () {
            CheckIfInventoryItemIsPressed();

        }

        //TODO Find better name
        void CheckIfInventoryItemIsPressed()
        {
            for(int i = 1; i <= inventorySize; i++)
            {
                if(Input.GetKeyUp(""+i))
                {
                    UseInventoryItem(i-1); // Zero index start
                }
            }
        }

        public void UseInventoryItem(int i)
        {
            if (_inventoryItems[i] == null) return;

            _character.ConsumeEdibleItem(_inventoryItems[i]);
            _inventoryItems[i] = null;
        }

        public EdibleItem GetInventoryItem(int i) {
            return _inventoryItems[i];
        }

        public EdibleItem[] GetInventoryItems() {
            return _inventoryItems;
        }
    }
}
