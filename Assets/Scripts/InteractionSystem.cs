﻿using UnityEngine;
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

		void Start () {
			controller = Settings.controller;
			interactableIndicatorContainer = Settings.interactableIndicatorContainer;
			windowsHandler = Settings.windowsHandler;
			_player = GetComponent<Character>();
            _inventory = GetComponent<Inventory>();
			_freeIndicators = new List<InteractableIndicator>();
			_occupiedIndicators = new List<InteractableIndicator>();
		}
		void Update () {
			_isDown = Input.GetKey(_key);
			controller.interactionButtonDown = _isDown;
			interactableIndicatorContainer.gameObject.SetActive(_isDown);
			if(_isDown){
				
				//ScanForInteractableIndicators();
				//UpdateInteractableIndicators();
				
				CheckMouseHoveringOverInteractable();
				if(_lastTarget != null){
					float distance = Vector3.Distance(_lastTarget.transform.position,transform.position);
					if(distance < Settings.pickupRange && Input.GetMouseButtonDown(0))
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
							
							bool hasQuest = false;	
												
							Quest[] acceptedQuests = QuestSystem.Instance.acceptedQuests.ToArray();
							for (int i = 0; i < acceptedQuests.Length; i++)
							{
								Fetch quest = (Fetch)acceptedQuests[i];
								bool playerHasObjective = _player.inventory.CheckInventoryForSpecificItem(quest.objective);

								if(quest.origin == target.character){
									//is talking to the quest origin 
									if(playerHasObjective && _player.inventory.RemoveItem(quest.objective)){
									//Player has objective and player has given objective
										QuestSystem.Instance.FreeUsedPersonColor(quest.colorOrigin);
										target.character.ChangeColor(Color.white,10f);
										quest.FinishQuest(_player.inventory);
										Language.QuestFinished(target.character.languageExchanger, _player.languageExchanger, quest);
										hasQuest = true;
									}
								}
							}
							
							if(!hasQuest){
								Language.DoYouWantToStartConversation(_player.languageExchanger, target);
							}
                        }

							
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