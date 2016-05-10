using UnityEngine;
using System.Collections;
using Kontiki;

namespace Kontiki.AI{
    public abstract class JobWithBoat : Job{      
        
        [Header("Boat Variables")]
        
        [HideInInspector] public Boat boat;
        [HideInInspector] public GameObject boatPrefab;
        [HideInInspector] public Transform port;
        
		public bool isReturningToPort = false;
        
        
        void Start(){
            boatPrefab = Settings.boatPrefab;

            port = Settings.GetPort();
            port.name = gameObject.name + "'s Port";
            GameObject g = Instantiate(boatPrefab, port.transform.position, port.transform.rotation) as GameObject;
            boat = g.GetComponent<Boat>();
            boat.name = gameObject.name + "'s Boat";
        }
        
        public virtual void moveBoatToPort(){moveBoatToPort(port);}
        
        public virtual void moveBoatToPort(Transform port){
            boat.transform.position = port.position;
        }
        
        public virtual void DockAtPort(){
            DockAtPort(port);
        } 
        
        public virtual void DockAtPort(Transform port){
            isReturningToPort = false;
            boat.Dock(port);
        }
                
        public virtual void GoToPort(){
			isReturningToPort = true;
			boat.GoTo(port);
        }
    }
}