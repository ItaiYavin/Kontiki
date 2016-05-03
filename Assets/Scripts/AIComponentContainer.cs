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
	[RequireComponent(typeof(UtilityAIComponent))]
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
        public Job job{
            get; 
            private set;
        }
        
        public bool debugAI;

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
            this.job = GetComponent<Job>();
            if(job == null)
                Debug.LogError("AI - Must have a Job");
        }

		void Awake () {
			_context = new AIContext(this);
		}
        
        #if UNITY_EDITOR
        void Reset() {
            if (job == null && GetComponent<Job>() == null)
                Debug.LogError("AI - Must have a Job");
            
        }
        #endif

		public IAIContext GetContext(Guid aiId)
		{
			return _context;
		}
	}
}
