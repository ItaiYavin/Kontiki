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
                    speakingTo = null;
                    iconSystem.Clear();
                }else{
                    Vector3 lookDirection = speakingTo.transform.position;
                    lookDirection.y = transform.position.y;
                    transform.LookAt(lookDirection);
                }
            }else if(speakingTo != null && isPlayer){
                float distance = Vector3.Distance(transform.position, speakingTo.transform.position);
                
                if(distance > Settings.stopInteractingDistance){
                    speakingTo = null;
                    iconSystem.Clear();
                    WindowsHandler.Instance.SetVisibility(false);
                }
            }
        }
        
        public void React(LanguageExchanger sender, Language.Topic topic, params Information[] information){
            switch (topic)
            {
                case Language.Topic.DoYouHaveQuest:{
                    if (!isPlayer) { 
                        
                        if(Settings.debugQuestInfo)
                            Debug.Log("NPC" + (ai.baseRoutine.hasQuestToOffer ? " has " : " does not have ") + "quest to offer");
                            
                        if(ai.baseRoutine.questOffer == null && ai.baseRoutine.hasQuestToOffer){
                            //if NPC react to DoYouHaveQuest from player
                            
                            Quest quest = QuestSystem.Instance.GenerateQuest(sender.character, character);
                           
                            Language.IHaveQuest(this, sender, quest); 
                            character.ChangeColor(quest.colorOrigin, 0.5f); 
                            ai.baseRoutine.questOffer = quest;  
                        }else if(ai.baseRoutine.questOffer != null && ai.baseRoutine.hasQuestToOffer){
                            
                            Language.IHaveQuest(this, null, ai.baseRoutine.questOffer); 
                        }else{
                            Language.IHaveNoInfoAboutQuest(this, sender);
                        }
                    }
                }break;
                case Language.Topic.IHaveQuest:{
                   if(isPlayer){
                        //is Player and has received the topic Quest from a npc
                        WindowsHandler.Instance.SwitchWindow(Window.Quest1);
                    }
                }break;
                case Language.Topic.WhatDoIGetForQuest:{
                    if (!isPlayer) { 
                        Quest quest = (Quest) information[0];
                        Language.WillTradeThisForQuestObjective(this, sender, quest);    
                    }
                }break;
                case Language.Topic.IWillTradeThisForQuestObjective:{
                    if(isPlayer){
                        WindowsHandler.Instance.SwitchWindow(Window.Quest2);
                    }
                }break;
                case Language.Topic.AcceptQuest:{
                    if (!isPlayer && ai.baseRoutine.questOffer != null){
                        Quest quest = (Quest) information[0];
                        quest.accepted = true;
                        QuestSystem.Instance.AcceptQuest(quest);
                        
                        //just to show player the quest an extra time.. receiver set to null ie. player will not receive it..
                        Language.IHaveQuest(this, null, quest); 
                    }
                }break;
                case Language.Topic.DeclineQuest:{
                    if (!isPlayer){
                        speakingTo = null;
                        character.ChangeColor(Color.white, 0.5f); 
                        Fetch quest = (Fetch) information[0];
                        QuestSystem.Instance.FreeUsedPersonColor(quest.colorOrigin);
                        iconSystem.Clear();
                    }
                }break;
                case Language.Topic.IHaveQuestObjective:{
                    if(isPlayer){
                        
                        if(Settings.debugQuestInfo)
                            Debug.Log("Quest Objective item");
                    }
                }break;
                case Language.Topic.QuestFinished:{
                    if(isPlayer){
                        
                        if(Settings.debugQuestInfo)
                            Debug.Log("Quest Finsihed");
                    }
                }break;
                case Language.Topic.DoYouHaveInfoAboutQuest:{
                    Fetch quest = (Fetch)information[0];
                    bool playerHasObjective = sender.character.inventory.CheckInventoryForSpecificItem(quest.objective);
                    
                    if(quest.origin == character){
                        //is talking to the quest origin 
                        if(playerHasObjective && sender.character.inventory.RemoveItem(quest.objective)){
                        //Player has objective and player has given objective
                            QuestSystem.Instance.FreeUsedPersonColor(quest.colorOrigin);
                            character.ChangeColor(Color.white,10f);
                            quest.FinishQuest(sender.character.inventory);
                            Language.QuestFinished(this, sender, quest);
                            
                        }else{
                            Language.IHaveQuest(this, null, quest); 
                        }
                    }else if(quest.CheckIfCharacterHasObjective(character)){
                        // asked person has objective and has given it to the player
                        Language.IHaveQuestObjective(this, sender, quest);
                        
                        quest.RemoveAreaOfInterest();
                        WindowsHandler.Instance.SetVisibility(false);
                        
                    }else if(quest.HasCharacterBeenAsked(character)){
                        
                        if(Settings.debugQuestInfo)    
                            Debug.Log("Character has already been asked");
                        
                        Language.IHaveAlreadyBeenAsked(this,sender,quest);
                    }else{
                        if(quest.CheckIfCharacterHasInfoAboutQuest(character)){
                            //has information
                            if(Settings.debugQuestInfo)
                                Debug.Log("Character has Information");
                            Language.IHaveInfoAboutQuest(this, sender, quest);
                            
                        }else{
                            //has no information
                            if(Settings.debugQuestInfo)
                                Debug.Log("Character has no Information");
                            Language.IHaveNoInfoAboutQuest(this, sender, quest);
                        }
                    }
                    speakingTo = null;
                }break;
                case Language.Topic.IHaveInfoAboutQuest:{
                    if(isPlayer){
                        //received that npc has info.
                        iconSystem.Clear();
                    }
                }break;
                case Language.Topic.DeclineInfo:{
                    if (!isPlayer){
                        speakingTo = null;
                        iconSystem.Clear();
                    }
                }break;
            }
        }
    }
}