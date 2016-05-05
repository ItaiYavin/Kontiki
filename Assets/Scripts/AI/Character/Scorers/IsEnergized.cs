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
    public sealed class IsEnergized : ContextualScorerBase
    {
        [ApexSerialization, FriendlyName("Threshold", "Threshold(0-1) for energy(0-1) where the score will be 0")]
        public float threshold = 0;

        public override float Score(IAIContext context)
        {
            AIContext ai = ((AIContext)context);
            /*
            float v = Mathf.Max((ai.character.energy / 1 - threshold), 0) / (1 - threshold);
            */
            float v = Mathf.Max(((ai.character.energy / 1) - threshold), 0) / (1 - threshold);

            //TODO Make this more flexible
            return v * score;
        }
    }
}
