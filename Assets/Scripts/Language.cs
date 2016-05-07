namespace Kontiki{
    public static class Language{
        
        
        public static void Quest(IconSystem iconSystem, Quest quest){
            if(quest is Fetch)
                Quest_Fetch(iconSystem,(Fetch)quest);
            
            
           
        }
        
        private static void Quest_Fetch(IconSystem iconSystem, Fetch quest){
             iconSystem.GenerateIcons(
                quest.colorObjective,
                quest.colorOrigin,
                IconType.QuestObjective,
                IconType.Bring,
                IconType.Person
            );
            quest.origin.ChangeColor(quest.colorOrigin,2f);
        }
        
    }
}