
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
        [Range(0f, 1f)]
        public float social = 1;

        [HideInInspector]
        public Inventory inventory;
        
        [HideInInspector]
        public LanguageExchanger languageExchanger;
        
        public SkinnedMeshRenderer modelRenderer;
        public Material material;
        
        public bool isTalking;

        [HideInInspector]
        public Character socialPartner;
        
        [HideInInspector] public AnimationController animationController;

        /**
        ** Inventory stats & Objects
        **/
        [HideInInspector] public Interactable selectedInteractable;
        
        [HideInInspector] public bool isSleeping;

        [HideInInspector] public bool wantsToTalk;
        void Awake(){
            animationController = GetComponent<AnimationController>();
            inventory = GetComponent<Inventory>();
            languageExchanger = GetComponent<LanguageExchanger>();
        }

        void Start()
        {
            if(!isPlayer){
                hunger = Random.Range(Settings.hungerRange.min, Settings.hungerRange.max);
                energy = Random.Range(Settings.energyRange.min, Settings.energyRange.max);
                social = Random.Range(Settings.socialRange.min, Settings.socialRange.max);
                name = NameGenerator.getName() + " - " + name;
            }
                        
            material = new Material(modelRenderer.material);
            modelRenderer.material = material;
            
            if(isPlayer)
                ChangeColor(QuestSystem.Instance.GetUnusedPersonColor(),10f);
        }

        void FixedUpdate()
        {
            HungerUpdate();
            TiredUpdate();
            SocialUpdate();
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
        
         private void SocialUpdate(){
            switch(isTalking)
            {
                case false: // If character is not talking
                {
                    social -= Settings.socialDecrementPerSec * Time.deltaTime;
                }
                break;

                case true: // IF character is talking
                {
                    social += Settings.socialIncrementPerSec * Time.deltaTime;
                }
                break;
            
            }
            social = Mathf.Clamp(social, Settings.socialRange.min, Settings.socialRange.max);
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
