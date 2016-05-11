using UnityEngine;
using System.Collections;

namespace Kontiki.AI{
    public class Trader : JobWithBoat{
        
        public Transform[] ports;
        private int index = 0;
        public bool isTrading = true;
        
        public float minTradingTime = 3f;
        public float maxTradingTime = 6f;
        
        void Start(){
            if(ports.Length == 0 && port == null){
                Debug.LogError("AI - Trader Must have at least one port");
                return;
            }
            
            if(port == null) 
                port = ports[index];
                
            if(port != null && ports.Length == 0) {
                ports = new Transform[1] {port};
            }
            for (int i = 0; i < ports.Length; i++)
            {
                ports[i].name = gameObject.name + "'s Port " + (i + 1);
            }
            
            GameObject g = Instantiate(boatPrefab, port.transform.position, port.transform.rotation) as GameObject;
            boat = g.GetComponent<Boat>();
            boat.name = gameObject.name + "'s Boat";
                
            if(isTrading){
                if(!boat.isDocked){
                    DockAtPort();
                }
                StartCoroutine(Routine_StopTrading(Random.Range(
                    minTradingTime,
                    maxTradingTime
                )));
            }
            
            GetComponent<Pathfinder>().enabled = false;
            GetComponent<NavMeshAgent>().enabled = false; 
                
            if(boat.characterInBoat == null){
                if(transform.IsChildOf(boat.transform)){
                    boat.characterInBoat = GetComponent<Character>();
                    transform.localPosition = Vector3.up * boat.transform.localScale.y/2;
				
                }else
                    boat.Interact(GetComponent<Character>());
                
            }
        }   
        
        public override void DockAtPort(Transform port){
            isReturningToPort = false;
            boat.Dock(port);
            StartTrading();
        }
        
        
         public override void GoToPort(){
            if(ports.Length == 1)
                StartTrading();

			isReturningToPort = true;
            
            index++;
            if(index == ports.Length)
                index = 0;
            port = ports[index];
            boat.GoTo(port);
        }
        
        public void StartTrading(){
            isTrading = true;
            StartCoroutine(Routine_StopTrading(Random.Range(
                minTradingTime,
                maxTradingTime
            )));
        }
        
         IEnumerator Routine_StopTrading(float length){
            yield return new WaitForSeconds(length);
            isTrading = false;
        }
    }
}