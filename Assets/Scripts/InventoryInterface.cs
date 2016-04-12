using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Kontiki {
    public class InventoryInterface : MonoBehaviour
    {
        [Header("Pointers")]
        public Inventory inventory;
        public GameObject buttonPrefab;

        [Header("Inventory Bar Look")]
        [Range(0,30)]
        public float marginBetweenButtons = 1;
        [Range(15,60)]
        public float sizeOfButtons = 15;

        private GameObject[] _inventoryItemButtons;


        // Use this for initialization
        void Start () {
            GenerateButtonsForInventoryBar();
        }

        // Update is called once per frame
        void FixedUpdate () {
            RefreshInventory();
        }

        private void RefreshInventory()
        {
            for (int i = 0; i < _inventoryItemButtons.Length; i++)
            {
                if(inventory.GetInventoryItem(i) != null)
                    _inventoryItemButtons[i].GetComponentInChildren<Text>().text = inventory.GetInventoryItem(i).name;
            }
        }

        private void GenerateButtonsForInventoryBar()
        {
            _inventoryItemButtons = new GameObject[inventory.inventorySize];

            for (int i = 0; i < _inventoryItemButtons.Length; i++)
            {
                // Instantiate buttons
                _inventoryItemButtons[i] = Instantiate(buttonPrefab);
                _inventoryItemButtons[i].transform.SetParent(gameObject.transform, false);
                _inventoryItemButtons[i].name = "Item";

                // Move them to correct position & size
                _inventoryItemButtons[i].GetComponent<RectTransform>().sizeDelta = new Vector2(sizeOfButtons, sizeOfButtons);
                Vector3 currentPosition = _inventoryItemButtons[i].GetComponent<RectTransform>().position;
                currentPosition.x += marginBetweenButtons + (sizeOfButtons * i);
                _inventoryItemButtons[i].GetComponent<RectTransform>().position = currentPosition;
            }
        }
    }
}
