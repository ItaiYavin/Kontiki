namespace Kontiki {
    public enum IconType
    {
        Bring,
        Find,
        No,
        Question,
        SomethingToSay,
        TakeTo,
        Trade,
        Boat,
        BoatWithSail,
        Fish,
        Fisherboat,
        Net,
        QuestObjective,
        Rig,
        Water,
        Person,
        Beggar,
        Yes
    }

    public enum PlaceType
    {
        Home,
        Trader,
        Boat
    }
    
    public enum Window
    {
        Start,
        Quest1,
        Quest2,
        Info,
        Trade
    }

    public enum ItemType
    {
        Interactable,
        Edible,
        Item
    };
   public enum QuestType{
       Fetch,
       Give
   }
   
   public enum LanguageTopic{
       IHaveQuest,
       DoYouHaveQuest,
       WhatDoIGetForQuest,
       IWillTradeThisForQuestObjective,
       AcceptQuest,
       DeclineQuest,
       Trade
   }
}

namespace Kontiki.AI
{
    public enum JobType{
       Fisher,
       Trader            
    }
}
