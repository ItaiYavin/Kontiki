using UnityEngine;
using System.Collections;

namespace Kontiki
{
    public sealed class Trigger : MonoBehaviour{
        
        private Renderer highlight;

        void Start(){
            highlight = GetComponent<Renderer>();
        }
        
        void FixedUpdate(){
            
        }
    }   
}