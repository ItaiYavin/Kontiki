using Apex.AI;
using UnityEngine;

namespace Kontiki {
	public class AIContext : IAIContext {

		public bool debugAI = false;

        public AIComponentContainer self
		{
			get;
			private set;
        }
        public Character character
        {
            get { return self.character;}
        }
        public Pathfinder pathfinder {
			get { return self.pathfinder;}
		}
		public Memory memory {
			get { return self.memory; }
		}
        public Inventory inventory
        {
            get { return self.inventory;}
        }
		
		public Transform transform
		{
			get { return self.transform;}		}

		public AIContext(AIComponentContainer self)
		{
			this.self = self;
		}
	}
}
