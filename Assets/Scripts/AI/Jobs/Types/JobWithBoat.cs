using UnityEngine;
using System.Collections;
using Kontiki;

namespace Kontiki.AI{
    public abstract class JobWithBoat : Job{      
        
        
        [Header("Boat Variables")]
        public Boat boat;
        public Transform port;
        
		public bool isReturningToPort = false;
        
        
        void Start(){
            moveBoatToPort();
        }
        
        public virtual void moveBoatToPort(){moveBoatToPort(port);}
        public virtual void moveBoatToPort(Transform port){
vvvvvvv            boat.transform.position = port.position;
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