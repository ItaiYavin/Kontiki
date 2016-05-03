
using UnityEngine;

namespace Kontiki {
    public class Character : MonoBehaviour
    {
        
        public bool isPlayer = false;
        public Gender gender;

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
        
        
        public Boat boat;
        
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
            if (hunger < SettingsSingleton.Instance.hungerRange.max)
            {
                hunger += SettingsSingleton.Instance.hungerIncrementPerSec;
            }
            else
            {
                hunger = SettingsSingleton.Instance.hungerRange.max;
            }
        }

        private void TiredUpdate()
        {
            switch(isSleeping)
            {
                case false: // If the character is awake
                {
                    energy -= 0.001f;
                }
                break;
                case true: // If the character is asleep
                {
                    energy += 0.003f;
                }
                break;
            }
        }

        public bool HasSelectedResource()
        {
            return selectedItem != null;
        }

        public bool HasSelected()
        {
            return selectedInteractable != null;
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
            if (selectedInteractable != null)
            {
                Gizmos.DrawLine(transform.position, selectedInteractable.transform.position);
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(selectedInteractable.transform.position, 0.25f);
            }
        }
    }
}
