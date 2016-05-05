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

        public override float Score(IAIContext context){
            AIContext ai = (AIContext) context;
            Memory character = ai.memory;

            for(int i = 0; i < character.memory.Count; i++)
                if(character.memory[i].GetComponent<EdibleItem>() != null)
                {
                    if (Settings.debugInteractionInfo) Debug.Log(ai.self.name + " found edible item in memory");
                    return 1 * score;
                }

            if(Settings.debugInteractionInfo) Debug.Log(ai.self.name + "Found NO edible item in memory");
            return 0;
        }
	}
}