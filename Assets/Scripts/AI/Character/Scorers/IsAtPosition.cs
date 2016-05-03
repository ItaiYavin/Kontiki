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

        [ApexSerialization, FriendlyName("Destination", "Position AI is checked in relation to")]
        public PlaceType place;

        [ApexSerialization, FriendlyName("Not", "Returns the opposite")]
        public bool not = false;

        public override float Score(IAIContext context)
        {
            AIContext ai = (AIContext)context;
            Character character = ai.character;
            bool b = false;

            switch(place){
                case PlaceType.Home:
                {
                    b = ai.pathfinder.IsAtPosition(ai.baseroutine.home.position, minimumRange);
                }
                break;

                case PlaceType.FoodQuench:
                {
                    b = ai.pathfinder.IsAtPosition(ai.baseroutine.foodQuench.position, minimumRange);
                }
                break;
            }

            if (not) b = !b;
            if (ai.debugAI)
                Debug.Log("Is at position: " + b);

            return b ? score : 0f;
        }
    }
}