using Apex.AI;
using UnityEngine;
using System;
using System.Collections.Generic;
using Apex.AI.Components;

namespace Kontiki {
    [RequireComponent(typeof(Character))]
    [RequireComponent(typeof(Pathfinder))]
	[RequireComponent(typeof(Memory))]
    [RequireComponent(typeof(Inventory))]
	public sealed class AIComponentContainer : MonoBehaviour, IContextProvider {
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
		}

		void Awake () {
			_context = new AIContext(this, this.character, this.pathfinder, this.memory, this.inventory);
		}

		public IAIContext GetContext(Guid aiId)
		{
			return _context;
		}
	}
}
