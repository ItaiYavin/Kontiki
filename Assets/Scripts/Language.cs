using UnityEngine;

namespace Kontiki{
    public static class Language{
                
        public enum Topic{
            DoYouWantToStartConversation,
            DeclineConversation,
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
            IHaveNoInfoAboutQuest,
            DeclineInfo,
            Trade,
            GotItem,
            RandomTalk,
        }
        
        public static void DoYouWantToStartConversation(LanguageExchanger sender, LanguageExchanger receiver){
            sender.iconSystem.GenerateIcons(IconType.SomethingToSay);
            receiver.iconSystem.Clear();
            receiver.React(sender, Topic.DoYouWantToStartConversation);

        }
        public static void DeclineConversation(LanguageExchanger sender, LanguageExchanger receiver){
            sender.iconSystem.GenerateIcons(receiver.character.material.color, IconType.No);
            receiver.iconSystem.Clear();
            receiver.playerIsSpeakingToMe = false;
            receiver.playerWantsToSpeakWithMe = false;
            receiver.speakingTo = null;
            
            Delayer.Start(delegate() {  
                if(receiver != null)
                    receiver.React(sender, Topic.DeclineConversation);
            }, Settings.speechDelay);
            
        }
        
        public static void IHaveQuest(LanguageExchanger sender, LanguageExchanger receiver, Quest quest){
            sender.character.animationController.Talk();
            if(quest is Fetch)
                IHaveQuest_Fetch(sender, receiver, (Fetch)quest);
                
            if(receiver != null)
                receiver.iconSystem.Clear();
                
            Delayer.Start(delegate() {  
                if(receiver != null)
                    receiver.React(sender, Topic.IHaveQuest, quest);
            }, Settings.speechDelay);
        }
        
        public static void IHaveNoInfoAboutQuest(LanguageExchanger sender, LanguageExchanger receiver)
        {
            sender.character.animationController.Talk();
            receiver.iconSystem.Clear();
            sender.iconSystem.GenerateIcons(IconType.No);
            
            Delayer.Start(delegate() {  
                receiver.React(sender, Topic.IHaveNoQuest);
            }, Settings.speechDelay);
            
            sender.ExitConversation();
        }

        public static void DoYouHaveQuest(LanguageExchanger sender, LanguageExchanger receiver)
        {

            sender.character.animationController.Talk();
            receiver.iconSystem.Clear();
            sender.iconSystem.GenerateIcons(IconType.Question);
            Delayer.Start(delegate() {
                receiver.React(sender, Topic.DoYouHaveQuest);
            }, Settings.speechDelay);
        }
        
        public static void WhatDoIGetForQuest(LanguageExchanger sender, LanguageExchanger receiver, Quest quest){
            sender.character.animationController.Talk();
            sender.iconSystem.GenerateIcons(IconType.Trade, IconType.Question);
            receiver.iconSystem.Clear();
            Delayer.Start(delegate() {
                receiver.React(sender, Topic.WhatDoIGetForQuest, quest);
            }, Settings.speechDelay);
        }
        
        public static void WillTradeThisForQuestObjective(LanguageExchanger sender, LanguageExchanger receiver, Quest quest){
            sender.character.animationController.Talk();
            receiver.iconSystem.Clear();
            if(quest is Fetch)
                WillTradeThisForQuestObjective_Fetch(sender, receiver, (Fetch)quest);
                
            Delayer.Start(delegate() {
                receiver.React(sender,Topic.IWillTradeThisForQuestObjective, quest);
            }, Settings.speechDelay);
        }
        
        
        public static void AcceptQuest(LanguageExchanger sender, LanguageExchanger receiver, Quest quest){
            sender.character.animationController.Talk();
            receiver.iconSystem.Clear();
            sender.iconSystem.GenerateIcons(IconType.Yes);
            Delayer.Start(delegate() {
                receiver.React(sender, Topic.AcceptQuest, quest);
            }, Settings.speechDelay);
        }
        
        
        public static void DeclineQuest(LanguageExchanger sender, LanguageExchanger receiver, Quest quest){
            sender.character.animationController.Talk();
            receiver.iconSystem.Clear();
            sender.iconSystem.GenerateIcons(IconType.No);
            Delayer.Start(delegate() {
                receiver.React(sender, Topic.DeclineQuest, quest);
            }, Settings.speechDelay);
        }
        
