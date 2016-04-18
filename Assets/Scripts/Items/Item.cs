using UnityEngine;

namespace Kontiki
{
    public abstract class Item : MonoBehaviour
    {
        public abstract bool UseItem(Character person);
    }
}
