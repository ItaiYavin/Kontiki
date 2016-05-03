using System;
using Apex.AI;
using Apex.Serialization;
using UnityEngine;

namespace Kontiki.AI
{
    /// <summary>
    /// Evaluates whether the character is tired or not (1 = tired, 0 = not tired)
    /// </summary>
    /// <seealso cref="Apex.AI.ContextualScorerBase" />
	public sealed class IsSleeping : ContextualScorerBase {
        [ApexSerialization, FriendlyName("Not", "Returns the opposite")]
        public bool not = false;

        public override float Score(IAIContext context)
        {
            AIContext ai = ((AIContext)context);

            bool b = false;

            b = ai.character.isSleeping;
            
            b = not ? !b : b;

            return b? score : 0f;
        }
	}
}