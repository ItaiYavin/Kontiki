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

    public enum ItemType
    {
        anything,
        edible,
        fuel,
        character
    };
    
   
}

namespace Kontiki.AI
{
    public enum JobType{
       Fisher,
       Scavenger,
       Trader            
    }
}
