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
        Beggar
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
        Quest,
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
   
}

namespace Kontiki.AI
{
    public enum JobType{
       Fisher,
       Trader            
    }
    
    [System.Flags]
    public enum DebugAI{
        Character   = (0 << 1),
        Job         = (0 << 2),
        Interaction = (0 << 3)
    }
}
