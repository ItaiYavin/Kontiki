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
		public GameObject iconPrefab;
		[Tooltip("Parent for icons")]
		public Canvas canvasParent;
		
		// ICONS //
		private List<Image> _iconList;

		// Display variables //
		[Header("Display Variables")]
		[Tooltip("Duration icons stay on screen")]
		public float iconDuration;
		
		public float iconsDestroyTime;

		void Start()
		{
			_iconList = new List<Image>();
		}

		void FixedUpdate()
		{
			
			if(iconsDestroyTime < Time.time){
				Clear();
			}
			
			if(Settings.debugIconSystem){
				float startX = -(_iconList.Count/2) * (Settings.iconWidth + Settings.iconOffset);
				Vector3 position = new Vector3();
				for(int i = 0; i < _iconList.Count; i++)
				{
					position.x = startX + i * (Settings.iconWidth + Settings.iconOffset);
					_iconList[i].GetComponent<RectTransform>().sizeDelta = new Vector2(Settings.iconWidth, Settings.iconWidth);
					_iconList[i].GetComponent<RectTransform>().anchoredPosition = position + Settings.iconContainerOffset;
				}
			}
		}

        private void CreateIcon(Sprite iconSprite, Vector3 position, Color col)
		{
			GameObject g = Instantiate(iconPrefab, position, Quaternion.identity) as GameObject;
			g.transform.SetParent(canvasParent.transform, false);
			g.GetComponent<RectTransform>().sizeDelta = new Vector2(Settings.iconWidth, Settings.iconWidth);
			g.GetComponent<RectTransform>().anchoredPosition = position;
			
			Image icon = g.GetComponent<Image>();
			icon.color = col;
			icon.sprite = iconSprite;
			_iconList.Add(icon);
			
		}

		private void CreateIconSeries(Sprite[] icons, Color[] col)
		{
			float startX = -(icons.Length/2) * (Settings.iconWidth + Settings.iconOffset);
			Vector3 position = new Vector3();
			iconsDestroyTime = Time.time + iconDuration;
			for(int i = 0; i < icons.Length; i++)
			{
				position.x = startX + i * (Settings.iconWidth + Settings.iconOffset);
				CreateIcon(icons[i], position, col[i]);
			}
		}


		public void Clear(){
			for (int i = 0; i < _iconList.Count; i++)
			{
				Destroy(_iconList[i].gameObject);
			}
			_iconList.Clear();
		}
		/// <summary>Create an series of icons with only white icons
		/// </summary>
		public void GenerateIcons(params IconType[] icons){ GenerateIcons(new Color(1,1,1), new Color(1,1,1), icons); }
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
					case IconType.QuestObjective:
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
			if(_iconList.Count != 0){
				Clear();
			}
			Sprite[] sprites = new Sprite[icons.Length];
			for(int i = 0; i < icons.Length; i++){
				int type = (int)icons[i];
				sprites[i] = Settings.iconSprites[Settings.iconTypes.IndexOf(icons[i])];
			}

			CreateIconSeries(sprites, col);
		}

	}
}
