using UnityEngine;
using System.Collections;

namespace Kontiki {
	public class AnimationController : MonoBehaviour {

        private NavMeshAgent agent;
		public Animator anim;

	    private sui_demo_animCharacter animController;
	    private bool isPlayer;

		void Start(){
			agent = GetComponent<NavMeshAgent>();

		    animController = GetComponent<sui_demo_animCharacter>();
		    if (animController != null)
		        isPlayer = true;
		    else
		        isPlayer = false;
		}

	    public void Update()
	    {
	        switch (isPlayer)
	        {
                case true:
	            {
	                if (animController.isRunning || animController.isSprinting || animController.isWalking)
	                {
	                    Run(true);
                        if(animController.isWalking) SetSpeed(0.5f);
                        if(animController.isSprinting) SetSpeed(1f);
	                }
	                else
	                {
                        Run(false);
                    }

                    if(animController.isInWater)
                        Swim(true);
                    else 
                        Swim(false);
	            } break;
                case false:
	            {
                    if (agent.enabled)
                    {
                        if (agent.remainingDistance > 1)
                        {
                            Run(true);
                            Sit(false);
                        }
                        else
                        {
                            Run(false);
                            Sit(true);
                        }
                    }
                    else
                    {
                        Run(false);
                        Sit(true);
                    }
                } break;
	        }


	    }

	    public
		    void SetSpeed 
		    (float
		    f)
		    {
		        anim.SetFloat("speed", f);
		    }

		public
		    void Run 
		    (bool
		    b)
		    {
		        anim.SetBool("isMoving", b);
		    }

		public
		    void Swim 
		    (bool
		    b)
		    {
		        anim.SetBool("isSwimming", b);
		    }

		public
		    void Talk 
		    (bool
		    b)
		    {
		        anim.SetBool("talk", b);
		    }

		public
		    void Point 
		    (bool
		    b)
		    {
		        anim.SetBool("point", b);
		    }

		public
		    void Sit 
		    (bool
		    b)
		    {
		        anim.SetBool("isSitting", b);
		    }
		}
	}
