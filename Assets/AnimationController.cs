using UnityEngine;
using System.Collections;

namespace Kontiki {
	public class AnimationController : MonoBehaviour {

        private NavMeshAgent agent;

		private Animator anim;

		void Start(){
			agent = GetComponent<NavMeshAgent>();
			anim = GetComponent<Animator>();
		}

		public void Update(){
			if(agent.remainingDistance > 1){
				Debug.Log(agent.remainingDistance);
				Run(true);
				Sit(false);
			}
			else
			{
				Run(false);
				Sit(true);
			}

		}

		public void SetSpeed(float f){
			anim.SetFloat("speed", f);
		}

		public void Run(bool b){
			anim.SetBool("isMoving", b);
		}

		public void Swim(bool b){
			anim.SetBool("isSwimming", b);
		}

		public void Talk(bool b){
			anim.SetBool("talk", b);
		}

		public void Point(bool b){
			anim.SetBool("point", b);
		}

		public void Sit(bool b){
			anim.SetBool("isSitting", b);
		}
	}
}
