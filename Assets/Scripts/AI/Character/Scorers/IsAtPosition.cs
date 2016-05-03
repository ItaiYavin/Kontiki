using Apex.AI;
using Apex.Serialization;
using UnityEngine;
using Kontiki;

namespace Kontiki.AI
{
    /// <summary>
    /// Scorer for whether character has an edible resource selected
    /// </summary>
    /// <seealso cref="Apex.AI.ContextualScorerBase" />
    public sealed class IsAtPosition : ContextualScorerBase
    {

        [ApexSerialization, FriendlyName("Minimum Range", "Range AI needs to be within to be considered being at position")]
        public float minimumRange = 2;

        [ApexSerialization, FriendlyName("Not", "Returns the opposite")]
        public bool not = false;

        public override float Score(IAIContext context)
        {
            AIContext ai = (AIContext)context;
            Character character = ai.character;
            bool b = false;

            b = ai.pathfinder.IsAtPosition(character.transform.position, minimumRange);

            if (not) b = !b;
            if (ai.debugAI)
                Debug.Log("Is at position: " + b);

            return b ? score : 0f;
        }
    }
}