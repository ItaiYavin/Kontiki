
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
        
        
        void Start()
        {
            inventory = GetComponent<Inventory>();
        }


        void FixedUpdate()
        {

            if (hunger < SettingsSingleton.Instance.hungerRange.max)
            {
                hunger += SettingsSingleton.Instance.hungerIncrementPerSec * Time.deltaTime;
            }
            else
            {
                hunger = SettingsSingleton.Instance.hungerRange.max;
            }
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
