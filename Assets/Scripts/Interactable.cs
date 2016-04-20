using UnityEngine;
using System.Collections;

namespace Kontiki
{
        
    public abstract class Interactable : MonoBehaviour {
        
        public Renderer renderer;
        
        void Awake(){
            if(renderer == null)
                renderer = GetComponent<Renderer>();
        }
        
        public void Highlight(float thickness){Highlight(thickness,new Color(0,0,0,1f));}
        public void Highlight(float thickness, Color color){
            renderer.material.SetFloat("_Outline", thickness);
            renderer.material.SetColor("_OutlineColor", color);
        }
        public void RemoveHighlight(){
            renderer.material.SetFloat("_Outline", 0f);
        }
        
        public abstract void Interact(Character character);
    }
}
