using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Kontiki.AI{
    public class DeliveryMan : Job{      
        public Character origin;
        public Character destination;
        
        public bool hasRoute = false;
        public bool isOnRoute = false;
        
        
        private Character character;
        
        void Start(){
            character = GetComponent<Character>();            
        }
        
        public void CreateRoute(){
            
            Collider[] colliders = Physics.OverlapSphere(transform.position, Settings.deliveryScanRange, 1 << 11);
            List<Character> characters = new List<Character>();
            for (int i = 0; i < colliders.Length; i++){
                Character c = colliders[i].gameObject.GetComponent<Character>();
                
                if(c != null && !c.isPlayer && c != character){
                    Job job = c.GetComponent<Job>();
                    if(job == null || !(job is JobWithBoat))
                        characters.Add(c);
                }
            }
            int index = Random.Range(0, characters.Count);
            origin = characters[index];
            characters.RemoveAt(index);
            
            
            for (int i = characters.Count - 1; i > 0; i--)
            {
                float d = Vector3.Distance(origin.transform.position, characters[i].transform.position);
                
                if(d < Settings.minDeliveryRouteRange)
                    characters.RemoveAt(i);
            }
            
            
            index = Random.Range(0, characters.Count);
            destination = characters[index];
            characters.RemoveAt(index);
            
            hasRoute = true;
        }
    }
}