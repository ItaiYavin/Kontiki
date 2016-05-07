using Apex.AI;
using UnityEngine;
using Kontiki.AI;

namespace Kontiki {
	public class AIContext : IAIContext {
		
		public bool debugAI_Character{
			get{return self.debugAI_Character;}
		}
		public bool debugAI_Job{
			get{return self.debugAI_Job;}
		}
		public bool debugAI_Interaction{
			get{return self.debugAI_Interaction;}
		}
		
		public bool isOnJob{
			get{ return self.isOnJob;}
			set{ self.isOnJob = value;}
		}

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
			get { return self.memory;}
		}
        public Inventory inventory
        {
            get { return self.inventory;}
        }
		
		public Job job
		{
			get { return self.job;}		
		}
		
		public Transform transform
		{
			get { return self.transform;}
        }

        public BaseRoutine baseRoutine
        {
            get { return self.baseRoutine; }
        }

        public IconSystem iconSystem
        {
            get { return self.iconSystem; }
        }

		public AIContext(AIComponentContainer self)
		{
			this.self = self;
		}
	}
}
