using Apex.AI;
using Apex.Serialization;
using UnityEngine;
using Kontiki;

namespace Kontiki.AI
{
    /// <summary>
    /// Scorer for evaluating hunger, linear score between 0-1, 0 being the threshold
    /// </summary>
    /// <seealso cref="Apex.AI.ContextualScorerBase" />
    public sealed class HasEdibleResource : ContextualScorerBase
    {
        [ApexSerialization, FriendlyName("Debug", "Debug Log values")]
        public bool debug = false;
        
        [ApexSerialization, FriendlyName("Not", "Returns the opposite")]
        public bool not = false;
        
        public override float Score(IAIContext context){
            Character character = ((CharacterAIContext)context).character;
            bool b = character.HasSelectedEdibleResource();
            if(not) b = !b;
            if(debug)
                Debug.Log("HasEdibleResource: " + b);
            
            return b ? 1f : 0f;
        }
    }
}