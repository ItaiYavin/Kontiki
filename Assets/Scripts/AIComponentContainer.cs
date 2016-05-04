using Apex.AI;
using UnityEngine;
using System;
using System.Collections.Generic;
using Apex.AI.Components;

using Kontiki.AI;

namespace Kontiki {
    [RequireComponent(typeof(Character))]
    [RequireComponent(typeof(Pathfinder))]
	[RequireComponent(typeof(Memory))]
    [RequireComponent(typeof(Inventory))]
    [RequireComponent(typeof(BaseRoutine))]
	public sealed class AIComponentContainer : MonoBehaviour, IContextProvider {
        
        public bool debugAI;
        /**
         * Components that is required for AI
         **/
        public Pathfinder pathfinder
        {
            get;
            private set;
        }
		public Memory memory
        {
            get;
            private set;
        }
        public Inventory inventory
        {
            get;
            private set;
        }
        public Character character
        {
            get;
            private set;
        }
        public Job job
        {
            get;
            private set;
        }
        public BaseRoutine baseroutine
        {
            get;
            private set;
        }

        /**
         * Apex specific
         **/
        private AIContext _context;


        // Use this for initialization
        void Start()
        {
            this.character = GetComponent<Character>();
            this.pathfinder = GetComponent<Pathfinder>();
			this.memory = GetComponent<Memory>();
            this.inventory = GetComponent<Inventory>();
            this.baseroutine = GetComponent<BaseRoutine>();		
            this.job = GetComponent<Job>();
        }

		void Awake () {
			_context = new AIContext(this);
		}

		public IAIContext GetContext(Guid aiId)
		{
			return _context;
		}
	}
}
