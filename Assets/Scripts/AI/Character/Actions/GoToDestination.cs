using Apex.AI;
using Kontiki;

namespace Kontiki.AI{
    
    
    /// <summary>
    /// Consumes the selected edible item if a edible is selected otherwise nothing.
    /// </summary>
    /// <seealso cref="Apex.AI.ActionBase" />
    
    public sealed class GoToDestination : ActionBase{
        public override void Execute(IAIContext context){
            ((CharacterAIContext)context).character.GoToDestination();
        }
    }
}
