using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Kontiki { 
    public class InventoryInterface : MonoBehaviour {
        public Inventory inventory;
        public GameObject buttonPrefab;

        private GameObject[] _inventoryItemButtons;


	    // Use this for initialization
	    void Start () {
            _inventoryItemButtons = new GameObject[inventory.inventorySize];

            for(int i = 0; i < _inventoryItemButtons.Length; i++)
            {
                _inventoryItemButtons[i] = Instantiate(buttonPrefab);
                _inventoryItemButtons[i].transform.SetParent(gameObject.transform, false);
                _inventoryItemButtons[i].name = "Item";
                //_inventoryItemButtons[i].GetComponent<RectTransform>().position;
            }
        }
	
	    // Update is called once per frame
	    void FixedUpdate () {
	    
	    }
    }
}
