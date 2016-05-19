using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace Kontiki{
	
	public class InteractionSystem : MonoBehaviour
	{
        [Header("Keys")]
		[SerializeField] private bool _isDown = false;
		
		[SerializeField] private KeyCode _key = KeyCode.Space;
		
		private Character _player;
        private Inventory _inventory;
        private Interactable _lastTarget;

        [Header("Setting")]
        [SerializeField] private float _scanRange = 100;
		
		[SerializeField] private float _minIndicatorSize = .2f;
		[SerializeField] private float _maxIndicatorSize = 1;
		
		[SerializeField] private float _indicatorMaxSizeRange = 1;
		[SerializeField] private float _indicatorMinSizeRange = 10;
		
		[SerializeField] private float _minIndicatorAlpha = 0;
		[SerializeField] private float _maxIndicatorAlpha = .5f;
		
		[SerializeField] private float _indicatorTranparentRange = 10;
		[SerializeField] private float _indicatorOpaqueRange = 1;
			
		[SerializeField] private LayerMask _interactableLayer;

        private sui_demo_ControllerMaster controller;

        [Header("Pointers")]
        public GameObject prefab_interactableIndicator;
		public Transform interactableIndicatorContainer;
        public WindowsHandler windowsHandler;

        private List<InteractableIndicator> _occupiedIndicators;
		private List<InteractableIndicator> _freeIndicators;
		
		public bool menuOpen = false;
		
		public Texture2D cursorCanReach;
		public Texture2D cursorIsPerson;
		public Texture2D cursorCannotReach;

		void Start () {
			controller = Settings.controller;
			interactableIndicatorContainer = Settings.interactableIndicatorContainer;
			windowsHandler = Settings.windowsHandler;
			_player = GetComponent<Character>();
            _inventory = GetComponent<Inventory>();
			_freeIndicators = new List<InteractableIndicator>();
			_occupiedIndicators = new List<InteractableIndicator>();
			
			Cursor.SetCursor(cursorCannotReach, Vector2.zero, CursorMode.Auto);
		}
		void Update () {
            /*if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
                return;
            }*/

            if (menuOpen)
				Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
			
			_isDown = Input.GetKey(_key);
			controller.interactionButtonDown = _isDown;
			interactableIndicatorContainer.gameObject.SetActive(_isDown);
			if(_isDown){
				
				//ScanForInteractableIndicators();
				//UpdateInteractableIndicators();
				CheckMouseHoveringOverInteractable();
				if(_lastTarget != null){
					float distance = Vector3.Distance(_lastTarget.transform.position,transform.position);
					if(distance < Settings.pickupRange && Input.GetMouseButtonDown(0) && Settings.player.languageExchanger.speakingTo == null)
					{
						bool successful = false;

                        if (_lastTarget is Item)
                        {
                            successful = _inventory.PutItemIntoInventory((Item)_lastTarget);
                        }
                        if (_lastTarget is AIComponentContainer)
                        {
                            //_lastTarget.Interact(_player); 
							LanguageExchanger target = _lastTarget.GetComponent<LanguageExchanger>();
							
							Language.DoYouWantToStartConversation(_player.languageExchanger, target);
							_player.languageExchanger.speakingTo = target;
						
                        }

							
						if(successful){
							InteractableIndicator indicator = _lastTarget.indicator;
							_occupiedIndicators.Remove(indicator);
							indicator.gameObject.SetActive(false);
							_freeIndicators.Add(indicator);
							
							_lastTarget = null;
						}
						
					}
				}
			}else if(_lastTarget != null){
				_lastTarget = null;
			}
			
			if(menuOpen || _isDown){
				
				Cursor.visible = true;
				Cursor.lockState = CursorLockMode.Confined;
			}else{
				Cursor.visible = false;
				Cursor.lockState = CursorLockMode.Locked;
			}
				
		}
		
		public void UpdateInteractableIndicators(){
			for (int i = 0; i < _occupiedIndicators.Count; i++)
			{
				// update position, scale, transparency 
				
				InteractableIndicator indicator = _occupiedIndicators[i];
				Interactable interactable = indicator.interactable;
				
				float distance = (interactable.transform.position - transform.position).magnitude;
				
				Vector3 screenPoint = Camera.main.WorldToScreenPoint(interactable.transform.position + indicator.offset);
				
				indicator.transform.position = screenPoint;
				
				
				
				float size = Mathf.Lerp(_minIndicatorSize, _maxIndicatorSize, (distance - _indicatorMinSizeRange) / (_indicatorMaxSizeRange - _indicatorMinSizeRange));

				Color color = indicator.image.color;
				float a = Mathf.Lerp(_maxIndicatorAlpha, _minIndicatorAlpha, (distance - _indicatorOpaqueRange) / (_indicatorTranparentRange - _indicatorOpaqueRange));
				color.a = a;
				indicator.image.color = color;
				indicator.transform.localScale = new Vector3(size,size,1);
			}
		}
		
		public void ScanForInteractableIndicators(){
			Collider[] colliders = Physics.OverlapSphere(transform.position, _scanRange, _interactableLayer.value);
				
			for (int i = 0; i < colliders.Length; i++)
			{
				Interactable interactable = colliders[i].GetComponent<Interactable>();
				
				if(interactable.indicator == null){
					//unregistered interactable
					InteractableIndicator indicator;
					if(_freeIndicators.Count > 0){
						//repurpose indicator
						indicator = _freeIndicators[0];
						
						_freeIndicators.RemoveAt(0);
					}else{
						//instantiate indicator
						GameObject g = Instantiate(prefab_interactableIndicator);
						g.transform.SetParent(interactableIndicatorContainer, false);
						indicator = g.GetComponent<InteractableIndicator>();
					}
					
					indicator.gameObject.SetActive(true);
					interactable.indicator = indicator;
					indicator.interactable = interactable;
					
					indicator.image.sprite = interactable.sprite;
					
					
					
					_occupiedIndicators.Add(indicator);
				}
			}
		}
		
		
        public void CheckMouseHoveringOverInteractable(){
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Interactable interactable;
            if (Physics.Raycast(ray, out hit)){
				interactable = hit.transform.GetComponent<Interactable>();
				if(interactable != null){
                    if (_lastTarget == null){					
						_lastTarget = interactable;
						Cursor.SetCursor(cursorIsPerson, Vector2.zero, CursorMode.Auto);
                        
						return;
					}else if(_lastTarget == interactable){
						float distance = Vector3.Distance(_lastTarget.transform.position , transform.position);
						
						if(distance < Settings.pickupRange){
							Cursor.SetCursor(cursorCanReach, Vector2.zero, CursorMode.Auto);
						}

                    }else if(_lastTarget != interactable){
						Cursor.SetCursor(cursorCannotReach, Vector2.zero, CursorMode.Auto);
						_lastTarget = interactable;
					}
				}
				else{
					if(_lastTarget != null){	 
						_lastTarget = null;
					}
					Cursor.SetCursor(cursorCannotReach, Vector2.zero, CursorMode.Auto);
                }
            }
        }
		
	}

}