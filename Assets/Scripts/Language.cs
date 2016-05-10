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
        }
        
        public static void IHaveQuest(LanguageExchanger sender, LanguageExchanger receiver, Quest quest){
            if(quest is Fetch)
                IHaveQuest_Fetch(sender, receiver, (Fetch)quest);
           
            if(receiver != null)
                receiver.React(sender, Topic.IHaveQuest, quest);
        }
        
        public static void IHaveNoInfoAboutQuest(LanguageExchanger sender, LanguageExchanger receiver){
            sender.iconSystem.GenerateIcons(IconType.No);
            receiver.React(sender, Topic.IHaveNoQuest);
        }
        
        public static void DoYouHaveQuest(LanguageExchanger sender, LanguageExchanger receiver){
            sender.iconSystem.GenerateIcons(IconType.Question);
            receiver.React(sender, Topic.DoYouHaveQuest);
        }
        
        public static void WhatDoIGetForQuest(LanguageExchanger sender, LanguageExchanger receiver, Quest quest){
            sender.iconSystem.GenerateIcons(IconType.Trade, IconType.Question);
            receiver.React(sender, Topic.WhatDoIGetForQuest, quest);
        }
        
        public static void WillTradeThisForQuestObjective(LanguageExchanger sender, LanguageExchanger receiver, Quest quest){
            if(quest is Fetch)
                WillTradeThisForQuestObjective_Fetch(sender, receiver, (Fetch)quest);
                
            receiver.React(sender,Topic.IWillTradeThisForQuestObjective, quest);
        }
        
        
        public static void AcceptQuest(LanguageExchanger sender, LanguageExchanger receiver, Quest quest){
            sender.iconSystem.GenerateIcons(IconType.Yes);
            receiver.React(sender, Topic.AcceptQuest, quest);
        }
        
        
        public static void DeclineQuest(LanguageExchanger sender, LanguageExchanger receiver, Quest quest){
            sender.iconSystem.GenerateIcons(IconType.No);
            receiver.React(sender, Topic.DeclineQuest, quest);
        }
        
        public static void IHaveQuestObjective(LanguageExchanger sender, LanguageExchanger receiver, Quest quest){
            Fetch f = (Fetch) quest;
            Debug.Log("i have quest object");
            sender.iconSystem.GenerateIcons(f.colorObjective, f.colorOrigin, IconType.QuestObjective, IconType.TakeTo, IconType.Person);
            
            receiver.React(sender, Topic.IHaveQuestObjective);
        }
        
        public static void QuestFinished(LanguageExchanger sender, LanguageExchanger receiver, Quest quest){
            sender.iconSystem.GenerateIcons(IconType.Yes);
            receiver.React(sender, Topic.QuestFinished);
        }
        
        public static void DoYouHaveInfoAboutQuest(LanguageExchanger sender, LanguageExchanger receiver, Quest quest){
            
            receiver.React(sender, Language.Topic.DoYouHaveInfoAboutQuest,quest);
        }
        public static void IHaveInfoAboutQuest(LanguageExchanger sender, LanguageExchanger receiver, Quest quest){
            sender.iconSystem.GenerateIcons(IconType.Find, IconType.Yes);
        }
        
        public static void IHaveNoInfoAboutQuest(LanguageExchanger sender, LanguageExchanger receiver, Quest quest){
            sender.iconSystem.GenerateIcons(IconType.No, IconType.Find);
        }
        
        public static void IHaveAlreadyBeenAsked(LanguageExchanger sender, LanguageExchanger receiver, Quest quest){
            sender.iconSystem.GenerateIcons(IconType.No);
        }
        
        public static void Trade(LanguageExchanger sender, LanguageExchanger receiver, ItemType itemGained, ItemType itemSold){
            GotItem(sender, itemGained);
            GotItem(receiver, itemSold);
        }
        
         public static void GotItem(LanguageExchanger sender, ItemType itemGained){
            sender.iconSystem.GenerateIcons(IconType.Fish);
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