using Apex.AI;
using Apex.Serialization;
using UnityEngine;
using Kontiki;

namespace Kontiki.AI
{
    /// <summary>
    /// Scorer for whether character has an edible resource selected
    /// </summary>
    /// <seealso cref="Apex.AI.ContextualScorerBase" />
	public class CanGetToTrader : ContextualScorerBase {

        [ApexSerialization, FriendlyName("Not", "Returns the opposite")]
        public bool not = false;

        public override float Score(IAIContext context){
        	bool b = false;
			AIContext ai = (AIContext) context;
        	Pathfinder pathfinder = ((AIContext)context).pathfinder;
            
            if(ai.baseRoutine.trader == null)
                return 0f;
                
            NavMeshPath path = new NavMeshPath();
            pathfinder.agent.CalculatePath(ai.baseRoutine.trader.transform.position, path);
            b = path.status == NavMeshPathStatus.PathComplete;
        	
            if(not)
        		b = !b;

        	return b ? score : 0f;
        }
	}
}