using Apex.AI;
using Apex.Serialization;
using UnityEngine;
using Kontiki;
using System.Collections;

namespace Kontiki.AI
{
    /// <summary>
    /// Scorer for whether character has edible in memory list
    /// </summary>
    /// <seealso cref="Apex.AI.ContextualScorerBase" />
	public class IsJobType : ContextualScorerBase {
		

		[ApexSerialization, FriendlyName("Not", "Invert return value")]
        public bool not = false;
        
		[ApexSerialization, FriendlyName("Job Type", "Type to check whether ai is or not")]
        public JobType type = JobType.Fisher;

        public override float Score(IAIContext context){
            Job job = ((AIContext)context).job;
            bool b = false;
           
            switch (type)
            {
                case JobType.Fisher:{
                    b = job is Fisher;
                }break;
                case JobType.Trader:{
                    b = job is Trader;
                }break;
            }
           
                
            if(not) b = !b;

            return b ? score : 0f;
        
        }
	}
}
