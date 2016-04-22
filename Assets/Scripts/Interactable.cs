using UnityEngine;
using System.Collections;

namespace Kontiki
{
        
    public abstract class Interactable : MonoBehaviour {
        
        public Renderer hightlightRenderer;
        
        void Awake(){
            if(hightlightRenderer == null)
                hightlightRenderer = GetComponent<Renderer>();
            RemoveHighlight();
        }
        
        public void Highlight(Color color){
            if(hightlightRenderer != null)
                hightlightRenderer.material.SetColor("_TintColor",color);
        }
        public void RemoveHighlight(){
            if(hightlightRenderer != null)
                hightlightRenderer.material.SetColor("_TintColor",new Color(0,0,0,0));
        }
        
        public abstract void Interact(Character character);
    }
}
