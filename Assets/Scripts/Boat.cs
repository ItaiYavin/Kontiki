using UnityEngine;
using System.Collections;

namespace Kontiki
{
			
	public class Boat : Interactable {
		
		public sui_demo_ControllerMaster CM;
		
		public Character characterInBoat;
		
		public NavMeshAgent agent;
		
		public bool isDocked = true;
		
		public Transform target;
		public NavMeshPath path;
		
		
		bool movingForward = false;
		float endMoveForward;
		
		void Start(){
			agent = GetComponent<NavMeshAgent>();
			//body = GetComponent<Rigidbody>();
			//agent.Stop();
			path = new NavMeshPath();
			
		}
		
		public override bool Interact(Character character){
			if (character.isPlayer){
				if(CM != null){
					if (CM.currentControllerType == Sui_Demo_ControllerType.character){
						CM.currentControllerType = Sui_Demo_ControllerType.boat;
						RemoveHighlight();
						
					} else if (CM.currentControllerType == Sui_Demo_ControllerType.boat){
						CM.currentControllerType = Sui_Demo_ControllerType.character;
						CM.resetState();
					}
				}
			}
			
			if(characterInBoat == character){
				characterInBoat = null;
				character.transform.SetParent(null,true);
				
				if(!character.isPlayer){
					character.GetComponent<Pathfinder>().enabled = true;
					character.GetComponent<Pathfinder>().agent.enabled = true;
				}
			}else if(characterInBoat == null){
				characterInBoat = character;
				
				character.transform.rotation = transform.rotation;
				character.transform.SetParent(transform,true);
				character.transform.localPosition = Vector3.up * transform.localScale.y/2;
				
				if(!character.isPlayer){
					character.GetComponent<Pathfinder>().enabled = false;
					character.GetComponent<Pathfinder>().agent.enabled = false;
				}
			}
				
			return true;
		}
		
		/* Navigation through rigidbody.. does not work properly
		public void Update(){
			if(target == null)return;
			Vector3 next = Vector3.zero;
			
			if(path != null){
				int i = 1;
				while (i < path.corners.Length) {
					if (Vector3.Distance (transform.position, path.corners [i]) > 0.5f) {
						next = path.corners[i];
						break;
					}
					i++;
				}
			}else{
				next = target.position;
			}
		
			
			Vector3 delta = next - transform.position;
			float distance = delta.magnitude;
			if(distance == 0) return;
			float speed = maxSpeed;
			if(slowingDistance > Vector2.Distance(transform.position,target.position)){
				//next point is the end
				float rampedSpeed = maxSpeed * (distance / slowingDistance);
				speed = Mathf.Min(rampedSpeed,maxSpeed);
			}
			
			desiredVelocity = (speed / distance) * delta;
			desiredVelocity.y = 0;
			steering = desiredVelocity - body.velocity;
			steering.y = 0;
			
			if(body.velocity.magnitude > speed)
				body.velocity = body.velocity.normalized * speed;
			else
				body.AddForce(steering * Time.deltaTime);
			
			transform.rotation = Quaternion.LookRotation(steering,transform.up);
			
			
			target_offset = target - position 
			distance = length (target_offset) 
			ramped_speed = max_speed * (distance / slowing_distance) 
			clipped_speed = minimum (ramped_speed, max_speed) 
			desired_velocity = (clipped_speed / distance) * target_offset 
			steering = desired_velocity - velocity  
			
		}
		public void FixedUpdate(){
			if(target == null)return;
			RaycastHit hitInfo;
			if(Physics.Linecast(transform.position,target.position,out hitInfo)){
				path = new NavMeshPath();
				agent.CalculatePath(target.position, path);
				Debug.Log(hitInfo.collider);
			}else
				path = null;
		}
		**/
		
		
		
		public void GoTo(Transform target){
			if(isDocked){
				isDocked = false;
			}
			this.target = target;
			
			agent.SetDestination(target.position);
			
			/*
			RaycastHit hitInfo;
			if(Physics.Linecast(transform.position,target.position,out hitInfo)){
				path = new NavMeshPath();
				agent.CalculatePath(target.position, path);
				Debug.Log(hitInfo.collider);
			}else
				path = null;
			
			*/
			
		}
		
		
		public void Dock(Transform target){
			isDocked = true;
			transform.position = target.position;
		}
		
        
        
			
		private void OnDrawGizmosSelected()
        {
			if(target == null) return;
			if(path == null){
				Gizmos.color = Color.blue;
				Gizmos.DrawLine(transform.position,target.position);
				Gizmos.color = Color.black;
				Gizmos.DrawSphere(target.position,0.2f);
			}else{
				int i = 0;
				Vector3 lastPoint = transform.position;
				while (i < path.corners.Length) {
					
					Gizmos.color = Color.blue;
					Gizmos.DrawLine(lastPoint,path.corners[i]);
					Gizmos.color = Color.black;
					Gizmos.DrawSphere(lastPoint,1f);
					lastPoint = path.corners[i];
					i++;
				}
				
				
			}
        }
	}
}
