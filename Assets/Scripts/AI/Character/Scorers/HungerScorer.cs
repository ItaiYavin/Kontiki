using Apex.AI;
using Apex.Serialization;
using UnityEngine;

namespace Kontiki.AI
{
    /// <summary>
    /// Scorer for evaluating hunger, linear score between 0-1, 0 being the threshold
    /// </summary>
    /// <seealso cref="Apex.AI.ContextualScorerBase" />
    public sealed class HungerScorer : ContextualScorerBase
    {
        
        [ApexSerialization, FriendlyName("Threshold", "Threshold(0-1) for hunger(0-1) where the score will be 0")]
        public float threshold = 0;
        
        public override float Score(IAIContext context){
            AIContext ai = ((AIContext)context);
            float v = Mathf.Max((ai.character.hunger/SettingsSingleton.Instance.hungerRange.max - threshold),0)/(1-threshold);
            if(ai.debugAI)
                Debug.Log("Hunger Score " + v + " character hunger: " + ai.character.hunger);
            return v * score;
        }
    }
}