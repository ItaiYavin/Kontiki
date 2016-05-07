
using UnityEngine;
using System.Collections;

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
        
        [HideInInspector]
        public IconSystem iconSystem;
        
        public SkinnedMeshRenderer modelRenderer;
        private Material material;
        
        

        /**
        ** Inventory stats & Objects
        **/
        public Interactable selectedInteractable;
        
        [HideInInspector]
        public bool isSleeping;
        
        public Character characterInteracting;

        void Start()
        {
            inventory = GetComponent<Inventory>();
            iconSystem = GetComponent<IconSystem>();
            
            material = new Material(modelRenderer.material);
            modelRenderer.material = material;
            
            if(isPlayer)
                ChangeColor(QuestSystem.Instance.GetUnusedColor(),10f);
        }

        void FixedUpdate()
        {
            HungerUpdate();
            TiredUpdate();
            
            if(characterInteracting != null){
                float distance = Vector3.Distance(transform.position,characterInteracting.transform.position);
                
                if(distance > Settings.stopInteractingDistance){
                    characterInteracting.characterInteracting = null;
                    characterInteracting = null;
                    iconSystem.Clear();
                }else{
                    Vector3 lookDirection = characterInteracting.transform.position;
                    lookDirection.y = transform.position.y;
                    transform.LookAt(lookDirection);
                }
            }
        }

        private void HungerUpdate()
        {
           hunger += Settings.hungerIncrementPerSec * Time.deltaTime;
           hunger = Mathf.Clamp(hunger, Settings.hungerRange.min, Settings.hungerRange.max);
        }

        private void TiredUpdate()
        {
            switch(isSleeping)
            {
                case false: // If the character is awake
                {
                    energy -= Settings.energyDecrementPerSec * Time.deltaTime;
                }
                break;
                case true: // If the character is asleep
                {
                    energy += Settings.energyIncrementPerSec * Time.deltaTime;
                }
                break;
            }
            
           energy = Mathf.Clamp(energy, Settings.energyRange.min, Settings.energyRange.max);
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
        
        public void ChangeColor(Color newColor, float duration){
            StartCoroutine(Routine_ChangeColor(newColor,duration));
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
        
        IEnumerator Routine_ChangeColor(Color endColor, float duration){
            Color startColor = material.color;
            
            float startTime = Time.time;
            float endTime = startTime + duration;
            
            while(endTime > Time.time){
                float t = (Time.time - startTime)/duration;
                material.color = Color.Lerp(startColor, endColor, t);
                yield return null;
            }
            
            material.color = endColor;
        }
    }
}
