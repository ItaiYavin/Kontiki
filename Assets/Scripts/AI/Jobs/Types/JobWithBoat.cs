using UnityEngine;
using System.Collections;
using Kontiki;

namespace Kontiki.AI{
    public abstract class JobWithBoat : Job{      
        
        
        [Header("Boat Variables")]
        public Boat boat;
        public Transform port;
        
        public float dockingRange; //Move into Settings
        
        
		public bool isReturningToPort = false;
        
        public void DockAtPort(){
            DockAtPort(port);
        } 
        public void DockAtPort(Transform port){
            isReturningToPort = false;
            boat.Dock(port);
        }
        
        
        
        public void GoToPort(){
			isReturningToPort = true;
			boat.GoTo(port);
        }
    }
}