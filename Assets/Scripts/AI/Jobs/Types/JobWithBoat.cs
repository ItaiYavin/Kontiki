using UnityEngine;
using System.Collections;
using Kontiki;

namespace Kontiki.AI{
    public abstract class JobWithBoat : Job{      
        
        
        [Header("Boat Variables")]
        public Boat boat;
        public Transform port;
        
        public float dockingRange = 1f; //Move into Settings
        
        
		public bool isReturningToPort = false;
        
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