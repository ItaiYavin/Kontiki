using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Kontiki {
	public class IconSystem : MonoBehaviour {

	public struct Icon {	
		public Image 	image;
		public Vector3 	inWorldPosition;
		public float 	timeAtInit;

		public Icon(Image prefab, Sprite imageSprite, Vector3 pos, float duration){
			image 			= Object.Instantiate<Image>(prefab);
			image.sprite 	= imageSprite;
			inWorldPosition = pos;
			timeAtInit 		= Time.time + duration;
		}

		public void SetImagePosition(Vector2 pos){
			image.GetComponent<RectTransform>().anchoredPosition = pos;
		}
	}

	// ICONS //
	public List<Icon> 	iconList;
	public Sprite[]		iconSprites;
	public Image		imagePrefab;

	// Display variables //
	public Vector3 		offset;
	public Canvas 		canvas;
	public float 		iconDuration;
	private Rect 		canvasRect;

	void Start(){
		iconList = new List<Icon>();
		canvasRect = canvas.GetComponent<RectTransform>().rect;
	}

	void FixedUpdate(){
		if(iconList.Count > 0)
			UpdateIcons();

		if(Input.GetKeyDown(KeyCode.Space)){
			CreateIcon(iconSprites[0], new Vector3(0,1,0));
		}
	}

	public void UpdateIcons(){
		for(int i = 0; i < iconList.Count; i++)
		{
			Vector3 screenPoint = Camera.main.WorldToScreenPoint(iconList[i].inWorldPosition + offset);
			iconList[i].SetImagePosition(new Vector2(screenPoint.x - canvasRect.width/2, screenPoint.y - canvasRect.height/2));

			if(iconList[i].timeAtInit - Time.time < 0){
				Icon temp = iconList[i];
				iconList.RemoveAt(i);
				Destroy(temp.image.gameObject);
			}
		}

	}

	private void CreateIcon(Sprite iconSprite, Vector3 pos){
		Icon icon = new Icon(imagePrefab, iconSprite, pos, iconDuration);
		icon.image.transform.parent = canvas.transform;
		iconList.Add(icon);
	}

	private void CreateIconSeries(Vector3 pos, Sprite[] icon){

	}

	public void CreateIcon_Eat(Vector3 pos){

	}

	public void CreateIcon_Food(Vector3 pos){

	}

	}
}
