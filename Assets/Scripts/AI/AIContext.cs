using Apex.AI;
using UnityEngine;

namespace Kontiki {
	public class AIContext : IAIContext {

        public AIComponentContainer self
		{
			get;
			private set;
        }
        public Character character
        {
            get;
            private set;
        }
        public Pathfinder pathfinder {
			get;
			private set;
		}
		public Memory memory {
			get;
			private set;
		}
        public Inventory inventory
        {
            get;
            private set;
        }

		public AIContext(AIComponentContainer self, Character character, Pathfinder pathfinder, Memory memory, Inventory inventory)
		{
			this.self = self;
            this.character = character;
			this.pathfinder = pathfinder;
			this.memory = memory;
            this.inventory = inventory;
		}
	}
}
