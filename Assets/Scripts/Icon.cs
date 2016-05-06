using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Kontiki{
    public class Icon 
    {	
        public Image 	image;
        public float 	destroyTime;
        

        public Icon(Image prefab, Sprite imageSprite, Vector3 pos, float duration)
        {
            image 			= Object.Instantiate(prefab,pos,Quaternion.identity) as Image;
            image.sprite 	= imageSprite;
            destroyTime 	= Time.time + duration;
        }

        public void SetImagePosition(Vector3 pos)
        {
            image.GetComponent<RectTransform>().anchoredPosition = pos + new Vector3(Settings.iconOffset,0,0);
        }

        public void SetImageSize(float size){
            image.GetComponent<RectTransform>().sizeDelta = new Vector2(size, size);
        }
    }
}