using Apex.AI;
using Apex.Serialization;
using UnityEngine;

namespace Kontiki.AI
{
    /// <summary>
    /// Scorer for whether character has edible in memory list
    /// </summary>
    /// <seealso cref="Apex.AI.ContextualScorerBase" />
    public sealed class SearchMemoryForEdible : ContextualScorerBase
    {
        [ApexSerialization, FriendlyName("Debug", "Debug Log values")]
        public bool debug = false;

        public override float Score(IAIContext context){
            Memory character = ((AIContext)context).memory;

            for(int i = 0; i < character.memory.Count; i++)
                if(character.memory[i].GetComponent<EdibleItem>() != null)
                {
                    if (debug) Debug.Log("Found edible item in memory");
                    return 1 * score;
                }

            if(debug) Debug.Log("Found NO edible item in memory");
            return 0;
        }
	}
}