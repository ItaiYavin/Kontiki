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

        [ApexSerialization, FriendlyName("Range", "Range AI needs to be within to be considered being at position")]
        public float range = 2;

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
                    b = ai.pathfinder.IsAtPosition(ai.baseRoutine.home.position, range);
                }
                break;

                case PlaceType.Trader:
                {
                   b = ai.pathfinder.IsAtPosition(ai.baseRoutine.trader.transform.position, range);
                }
                break;

                case PlaceType.Boat:
                {
                    if(ai.job is JobWithBoat){
                        JobWithBoat job = (JobWithBoat)ai.job;
                        b = ai.pathfinder.IsAtPosition(job.boat.transform.position, range);
                    }
                }
                break;
            }

            if (ai.debugAI_Character)
                Debug.Log("Is " + (b ? "" : "not") + " at " + place);

            if (not) b = !b;
            return b ? score : 0f;
        }
    }
}