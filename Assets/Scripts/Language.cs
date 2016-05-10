using UnityEngine;

namespace Kontiki{
    public static class Language{
                
        public enum Topic{
            IHaveQuest,
            IHaveNoQuest,
            DoYouHaveQuest,
            WhatDoIGetForQuest,
            IWillTradeThisForQuestObjective,
            AcceptQuest,
            DeclineQuest,
            IHaveQuestObjective,
            QuestFinished,
            DoYouHaveInfoAboutQuest,
            IHaveInfoAboutQuest,
            DeclineInfo,
            Trade,
            GotItem,
            RandomTalk,
        }
        
        public static void IHaveQuest(LanguageExchanger sender, LanguageExchanger receiver, Quest quest){
            sender.character.animationController.anim.SetTrigger("talk");
            if(quest is Fetch)
                IHaveQuest_Fetch(sender, receiver, (Fetch)quest);
                
            if(receiver != null)
                receiver.iconSystem.Clear();
                
            Delayer.Start(delegate() {  
                if(receiver != null)
                    receiver.React(sender, Topic.IHaveQuest, quest);
            }, Settings.speechDelay);
        }
        
        public static void IHaveNoInfoAboutQuest(LanguageExchanger sender, LanguageExchanger receiver){
            sender.character.animationController.anim.SetTrigger("talk");
            receiver.iconSystem.Clear();
            sender.iconSystem.GenerateIcons(IconType.No);
            
            Delayer.Start(delegate() {  
                receiver.React(sender, Topic.IHaveNoQuest);
            }, Settings.speechDelay);
        }
        
        public static void DoYouHaveQuest(LanguageExchanger sender, LanguageExchanger receiver){
            sender.character.animationController.anim.SetTrigger("talk");
            receiver.iconSystem.Clear();
            sender.iconSystem.GenerateIcons(IconType.Question);
            Delayer.Start(delegate() {
                receiver.React(sender, Topic.DoYouHaveQuest);
            }, Settings.speechDelay);
        }
        
        public static void WhatDoIGetForQuest(LanguageExchanger sender, LanguageExchanger receiver, Quest quest){
            sender.character.animationController.anim.SetTrigger("talk");
            sender.iconSystem.GenerateIcons(IconType.Trade, IconType.Question);
            receiver.iconSystem.Clear();
            Delayer.Start(delegate() {
                receiver.React(sender, Topic.WhatDoIGetForQuest, quest);
            }, Settings.speechDelay);
        }
        
        public static void WillTradeThisForQuestObjective(LanguageExchanger sender, LanguageExchanger receiver, Quest quest){
            sender.character.animationController.anim.SetTrigger("talk");
            receiver.iconSystem.Clear();
            if(quest is Fetch)
                WillTradeThisForQuestObjective_Fetch(sender, receiver, (Fetch)quest);
                
            Delayer.Start(delegate() {
                receiver.React(sender,Topic.IWillTradeThisForQuestObjective, quest);
            }, Settings.speechDelay);
        }
        
        
        public static void AcceptQuest(LanguageExchanger sender, LanguageExchanger receiver, Quest quest){
            sender.character.animationController.anim.SetTrigger("talk");
            receiver.iconSystem.Clear();
            sender.iconSystem.GenerateIcons(IconType.Yes);
            Delayer.Start(delegate() {
                receiver.React(sender, Topic.AcceptQuest, quest);
            }, Settings.speechDelay);
        }
        
        
        public static void DeclineQuest(LanguageExchanger sender, LanguageExchanger receiver, Quest quest){
            sender.character.animationController.anim.SetTrigger("talk");
            receiver.iconSystem.Clear();
            sender.iconSystem.GenerateIcons(IconType.No);
            Delayer.Start(delegate() {
                receiver.React(sender, Topic.DeclineQuest, quest);
            }, Settings.speechDelay);
        }
        
