using Apex.AI;
using UnityEngine;
using System;
using System.Collections;
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
            
        public bool debugAI_Character = false;
        public bool debugAI_Job = false;
        public bool debugAI_Interaction = false;
        
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
            if(debugAI_Interaction)
                Debug.Log(character + " interacted with " + gameObject.name);

            if(Settings.debugQuestInfo)
                Debug.Log("NPC" + (baseroutine.hasQuestToOffer ? " has " : " does not have ") + "quest to offer");

            if (baseroutine.hasQuestToOffer && baseroutine.questOffer == null) { 
                baseroutine.questOffer = QuestGenerator.Instance.GenerateQuest(character, player);
                QuestGenerator.Instance.proposedQuest = baseroutine.questOffer;
            }

            return true;
        }

        public IAIContext GetContext(Guid aiId)
		{
			return _context;
		}
	}
}
