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
        [ApexSerialization, FriendlyName("Non-Zero", "returns false if range is zero")]
        public bool nonZero = false;
        
        [ApexSerialization, FriendlyName("Use Pathfinding Distance", "Use the calculated distance from the pathfinder else use direct path")]
        public bool usePathfindingDistance = false;

        [ApexSerialization, FriendlyName("Destination", "Position AI is checked in relation to"), MemberDependency("usePathfindingDistance", false)]
        public PlaceType place;

        [ApexSerialization, FriendlyName("Not", "Returns the opposite")]
        public bool not = false;

        public override float Score(IAIContext context)
        {
            AIContext ai = (AIContext)context;
            Character character = ai.character;
            bool b = false;
            
            if(usePathfindingDistance ){
                if(nonZero && ai.pathfinder.agent.remainingDistance == 0)
                    b = false;
                else
                    b = ai.pathfinder.agent.remainingDistance < range;
            }else{
                switch(place){
                    case PlaceType.Home:
                    {
                        b = ai.pathfinder.IsAtPosition(ai.baseRoutine.home.position, range);
                    }
                    break;

					case PlaceType.Plaza:
					{					
						b = ai.pathfinder.IsAtPosition(ai.baseRoutine.plaza.transform.position, range);
                        
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
                    
                    case PlaceType.DeliveryOrigin:{
                        if(ai.job is DeliveryMan){
                            DeliveryMan job = (DeliveryMan)ai.job;
                            b = ai.pathfinder.IsAtPosition(job.origin.transform.position, range);
                        }
                    }break;
                    
                    case PlaceType.DeliveryDestination:{
                        if(ai.job is DeliveryMan){
                            DeliveryMan job = (DeliveryMan)ai.job;
                            b = ai.pathfinder.IsAtPosition(job.destination.transform.position, range);
                        }
                    }break;
                }

            }
           
            if (ai.debugAI_Character)
                Debug.Log("Is " + (b ? "" : "not") + " at " + place);

            if (not) b = !b;
            return b ? score : 0f;
        }
    }
}