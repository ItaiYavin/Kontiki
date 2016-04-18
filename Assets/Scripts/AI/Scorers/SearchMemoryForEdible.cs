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
            Character character = ((CharacterAIContext)context).character;

            for(int i = 0; i < character.memory.Count; i++)
                if(character.memory[i].GetComponent<EdibleItem>() != null)
                    return 1;

            return 0;
        }
	}
}