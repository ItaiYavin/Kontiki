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
				iconOffset 		= offset;
			}

			public void SetImagePosition(Vector2 pos)
			{
				image.GetComponent<RectTransform>().anchoredPosition = pos + iconOffset;
			}

			public void SetImageSize(float size){
				image.GetComponent<RectTransform>().sizeDelta = new Vector2(size, size);
			}
		}

		// References //
		[Header("References")]
		[Tooltip("Prefab for icon images")]
		public Image		imagePrefab;
		[Tooltip("Parent for icons")]
		public Canvas 		canvasParent;
		
		// ICONS //
		private List<Icon> 	_iconList;
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

		void Start()
		{
			_iconList 		= new List<Icon>();
		}

		void FixedUpdate()
		{
			if(_iconList.Count > 0)
				UpdateIcons();
		}

		public void UpdateIcons()
		{
			for(int i = 0; i < _iconList.Count; i++)
			{
				_iconList[i].SetImagePosition(offset);
				if(_iconList[i].image != null){

					if(_iconList[i].timeAtInit - Time.time < 0)
					{
						Icon tempIcon = _iconList[i];
						_iconList.RemoveAt(i);

						Destroy(tempIcon.image.gameObject);
					}
				}
				else 
					_iconList.RemoveAt(i);
			}

		}

		private void CreateIcon(Sprite iconSprite, Transform trans, Vector2 iconOffset, Color col)
		{
			Icon icon = new Icon(imagePrefab, iconSprite, trans.position, iconDuration);
			icon.image.color = col;
			icon.image.transform.SetParent(canvasParent.transform, true);
			icon.SetOffset(iconOffset);
			_iconList.Add(icon);
		}

		private void CreateIconSeries(Sprite[] icons, Transform trans, Color[] col)
		{
			Vector2 temp = Vector2.zero;
			for(int i = 0; i < icons.Length; i++)
			{
				float count = icons.Length;
				temp.x = (i - (count - 1) / 2) * (icons[i].rect.width / 2 + seriesOffset);
				CreateIcon(icons[i], trans, temp, col[i]);
			}
		}

		
		/// <summary>Create an series of icons and colors the first Quest Item or Person
		/// </summary>
		public void GenerateIcons(Transform trans, Color col, params IconTypes[] icons){ GenerateIcons(trans, col, new Color(1,1,1), icons); }

		
		/// <summary>Create an series of icons and colors the the first and second Quest Item or Person
		/// </summary>
		public void GenerateIcons(Transform trans, Color col, Color col2, params IconTypes[] icons) { 
			Color[] cols = new Color[icons.Length];
			
			bool firstSymbol = true;

			for(int i = 0; i < icons.Length; i++){
				switch(icons[i]){
					case IconTypes.Quest:
						if(firstSymbol){
							cols[i] = col;
							firstSymbol = false;
						}
						else 
							cols[i] = col2;
					break;

					case IconTypes.Person:
						if(firstSymbol){
							cols[i] = col;
							firstSymbol = false;
						}
						else 
							cols[i] = col2;
					break;

					default:
						cols[i] = new Color(1f,1f,1f);
					break;
				}
			}

			GenerateIcons(trans, cols, icons); 
		}

		/// <summary>Create an series of icons and colors them pairwise
		/// </summary>
		public void GenerateIcons(Transform trans, Color[] col, params IconTypes[] icons)
		{
			Sprite[] sprites = new Sprite[icons.Length];

			for(int i = 0; i < icons.Length; i++){
				int type = (int)icons[i];
				sprites[i] = iconSprites[type];
			}

			CreateIconSeries(sprites, trans, col);
		}

	}
}
