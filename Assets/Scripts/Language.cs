namespace Kontiki{
    public static class Language{
        
        
        public static void IHaveQuest(LanguageExchanger sender, LanguageExchanger receiver, Quest quest){
            if(quest is Fetch)
                IHaveQuest_Fetch(sender, receiver, (Fetch)quest);
            
           receiver.React(sender, LanguageTopic.IHaveQuest, quest);
        }
        
        public static void DoYouHaveQuest(LanguageExchanger sender, LanguageExchanger receiver){
            sender.iconSystem.GenerateIcons(IconType.Question);
            receiver.React(sender, LanguageTopic.DoYouHaveQuest);
        }
        
        public static void WhatDoIGetForQuest(LanguageExchanger sender, LanguageExchanger receiver){
            sender.iconSystem.GenerateIcons(IconType.Trade, IconType.Question);
            receiver.React(sender, LanguageTopic.WhatDoIGetForQuest);
        }
        
        public static void WillTradeThisForQuestObjective(LanguageExchanger sender, LanguageExchanger receiver, Quest quest){
            if(quest is Fetch)
                WillTradeThisForQuestObjective_Fetch(sender, receiver, (Fetch)quest);
            receiver.React(sender,LanguageTopic.IWillTradeThisForQuestObjective);
        }
        
        
        public static void AcceptQuest(LanguageExchanger sender, LanguageExchanger receiver){
            sender.iconSystem.GenerateIcons(IconType.Yes);
            receiver.React(sender, LanguageTopic.AcceptQuest);
        }
        
        
        public static void DeclineQuest(LanguageExchanger sender, LanguageExchanger receiver){
            sender.iconSystem.GenerateIcons(IconType.No);
            receiver.React(sender, LanguageTopic.DeclineQuest);
        }
        
        
        /**
         * Private
         **/
        
        private static void IHaveQuest_Fetch(LanguageExchanger sender, LanguageExchanger receiver, Fetch quest){
             sender.iconSystem.GenerateIcons(
                quest.colorObjective,
                quest.colorOrigin,
                IconType.QuestObjective,
                IconType.Bring,
                IconType.Person
            );
            quest.origin.ChangeColor(quest.colorOrigin,2f);
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