using Apex.AI;
using Kontiki;

namespace Kontiki.AI{


    /// <summary>
    /// Finds nearest item within character scanning range.
    /// </summary>
    /// <seealso cref="Apex.AI.ActionBase" />

    public sealed class FindClosestItemInRange : ActionBase{
        public override void Execute(IAIContext context){
            ((AIContext)context).pathfinder.TargetClosestItemInRange();

        }
    }
}
