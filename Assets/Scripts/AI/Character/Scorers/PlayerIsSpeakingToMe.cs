

using System;
using Apex.AI;
using Apex.Serialization;
using UnityEngine;

namespace Kontiki.AI
{
    /// <summary>
    /// Boolean whether or not the player is speaking to me
    /// </summary>
    /// <seealso cref="Apex.AI.ContextualScorerBase" />
    public sealed class PlayerIsSpeakingToMe : ContextualScorerBase
    {
        
        [ApexSerialization, FriendlyName("Not", "Invert")]
        public bool not = false;

        public override float Score(IAIContext context)
        {
            AIContext ai = ((AIContext)context);
            
            bool b = ai.character.languageExchanger.playerIsSpeakingToMe;
           
            if(not) b = !b;
           
            //TODO Make this more flexible
            return b ? score : 0f;
        }
    }
}