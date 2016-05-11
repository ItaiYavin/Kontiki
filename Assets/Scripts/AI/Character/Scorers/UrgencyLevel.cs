using Apex.AI;
using Apex.Serialization;
using UnityEngine;
using Kontiki;

namespace Kontiki.AI
{
    /// <summary>
    /// =
    /// </summary>
    /// <seealso cref="Apex.AI.ContextualScorerBase" />
	public class UrgencyLevel : ContextualScorerBase {
        
        [ApexSerialization, FriendlyName("level", "")]
        public Kontiki.UrgencyLevel level;

        public override float Score(IAIContext context)
        {
            AIContext ai = ((AIContext)context);
            
            bool b = Settings.urgencyLevel == level;
            
            return b ? score : 0f;
        }
	}
}