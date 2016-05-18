using System;
using Apex.AI;
using Apex.Serialization;
using UnityEngine;
using Kontiki;

namespace Kontiki.AI
{
    /// <summary>
    /// Scorer for evaluating whether an item is within pick up range
    /// </summary>
    /// <seealso cref="Apex.AI.ContextualScorerBase" />
    public sealed class RelatedToQuest : ContextualScorerBase {
        
        [ApexSerialization, FriendlyName("questOriginMultiplier", "score mutliplier")]
        public bool isQuestOrigin = true;
        [ApexSerialization, FriendlyName("hasObjectiveMultiplier", "score mutliplier")]
        public bool hasObjective = true;
        


        public override float Score(IAIContext context){
            AIContext ai = (AIContext) context;
            
            
            Quest[] acceptedQuest = QuestSystem.Instance.GetAcceptedQuests();
            
            bool foundNothing = true;
                
            for (int i = 0; i < acceptedQuest.Length; i++)
            {
                Fetch quest = (Fetch) acceptedQuest[i];
                if(isQuestOrigin && quest.origin == ai.character){
                    foundNothing = false;
                    break;
                }
                if(hasObjective && quest.objectiveHolder == ai.character){
                    foundNothing = false;
                    break;
                }
            }
            
            return (foundNothing ? 0f : score);
        }
    }
}

                
