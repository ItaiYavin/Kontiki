using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Kontiki {

	public class IconSystem : MonoBehaviour {

	public struct Icon {	
		public Image 	image;
		public Vector3 	inWorldPosition;

		public Icon(Image prefab, Sprite imageSprite, Vector3 pos){
			image 			= Object.Instantiate<Image>(prefab);
			image.sprite 	= imageSprite;
			inWorldPosition = pos;
		}

		public void SetImagePosition(Vector3 pos){
			
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


	void FixedUpdate(){
		if(iconList.Count > 0)
			UpdateIcons();
	}

	public void UpdateIcons(){
		for(int i = 0; i < iconList.Count; i++)
		{
			Vector3 screenPoint = Camera.main.WorldToScreenPoint(iconList[i].inWorldPosition + offset);
			iconList[i].SetImagePosition(screenPoint);
		}
	}

	private void DisplayIcon(Sprite iconSprite, Vector3 pos){
		Icon icon = new Icon(imagePrefab, iconSprite, pos);
		iconList.Add(icon);

	}

	private void DisplayIconSeries(Vector3 pos, Sprite[] icon){

	}

	public void DisplayIcon_Eat(Vector3 pos){

	}

	public void DisplayIcon_Food(Vector3 pos){

	}

	}
}