        public static void IHaveQuestObjective(LanguageExchanger sender, LanguageExchanger receiver, Quest quest){
            sender.character.animationController.Talk();
            receiver.iconSystem.Clear();
            Fetch f = (Fetch) quest;
            Debug.Log("i have quest object");
            sender.iconSystem.GenerateIcons(f.colorObjective, f.colorOrigin, IconType.QuestObjective, IconType.TakeTo, IconType.Person);
            
            Delayer.Start(delegate() {
                receiver.React(sender, Topic.IHaveQuestObjective);
            }, Settings.speechDelay);
        }
        
        public static void QuestFinished(LanguageExchanger sender, LanguageExchanger receiver, Quest quest){
            sender.character.animationController.Talk();
            receiver.iconSystem.Clear();
            sender.iconSystem.GenerateIcons(IconType.Yes);
            Delayer.Start(delegate() {
                receiver.React(sender, Topic.QuestFinished);
            }, Settings.speechDelay);
        }
        
        public static void DoYouHaveInfoAboutQuest(LanguageExchanger sender, LanguageExchanger receiver, Quest quest){
            sender.character.animationController.Talk();
            sender.iconSystem.GenerateIcons(quest.colorOrigin, IconType.QuestObjective, IconType.Question);
            receiver.iconSystem.Clear();
            Delayer.Start(delegate() {
                receiver.React(sender, Language.Topic.DoYouHaveInfoAboutQuest,quest);
            }, Settings.speechDelay);
        }
        public static void IHaveInfoAboutQuest(LanguageExchanger sender, LanguageExchanger receiver, Quest quest){
            sender.character.animationController.Point();
            receiver.iconSystem.Clear();
            sender.iconSystem.GenerateIcons(IconType.Find, IconType.Yes);
            Delayer.Start(delegate () {
                receiver.React(sender, Language.Topic.IHaveInfoAboutQuest, quest);
            }, Settings.speechDelay);

            //TODO(frans) PLEASE HELP

            sender.speakingTo = null;
            sender.character.gameObject.transform.LookAt(quest.areaOfInterest.transform, Vector3.up);
        }
        
        public static void IHaveNoInfoAboutQuest(LanguageExchanger sender, LanguageExchanger receiver, Quest quest){
            sender.character.animationController.Talk();
            receiver.iconSystem.Clear();
            sender.iconSystem.GenerateIcons(IconType.No, IconType.Find);
            
            Delayer.Start(delegate() {
                receiver.React(sender, Language.Topic.IHaveNoInfoAboutQuest, quest);
            }, Settings.speechDelay);
        }
        
        public static void IHaveAlreadyBeenAsked(LanguageExchanger sender, LanguageExchanger receiver, Quest quest){
            sender.character.animationController.Talk();
            receiver.iconSystem.Clear();
            sender.iconSystem.GenerateIcons(IconType.No);
        }
        
        public static void Trade(LanguageExchanger sender, LanguageExchanger receiver, ItemType itemGained, ItemType itemSold){
            sender.character.animationController.Talk();
            receiver.iconSystem.Clear();
            GotItem(sender, itemGained);
            GotItem(receiver, itemSold);
        }
        
         public static void GotItem(LanguageExchanger sender, ItemType itemGained){
            sender.character.animationController.Talk();
            sender.iconSystem.GenerateIcons(IconType.Fish);
        }

        public static void RandomTalk(LanguageExchanger sender, LanguageExchanger receiver){
            sender.character.animationController.Talk();
            receiver.iconSystem.Clear();
            sender.iconSystem.GenerateIcons(
                Settings.iconTypes[Random.Range(0, Settings.iconTypes.Count)]
            );
            Delayer.Start(delegate() {
                receiver.React(sender, Topic.RandomTalk);
            }, Settings.speechDelay);
        }

        public static void IHaveNoQuest(LanguageExchanger sender, LanguageExchanger receiver)
        {
            sender.character.animationController.Talk();
            receiver.iconSystem.Clear();
            sender.iconSystem.GenerateIcons(IconType.No);

            Delayer.Start(delegate () {
                receiver.React(sender, Topic.IHaveNoQuest);
            }, Settings.speechDelay);

            sender.ExitConversation();
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
                IconType.QuestObjective,
                IconType.Bring
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