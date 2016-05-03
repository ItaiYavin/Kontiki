
using UnityEngine;

namespace Kontiki {
    public class Character : MonoBehaviour
    {
        [Range(0f, 1f)]
        public float energy = 1;
        [Range(0, 100)]
        public float hunger = 0;

        [HideInInspector]
        public Inventory inventory;

        /**
        ** Inventory stats & Objects
        **/
        public Item selectedItem;
        
        public Boat boat;

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

        public void UseSelectedItem()
        {
            if (selectedItem != null)
            {
                selectedItem.UseItem(this);
            }
        }

        public void Sleep(bool b)
        {
            isSleeping = b;
        }

        private void OnDrawGizmosSelected()
        {
            if (selectedItem != null)
            {
                Gizmos.DrawLine(transform.position, selectedItem.transform.position);
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(selectedItem.transform.position, 0.25f);
            }
        }
    }
}
