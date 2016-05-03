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
            	case ItemType.edible:
            	{
            		ai.inventory.GetItemFromTrade(gainedItem);
            	}
            	break;

            	case ItemType.fuel:
            	break;

            	case ItemType.character:
            	break;
            }

        }
	}
}