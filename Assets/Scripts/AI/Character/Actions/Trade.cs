using System;
using Apex.AI;
using Apex.Serialization;
using Kontiki;
using UnityEngine;

namespace Kontiki.AI
{

    /// <summary>
    /// Consumes the selected edible item if a edible is selected otherwise nothing.
    /// </summary>
    /// <seealso cref="Apex.AI.ActionBase" />
	public sealed class Trade : ActionBase {
		[ApexSerialization, FriendlyName("Trade Item", "Trade WITH item of type")]
        public ItemType tradeItem;

		[ApexSerialization, FriendlyName("Gained Item", "Trade FOR item of type")]
        public ItemType gainedItem;

    	public override void Execute(IAIContext context)
        {
            AIContext ai = ((AIContext)context);
            Inventory inventory = ai.inventory;

            switch(gainedItem){
            	case ItemType.Edible:
            	{
            		ai.inventory.GetItemFromTrade(gainedItem);
            	}
            	break;
            }
            if(ai.job is Fisher && ((Fisher)ai.job).hasItems){
                for (int i = 0; i < 4; i++)
                {
            		ai.inventory.GetItemFromTrade(gainedItem);
                }
                ((Fisher)ai.job).hasItems = false;
            }

        }
	}
}