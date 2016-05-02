using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Kontiki {
	public class IconSystem : MonoBehaviour 
	{

		public struct Icon 
		{	
			public Image 	image;
			public Vector3 	inWorldPosition;
			public float 	timeAtInit;
			public Vector2 	iconOffset;

			public Icon(Image prefab, Sprite imageSprite, Vector3 pos, float duration)
			{
				iconOffset 		= Vector3.zero;
				image 			= Object.Instantiate<Image>(prefab);
				image.sprite 	= imageSprite;
				inWorldPosition = pos;
				timeAtInit 		= Time.time + duration;
			}

			public void SetOffset(Vector3 offset){
				iconOffset = offset;
			}

			public void SetImagePosition(Vector2 pos)
			{
				image.GetComponent<RectTransform>().anchoredPosition = pos + iconOffset;
			}
		}

		// References //
		[Header("References")]
		[Tooltip("Prefab for icon images")]
		public Image		imagePrefab;
		[Tooltip("Reference to the Canvas that renders the icons")]
		public Canvas 		canvas;
		
		// ICONS //
		private List<Icon> 	iconList;
		[Header("Icon sprites")]
		[Tooltip("Array of icon sprites")]
		public Sprite[]		iconSprites;

		// Display variables //
		[Header("Display Variables")]
		[Tooltip("Duration icons stay on screen")]
		public float 		iconDuration;
		[Tooltip("Position offset between icons in series")]
		public float 		seriesOffset;
		[Tooltip("Position offset of icon in world space")]
		public Vector3 		offset;
		private Rect 		canvasRect;

		void Start()
		{
			iconList = new List<Icon>();
			canvasRect = canvas.GetComponent<RectTransform>().rect;
		}

		void FixedUpdate()
		{
			if(iconList.Count > 0)
				UpdateIcons();

			if(Input.GetKeyDown(KeyCode.Space))
			{
				CreateIconSeries(iconSprites, new Vector3(-3.5f,1,2));
			}
		}

		public void UpdateIcons()
		{
			for(int i = 0; i < iconList.Count; i++)
			{
				Vector3 screenPoint = Camera.main.WorldToScreenPoint(iconList[i].inWorldPosition + offset);
				iconList[i].SetImagePosition(new Vector2(screenPoint.x - canvasRect.width/2, screenPoint.y - canvasRect.height/2));

				if(iconList[i].timeAtInit - Time.time < 0)
				{
					Icon temp = iconList[i];
					iconList.RemoveAt(i);
					Destroy(temp.image.gameObject);
				}
			}

		}

		private void CreateIcon(Sprite iconSprite, Vector3 pos, Vector2 offset)
		{
			Icon icon = new Icon(imagePrefab, iconSprite, pos, iconDuration);
			icon.image.transform.parent = canvas.transform;
			icon.SetOffset(offset);
			iconList.Add(icon);
		}

		private void CreateIconSeries(Sprite[] icons, Vector3 pos)
		{
			Vector2 temp = Vector2.zero;
			for(int i = 0; i < icons.Length; i++)
			{
				float count = icons.Length;
				temp.x = (i - (count - 1) / 2) * (icons[i].rect.width / 2 + seriesOffset);
				CreateIcon(icons[i], pos, temp);
			}
		}

		public void CreateIcon_Eat(Vector3 pos)
		{

		}

		public void CreateIcon_Food(Vector3 pos)
		{

		}

	}
}
