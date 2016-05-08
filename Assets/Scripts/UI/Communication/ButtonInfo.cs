using UnityEngine;
using UnityEngine.UI;

namespace Kontiki
{
    public class ButtonInfo : MonoBehaviour{
        public int index = -1;
        public Image icon;
        [HideInInspector] public WindowsHandler windowHandler;
        
        public void OnButtonClick(){
            windowHandler.OnButtonClick(index);
        }
    }
}