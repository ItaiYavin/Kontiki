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
        [Range(0, 30)]
        public float marginBetweenButtons = 1;
        [Range(15, 60)]
        public float sizeOfButtons = 15;
        [Range(5, 60)]
        public float marginBetweenButtonsAndBar = 5;

        [Header("Button Look")]
        [Range(0, 1)]
        public float buttonTransparency;

        public GameObject[] _inventoryItemButtons;
        
        public Sprite empty;

        private RectTransform _rt;

        // Use this for initialization
        void Start()
        {
            _rt = GetComponent<RectTransform>();
            GenerateButtonsForInventoryBar();
            float elementSize = marginBetweenButtons + sizeOfButtons;

            float width = elementSize * inventory.inventorySize + marginBetweenButtonsAndBar * 2;

            _rt.sizeDelta = new Vector2(width, marginBetweenButtonsAndBar + sizeOfButtons);
            _rt.position = new Vector2(Camera.main.pixelWidth/2, _rt.position.y + (marginBetweenButtonsAndBar + sizeOfButtons)/2);
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            RefreshInventory();
        }

        private void RefreshInventory()
        {
        	Item item;
            for (int i = 0; i < _inventoryItemButtons.Length; i++)
            {
            	item = inventory.GetInventoryItem(i);
                
                Image invImage = _inventoryItemButtons[i].GetComponent<Image>();
                if (item != null){
                    Color col = invImage.color;
                    invImage.sprite = item.sprite;		
                    invImage.color = new Color(col.r, col.g, col.b, buttonTransparency);	
                    _inventoryItemButtons[i].GetComponent<InventoryItem>().text.text = "" + (i+1);		
                }
                else 
                {
                    Color col = invImage.color;
                    invImage.color = new Color(col.r, col.b, col.g, 0.3f);
                    invImage.sprite = empty;
                    _inventoryItemButtons[i].GetComponent<InventoryItem>().text.text = "";
                }
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
                currentPosition.x += marginBetweenButtons * (i+1) + (sizeOfButtons/2) + (sizeOfButtons * i);
                _inventoryItemButtons[i].GetComponent<RectTransform>().position = currentPosition;
                _inventoryItemButtons[i].GetComponent<InventoryItem>().text.text = "";
            }
        }
    }
}
