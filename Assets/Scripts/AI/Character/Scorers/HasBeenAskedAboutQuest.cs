using System;
using Apex.AI;
using Apex.Serialization;
using UnityEngine;
using Kontiki;

namespace Kontiki.AI
{
    /// <summary>
    /// Has been asked about all quests
    /// </summary>
    /// <seealso cref="Apex.AI.ContextualScorerBase" />
    public sealed class HasBeenAskedAboutQuest : ContextualScorerBase {
   [ApexSerialization, FriendlyName("Not", "Returns the opposite")]
        public bool not = false;
        public override float Score(IAIContext context){
            AIContext ai = (AIContext) context;
            
            bool b = false;
            
            Quest[] acceptedQuest = QuestSystem.Instance.GetAcceptedQuests();
                
                
            for (int i = 0; i < acceptedQuest.Length; i++)
            {
                Fetch quest = (Fetch) acceptedQuest[i];
               
                if(quest.HasCharacterBeenAsked(ai.character)){
                    b = true;
                }else{
                    b = false;
                }
            }
         
            if(not) b = !b;
            return b ? 1f * score : 0f * score;
        }
    }
}

                
