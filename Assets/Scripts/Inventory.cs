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
        private Item[] _inventoryItems;
        private Character _character;

	    void Start () {
            _inventoryItems = new Item[inventorySize];
            _character = GetComponent<Character>();
        }

	    void Update () {
            CheckIfInventoryItemIsPressed();

        }

        public void Clean(){
            for(int i = 0; i < _inventoryItems.Length; i++){
                if(_inventoryItems[i] == null){
                    _inventoryItems[i] = null;
                }
            }
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

            _inventoryItems[i].UseItem(_character);
            _inventoryItems[i] = null;
        }

        public Item GetInventoryItem(int i) {
            return _inventoryItems[i];
        }

        public Item[] GetInventoryItems() {
            return _inventoryItems;
        }

        public bool IsInventoryFull(){
            for(int i = 0; i < inventorySize; i++){
                if(_inventoryItems[i] == null){
                    return false;
                }
            }
            return true;
        }

        public bool IsInventoryEmpty(){
            for(int i = 0; i < inventorySize; i++){
                if(_inventoryItems[i] != null){
                    return false;
                }
            }
            return true;
        }
    }
}
