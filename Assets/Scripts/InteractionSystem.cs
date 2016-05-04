using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace Kontiki{
	
	public class InteractionSystem : MonoBehaviour {
		
		
		[SerializeField] private bool _isDown = false;
		
		[SerializeField] private KeyCode _key = KeyCode.Space;
		
		private Character _character;
        private Inventory _inventory;
        private Interactable _lastTarget;
		
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
		
		public sui_demo_ControllerMaster controller;
		
		public GameObject prefab_interactableIndicator;
		public GameObject container;
		
		
		private List<InteractableIndicator> _occupiedIndicators;
		private List<InteractableIndicator> _freeIndicators;
		

		void Start () {
			_character = GetComponent<Character>();
            _inventory = GetComponent<Inventory>();
			_freeIndicators = new List<InteractableIndicator>();
			_occupiedIndicators = new List<InteractableIndicator>();
		}
		
		void Update () {
			_isDown = Input.GetKey(_key);
			controller.interactionButtonDown = _isDown;
			container.SetActive(_isDown);
			if(_isDown){
				
				ScanForInteractableIndicators();
				UpdateInteractableIndicators();
				
				CheckMouseHoveringOverInteractable();
				if(_lastTarget != null){
					float distance = Vector3.Distance(_lastTarget.transform.position,transform.position);
					if(distance < Settings.pickupRange && Input.GetMouseButtonDown(0)){
						
						
						bool successful = false;
						
						if(_lastTarget is Item)
							successful = _inventory.PutItemIntoInventory((Item)_lastTarget);
						else
							successful = _lastTarget.Interact(_character);
							
						if(successful){
							InteractableIndicator indicator = _lastTarget.indicator;
							_occupiedIndicators.Remove(indicator);
							indicator.gameObject.SetActive(false);
							_freeIndicators.Add(indicator);
							
							_lastTarget.RemoveHighlight();
							_lastTarget = null;
						}
						
					}
				}
			}else if(_lastTarget != null){
				_lastTarget.RemoveHighlight();
				_lastTarget = null;
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
					//unregistered
					InteractableIndicator indicator;
					if(_freeIndicators.Count > 0){
						//repurpose
						indicator = _freeIndicators[0];
						
						_freeIndicators.RemoveAt(0);
					}else{
						//isntantiate
						GameObject g = Instantiate(prefab_interactableIndicator);
						g.transform.SetParent(container.transform, false);
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
		
		
        public void CheckMouseHoveringOverInteractable(){ //TODO: FIND BETTER METHOD (THIS METHOD SETS THE OUTLINE TO 0)
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Interactable interactable;
            if (Physics.Raycast(ray, out hit)){
				interactable = hit.transform.GetComponent<Interactable>();
				if(interactable != null){
					
					if(_lastTarget == null){					
						_lastTarget = interactable;
                        
                        interactable.Highlight(new Color(0.5f,0,0,0.3f));
						return;
					}else if(_lastTarget == interactable){
						float distance = Vector3.Distance(_lastTarget.transform.position , transform.position);
						
						if(distance < Settings.pickupRange){
                       		interactable.Highlight(new Color(0,1f,0,0.3f));
						}

                    }else if(_lastTarget != interactable){
						_lastTarget.RemoveHighlight();

                        interactable.Highlight(new Color(0.5f,0,0,0.3f));
						_lastTarget = interactable;
					}
				}
				else{
					if(_lastTarget != null){
						_lastTarget.RemoveHighlight();
						_lastTarget = null;
					}
                }
            }
        }
	}

}