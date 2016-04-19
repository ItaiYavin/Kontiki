using Apex.AI;
using Kontiki;

namespace Kontiki.AI{


    /// <summary>
    /// Selects edible resource within character scanning range.
    /// </summary>
    /// <seealso cref="Apex.AI.ActionBase" />

    public sealed class SelectClosestEdibleInRange : ActionBase{
        public override void Execute(IAIContext context){
            ((CharacterAIContext)context).character.SelectClosestItemInRange();

        }
    }
}
