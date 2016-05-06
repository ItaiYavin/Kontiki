using System.Collections;
using UnityEngine;

namespace Kontiki{
    public static class Language{
        
        
        public static void Quest(IconSystem iconSystem, Quest quest){
            iconSystem.GenerateIcons(
                new Color(1,0,0),
                new Color(0,1,0),
                IconType.Quest,
                IconType.Find,
                IconType.Person
            );
        }
    }
}