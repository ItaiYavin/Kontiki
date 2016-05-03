using System;
using Apex.AI;
using Apex.Serialization;
using Kontiki;

namespace Kontiki.AI
{

    /// <summary>
    /// Consumes the selected edible item if a edible is selected otherwise nothing.
    /// </summary>
    /// <seealso cref="Apex.AI.ActionBase" />
    public sealed class Sleep : ActionBase
    {
        [ApexSerialization, FriendlyName("Stop", "Puts AI in an awake state")]
        public bool stop = false;

    	public override void Execute(IAIContext context)
        {
            AIContext ai = ((AIContext)context);
            bool b = true;
            b = stop ? !b : b;

            ai.character.Sleep(b);
        }
    }
}
