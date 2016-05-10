using Apex.AI;
using Kontiki;
using UnityEngine;

namespace Kontiki.AI
{
    /// <summary>
    /// Goes to the specified destination
    /// </summary>
    /// <seealso cref="Apex.AI.ActionBase" />

	public class GoToPartner : ActionBase {
		public override void Execute(IAIContext context)
        {
            
            AIContext ai = ((AIContext)context);
            
            if(ai.debugAI_Character){
                Debug.Log("Going to partner");

                Debug.Log("0");
                
                GameObject temp = new GameObject();
                temp.AddComponent<Delay>();
                Delay delay = temp.GetComponent<Delay>();
                delay.StartDelay(1f);            
                
                Debug.Log("1");
            }

            if(ai.character.socialPartner != null){
                ai.pathfinder.GoTo(ai.character.socialPartner.transform);
                
            }
        }
	}
}