using Apex.AI;
using Apex.Serialization;
using Kontiki;
using UnityEngine;

namespace Kontiki.AI{
    /// <summary>
    /// Places selected item into the inventory (if not already there)
    /// </summary>
    /// <seealso cref="Apex.AI.ActionBase" />
	public sealed class PlaceItemInInventory : ActionBase {
		
		public override void Execute(IAIContext context){
            Character character = ((CharacterAIContext)context).character;
		}
	}
}