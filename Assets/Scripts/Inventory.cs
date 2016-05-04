using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace Kontiki {
    public class Inventory : MonoBehaviour {
        [Range(4, 9)]
        public int inventorySize = 4;

        public EdibleItem applePrefab;

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

        public void PutItemIntoInventoryRegardlessOfDistance(Item item){
            Item pickup = item;
            for (int i = 0; i < inventorySize; i++)
            {
                if (_inventoryItems[i] == null)
                {
                    _inventoryItems[i] = pickup;
                    item.gameObject.SetActive(false);
                    return;
                }
            }
        }

        public bool PutItemIntoInventory(Item item)
        {
            Item pickup;
            if (Vector3.Distance(item.transform.position, transform.position) < SettingsSingleton.Instance.pickupRange)
            {
                pickup = item;
                for (int i = 0; i < inventorySize; i++)
                {
                    if (_inventoryItems[i] == null)
                    {
                        _inventoryItems[i] = pickup;
                        item.gameObject.SetActive(false);
                        return true;
                    }
                }
            }
            return false;
        }

        public int CheckInventoryForSpecificItemAndReturnIndex(Item item){
            for(int i = 0; i < inventorySize; i++){
                if(_inventoryItems[i] == item){
                    return i;
                }
            }
            
            return -1;
        }

        public bool CheckInventoryForSpecificItem(Item item){
            for(int i = 0; i < inventorySize; i++){
                if(_inventoryItems[i] == item){
                    return true;
                }
            }
            
            return false;
        }

        public void GetItemFromTrade(ItemType itemType){
            //TODO when more items are in the game, a switch case has to be made with the enum
            EdibleItem item = (EdibleItem)Instantiate(applePrefab, transform.position, transform.rotation);
            
            PutItemIntoInventory(item);
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
            Item item = _inventoryItems[i];
            _inventoryItems[i] = null;
            Destroy(item);
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
