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
    public sealed class Energy : ContextualScorerBase
    {
        [ApexSerialization, FriendlyName("Threshold", "Threshold(0-1) for energy(0-max) where the score will be 0")]
        public float threshold = 0;
        
        [ApexSerialization, FriendlyName("Inverted", "Invert Energy(0-max)")]
        public bool inverted = false;

        public override float Score(IAIContext context)
        {
            AIContext ai = ((AIContext)context);
            
            float e;
            if(inverted)
                e = 1 - (ai.character.energy / Settings.energyRange.max);
            else
                e = ai.character.energy / Settings.energyRange.max;
                
            float v = Mathf.Max(e - threshold, 0) / (1 - threshold);
           

            if(ai.debugAI_Character)
                Debug.Log("Energy: " + (v * score) + " value " + ai.character.energy);
           
            //TODO Make this more flexible
            return v * score;
        }
    }
}