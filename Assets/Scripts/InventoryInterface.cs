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
        public Sprite empty, apple, water;

        public GameObject[] _inventoryItemButtons;

        private RectTransform _rt;

        // Use this for initialization
        void Start()
        {
            _rt = GetComponent<RectTransform>();
            GenerateButtonsForInventoryBar();

            _rt.sizeDelta = new Vector2(_rt.rect.x, marginBetweenButtonsAndBar + sizeOfButtons);
            _rt.position = new Vector2(_rt.position.x, _rt.position.y + (marginBetweenButtonsAndBar + sizeOfButtons)/2);
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
                if (item != null){
            		if(item is EdibleItem){
            			Color col = _inventoryItemButtons[i].GetComponent<Image>().color;
                		_inventoryItemButtons[i].GetComponent<Image>().sprite = apple;		
                		_inventoryItemButtons[i].GetComponent<Image>().color = new Color(col.r, col.g, col.b, buttonTransparency);			
            		}
                }
                else 
                {
                    Color col = _inventoryItemButtons[i].GetComponent<Image>().color;
                    _inventoryItemButtons[i].GetComponent<Image>().color = new Color(col.r, col.b, col.g, 0.3f);
                    _inventoryItemButtons[i].GetComponent<Image>().sprite = empty;
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


                // Set OnClick function
                int k = i; // Doing weird magic in the name of Itai Yavin
                _inventoryItemButtons[i].GetComponent<Button>().onClick.AddListener(delegate { inventory.UseInventoryItem(k); });
            }
        }
    }
}
