using Apex.AI;
using Kontiki;

namespace Kontiki.AI{


    /// <summary>
    /// Consumes the selected edible item if an edible is selected otherwise nothing.
    /// </summary>
    /// <seealso cref="Apex.AI.ActionBase" />

    public sealed class ConsumeSelectedItem : ActionBase{
        public override void Execute(IAIContext context){
            AIContext ai = (AIContext) context;
            if(ai.character.selectedInteractable is EdibleItem)
                ai.character.InteractWithSelected();
        }
    }
}