        public static void IHaveQuestObjective(LanguageExchanger sender, LanguageExchanger receiver, Quest quest){
            sender.character.animationController.anim.SetTrigger("talk");
            receiver.iconSystem.Clear();
            Fetch f = (Fetch) quest;
            Debug.Log("i have quest object");
            sender.iconSystem.GenerateIcons(f.colorObjective, f.colorOrigin, IconType.QuestObjective, IconType.TakeTo, IconType.Person);
            
            Delayer.Start(delegate() {
                receiver.React(sender, Topic.IHaveQuestObjective);
            }, Settings.speechDelay);
        }
        
        public static void QuestFinished(LanguageExchanger sender, LanguageExchanger receiver, Quest quest){
            sender.character.animationController.anim.SetTrigger("talk");
            receiver.iconSystem.Clear();
            sender.iconSystem.GenerateIcons(IconType.Yes);
            Delayer.Start(delegate() {
                receiver.React(sender, Topic.QuestFinished);
            }, Settings.speechDelay);
        }
        
        public static void DoYouHaveInfoAboutQuest(LanguageExchanger sender, LanguageExchanger receiver, Quest quest){
            sender.character.animationController.anim.SetTrigger("talk");
            receiver.iconSystem.Clear();
            Delayer.Start(delegate() {
                receiver.React(sender, Language.Topic.DoYouHaveInfoAboutQuest,quest);
            }, Settings.speechDelay);
        }
        public static void IHaveInfoAboutQuest(LanguageExchanger sender, LanguageExchanger receiver, Quest quest){
            sender.character.animationController.anim.SetTrigger("talk");
            receiver.iconSystem.Clear();
            sender.iconSystem.GenerateIcons(IconType.Find, IconType.Yes);
        }
        
        public static void IHaveNoInfoAboutQuest(LanguageExchanger sender, LanguageExchanger receiver, Quest quest){
            sender.character.animationController.anim.SetTrigger("talk");
            receiver.iconSystem.Clear();
            sender.iconSystem.GenerateIcons(IconType.No, IconType.Find);
        }
        
        public static void IHaveAlreadyBeenAsked(LanguageExchanger sender, LanguageExchanger receiver, Quest quest){
            sender.character.animationController.anim.SetTrigger("talk");
            receiver.iconSystem.Clear();
            sender.iconSystem.GenerateIcons(IconType.No);
        }
        
        public static void Trade(LanguageExchanger sender, LanguageExchanger receiver, ItemType itemGained, ItemType itemSold){
            sender.character.animationController.anim.SetTrigger("talk");
            receiver.iconSystem.Clear();
            GotItem(sender, itemGained);
            GotItem(receiver, itemSold);
        }
        
         public static void GotItem(LanguageExchanger sender, ItemType itemGained){
            sender.character.animationController.anim.SetTrigger("talk");
            sender.iconSystem.GenerateIcons(IconType.Fish);
        }

        public static void RandomTalk(LanguageExchanger sender, LanguageExchanger receiver){
            sender.character.animationController.anim.SetTrigger("talk");
            receiver.iconSystem.Clear();
            sender.iconSystem.GenerateIcons(
                Settings.iconTypes[Random.Range(0, Settings.iconTypes.Count)],
                Settings.iconTypes[Random.Range(0, Settings.iconTypes.Count)],
                Settings.iconTypes[Random.Range(0, Settings.iconTypes.Count)]
            );
            Delayer.Start(delegate() {
                receiver.React(sender, Topic.RandomTalk);
            }, Settings.speechDelay);
        }
        
    /**
    * Private
    **/
        
        
        /*
         Quest Type Specific
         */
        private static void IHaveQuest_Fetch(LanguageExchanger sender, LanguageExchanger receiver, Fetch quest){
             sender.iconSystem.GenerateIcons(
                quest.colorObjective,
                quest.colorOrigin,
                IconType.QuestObjective,
                IconType.Bring,
                IconType.Person
            );
        }
        
        private static void WillTradeThisForQuestObjective_Fetch(LanguageExchanger sender, LanguageExchanger receiver, Fetch quest){
             sender.iconSystem.GenerateIcons(
                quest.colorObjective,
                IconType.QuestObjective,
                IconType.Trade,
                IconType.Fish
            );
        }
    }
}