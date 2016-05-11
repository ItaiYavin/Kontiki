using System;
using Apex.AI;
using UnityEngine;
using Apex.Serialization;
using Kontiki;

namespace Kontiki.AI
{

    /// <summary>
    /// Consumes the selected edible item if a edible is selected otherwise nothing.
    /// </summary>
    /// <seealso cref="Apex.AI.ActionBase" />

    public sealed class RespondToPlayerWantingToSpeak : ActionBase
    {
        
        [ApexSerialization, FriendlyName("value", "yes = conversation, no = no conversation")]
        public bool val = false;
        public override void Execute(IAIContext context)
        {
            AIContext ai = ((AIContext)context);
            
            Debug.Log("Responding to player " + val);
            ai.character.languageExchanger.RespondToPlayerWantingToSpeak(val);
        }
    }
}
