using Apex.AI;
using UnityEngine;

namespace Kontiki.AI
{

    public class CharacterAIContext : IAIContext{

        public Character character;
        public NavMeshAgent navAgent;

        public CharacterAIContext(Character character){
            this.character = character;
            navAgent = character.GetComponent<NavMeshAgent>();
        }

        public void GoToTarget(){
            navAgent.Move(character.selectedItem.transform.position - character.transform.position);
        }


    }
}
