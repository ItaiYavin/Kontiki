using UnityEngine;
using System.Collections;

namespace Kontiki{
    public class LanguageExchanger : MonoBehaviour{
        
        
        
        public LanguageExchanger speakingTo;
        
        
        [HideInInspector] public IconSystem iconSystem;
        [HideInInspector] public Character character;
        [HideInInspector] public AIComponentContainer ai;
        
        private bool isPlayer = false;

        void Start(){
            iconSystem = GetComponent<IconSystem>();
            ai = GetComponent<AIComponentContainer>();
            character = GetComponent<Character>();
            
            isPlayer = character.isPlayer;
        }
        
        void FixedUpdate(){
              if(speakingTo != null && !isPlayer){
                float distance = Vector3.Distance(transform.position, speakingTo.transform.position);
                
                if(distance > Settings.stopInteractingDistance){
                    speakingTo.speakingTo = null;
                    speakingTo = null;
                    iconSystem.Clear();
                }else{
                    Vector3 lookDirection = speakingTo.transform.position;
                    lookDirection.y = transform.position.y;
                    transform.LookAt(lookDirection);
                }
            }
        }
        
        public void React(LanguageExchanger sender, LanguageTopic topic, params Information[] information){
            switch (topic)
            {
                case LanguageTopic.DoYouHaveQuest:{
                    if (!isPlayer) { 
                        
                        if(Settings.debugQuestInfo)
                            Debug.Log("NPC" + (ai.baseRoutine.hasQuestToOffer ? " has " : " does not have ") + "quest to offer");
                            
                        if(ai.baseRoutine.hasQuestToOffer){
                            
                            //if NPC react to DoYouHaveQuest from player
                            if(ai.baseRoutine.questOffer == null){
                                ai.baseRoutine.questOffer = QuestSystem.Instance.GenerateQuest(sender.character, character);
                                
                            }
                            
                            Language.IHaveQuest(this, sender, ai.baseRoutine.questOffer);    
                        }
                    }
                }break;
                case LanguageTopic.IHaveQuest:{
                   if(isPlayer){
                        //is Player and has received the topic Quest from a npc
                        WindowsHandler.Instance.SwitchWindow(Window.Quest1);
                    }
                }break;
                case LanguageTopic.WhatDoIGetForQuest:{
                    if (!isPlayer) { 
                        Language.WillTradeThisForQuestObjective(this, sender, ai.baseRoutine.questOffer);    
                    }
                }break;
                case LanguageTopic.IWillTradeThisForQuestObjective:{
                    if(isPlayer){
                        WindowsHandler.Instance.SwitchWindow(Window.Quest2);
                    }
                }break;
                case LanguageTopic.AcceptQuest:{
                    if (!isPlayer && ai.baseRoutine.questOffer != null){
                        QuestSystem.Instance.proposedQuest = ai.baseRoutine.questOffer;
                    }
                }break;
                case LanguageTopic.DeclineQuest:{
                    
                }break;
            }
        }
    }
}