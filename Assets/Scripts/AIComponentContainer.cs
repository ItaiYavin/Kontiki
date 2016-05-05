using Apex.AI;
using UnityEngine;
using System;
using System.Collections.Generic;
using Apex.AI.Components;

using Kontiki.AI;
using Random = UnityEngine.Random;

namespace Kontiki {
    [RequireComponent(typeof(Character))]
    [RequireComponent(typeof(Pathfinder))]
	[RequireComponent(typeof(Memory))]
    [RequireComponent(typeof(Inventory))]
    [RequireComponent(typeof(BaseRoutine))]
	public sealed class AIComponentContainer : Interactable, IContextProvider {
        public bool isOnJob = false;
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

            indicator = gameObject.AddComponent <InteractableIndicator>();
            indicator.interactable = this;
        }

		void Awake () {
			_context = new AIContext(this);
		}

        public override bool Interact(Character player)
        {
            if(Settings.debugging)
                Debug.Log(character + " interacted with " + gameObject.name);

            return Random.Range(0f,1f) > 0.5f; // FAIR ROLL OF DICE
        }

        public Quest GetQuest(Character player)
        {
            return QuestGenerator.Instance.GenerateQuest(this.character, player);
        }

        public IAIContext GetContext(Guid aiId)
		{
			return _context;
		}
	}
}
