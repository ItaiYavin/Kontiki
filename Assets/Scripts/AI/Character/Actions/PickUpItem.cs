using Apex.AI;
using Apex.Serialization;
using Kontiki;
using UnityEngine;

namespace Kontiki.AI{
    /// <summary>
    /// Picks up item into the inventory (if not already there)
    /// </summary>
    /// <seealso cref="Apex.AI.ActionBase" />
	public class PickUpItem : ActionBase {
		enum ItemType 
		{
			edible, 
			fuel
		};

		[ApexSerialization, FriendlyName("Debug", "if on, writes debug messages to console")]
		bool debug;

		[ApexSerialization, FriendlyName("Item Types", "Type of item")]
		ItemType itemType;

		public override void Execute(IAIContext context){
			/*switch(){
				Case :
			}*/
		}
	}
}