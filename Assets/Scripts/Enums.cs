using UnityEngine;
using System.Collections;

namespace Kontiki {
    public enum IconTypes
    {
        Food,
        Eat,
        Trade,
        Quest,
        QuestItem,
        Person,
        Question
    }

    public enum PlaceType
    {
        Home,
        FoodQuench,
    }

	public enum Gender
    {
        Male,
        Female
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
    
   
}

namespace Kontiki.AI
{
    public enum JobType{
       Fisher,
       Trader            
    }
}
