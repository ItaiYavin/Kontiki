using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Kontiki {
	public class IconSystem : MonoBehaviour 
	{
		// References //
		[Header("References")]
		[Tooltip("Prefab for icon images")]
		public Image imagePrefab;
		[Tooltip("Parent for icons")]
		public Canvas canvasParent;
		
		// ICONS //
		private List<Icon> _iconList;

		// Display variables //
		[Header("Display Variables")]
		[Tooltip("Duration icons stay on screen")]
		public float iconDuration;
		[Tooltip("Position offset between icons in series")]
		public float offset;

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
			float startX = -(_iconList.Count/2) * (Settings.iconWidth + Settings.iconOffset) + offset;
			Vector3 position = new Vector3();
			
			for(int i = 0; i < _iconList.Count; i++)
			{
				if(_iconList[i].image != null){

					if(_iconList[i].destroyTime < Time.time){

						Destroy(_iconList[i].image.gameObject);
						_iconList.RemoveAt(i);
					}
						
					position.x = startX + i * (Settings.iconWidth + Settings.iconOffset) + offset;
					_iconList[i].SetImagePosition(position);
					_iconList[i].SetImageSize(Settings.iconWidth);
				}else{
					_iconList.RemoveAt(i);
				}
			}
		}

		private void CreateIcon(Sprite iconSprite, Vector3 position, Color col)
		{
			Icon icon = new Icon(imagePrefab, iconSprite, position, iconDuration);
			icon.image.color = col;
			icon.image.transform.SetParent(canvasParent.transform, false);
			icon.SetImageSize(Settings.iconWidth);
			_iconList.Add(icon);
		}

		private void CreateIconSeries(Sprite[] icons, Color[] col)
		{
			float startX = -(icons.Length/2) * (Settings.iconWidth + Settings.iconOffset) + offset;
			Vector3 position = new Vector3();
			for(int i = 0; i < icons.Length; i++)
			{
				position.x = startX + i * (Settings.iconWidth + Settings.iconOffset) + offset;
				CreateIcon(icons[i], position, col[i]);
			}
		}

		
		/// <summary>Create an series of icons and colors the first Quest Item or Person
		/// </summary>
		public void GenerateIcons(Color col, params IconType[] icons){ GenerateIcons(col, new Color(1,1,1), icons); }

		
		/// <summary>Create an series of icons and colors the the first and second Quest Item or Person
		/// </summary>
		public void GenerateIcons(Color col, Color col2, params IconType[] icons) { 
			Color[] cols = new Color[icons.Length];
			
			bool firstSymbol = true;

			for(int i = 0; i < icons.Length; i++){
				switch(icons[i]){
					case IconType.Quest:
						if(firstSymbol){
							cols[i] = col;
							firstSymbol = false;
						}
						else 
							cols[i] = col2;
					break;

					case IconType.Person:
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

			GenerateIcons(cols, icons); 
		}

		/// <summary>Create an series of icons and colors them pairwise
		/// </summary>
		public void GenerateIcons(Color[] col, params IconType[] icons)
		{
			Sprite[] sprites = new Sprite[icons.Length];
			Debug.Log("in here");
			for(int i = 0; i < icons.Length; i++){
				int type = (int)icons[i];
				sprites[i] = Settings.iconSprites[Settings.iconTypes.IndexOf(icons[i])];
			}

			CreateIconSeries(sprites, col);
		}

	}
}
