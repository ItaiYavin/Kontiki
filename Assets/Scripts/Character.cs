
using UnityEngine;

namespace Kontiki {
    public class Character : MonoBehaviour
    {
        
        public bool isPlayer = false;

        [Range(0f, 1f)]
        public float energy = 1;
        [Range(0, 100)]
        public float hunger = 0;

        [HideInInspector]
        public Inventory inventory;

        /**
        ** Inventory stats & Objects
        **/
        public Interactable selectedInteractable;
        
        [HideInInspector]
        public bool isSleeping;

        void Start()
        {
            inventory = GetComponent<Inventory>();
        }

        void Update()
        {
            //TODO: Check if this is NPC or not
            /*
            if (selectedItem != null)
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    selectedItem.UseItem(this);
                }
            }*/
        }

        void FixedUpdate()
        {
            HungerUpdate();

            TiredUpdate();
        }

        private void HungerUpdate()
        {
           hunger += Settings.hungerIncrementPerSec * Time.deltaTime;
           hunger = Mathf.Clamp(hunger,Settings.hungerRange.min,Settings.hungerRange.max);
        }

        private void TiredUpdate()
        {
            switch(isSleeping)
            {
                case false: // If the character is awake
                {
                    energy -= 0.001f * Time.deltaTime;
                }
                break;
                case true: // If the character is asleep
                {
                    energy += 0.003f * Time.deltaTime;
                }
                break;
            }
            
           energy = Mathf.Clamp(energy,Settings.energyRange.min,Settings.energyRange.max);
        }

        public bool HasSelected(ItemType type)
        {
            if(selectedInteractable == null)return false;
            switch (type)
            {
                case ItemType.Interactable:
                    return selectedInteractable is Interactable;
                
                case ItemType.Edible:
                    return selectedInteractable is EdibleItem;
                
                case ItemType.Item:
                    return selectedInteractable is Item;
                
            }
            return false;
        }
        
        public void SetSelected(Interactable interactable){
            selectedInteractable = interactable;
        }
        
        public void InteractWithSelected()
        {
            if (selectedInteractable != null)
            {
                selectedInteractable.Interact(this);
            }
        }

        public void Sleep(bool b)
        {
            isSleeping = b;
        }

        private void OnDrawGizmosSelected()
        {
            if (Settings.debugging)
            {
                if (selectedInteractable != null)
                {
                    Gizmos.DrawLine(transform.position, selectedInteractable.transform.position);
                    Gizmos.color = Color.red;
                    Gizmos.DrawSphere(selectedInteractable.transform.position, 0.25f);
                }

                Gizmos.DrawWireSphere(transform.position, Settings.scanningRange);
            }
        }
    }
}
