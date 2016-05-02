using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Kontiki {
	public class IconSystem : MonoBehaviour 
	{
		public enum IconTypes{
			Food = 0,
			Eat = 1,
			Trade = 2,
			Quest = 3,
			QuestItem = 4,
			Person = 5,
			Question = 6
		}

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
		[Tooltip("Prefab for icon canvas")]
		public Canvas 		canvasPrefab;
		
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

		private Rect 		_canvasRect;

        // Static singleton property
        public static IconSystem Instance { get; private set; }

        void Awake()
        {
            // First we check if there are any other instances conflicting
            if (Instance != null && Instance != this)
            {
                // If that is the case, we destroy other instances
                Destroy(gameObject);
            }

            // Here we save our singleton instance
            Instance = this;

            // Furthermore we make sure that we don't destroy between scenes (this is optional)
            DontDestroyOnLoad(gameObject);
        }

		void Start()
		{
			_iconList 		= new List<Icon>();
			_canvasRect 	= canvasPrefab.GetComponent<RectTransform>().rect;
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
				if(_iconList[i].image != null){
					_iconList[i].SetImagePosition(offset);

					if(_iconList[i].timeAtInit - Time.time < 0)
					{
						Icon tempIcon = _iconList[i];
						Transform tempCanvas = tempIcon.image.transform.parent;
						_iconList.RemoveAt(i);

						Destroy(tempIcon.image.gameObject);
						Destroy(tempCanvas.gameObject);
					}
				}
				else 
					_iconList.RemoveAt(i);
			}

		}

		public Canvas CreateCanvas(Transform parent){
			Canvas can;
			can	= Object.Instantiate<Canvas>(canvasPrefab);
			can.renderMode = RenderMode.WorldSpace;
			can.transform.SetParent(parent, false);
			
			RectTransform canRect = can.GetComponent<RectTransform>();
			canRect.localScale = new Vector3(1,1,1);
			canRect.sizeDelta = new Vector2(20, 20);
			canRect.position = new Vector2(-10, -10);

			return can;
		}

		private void CreateIcon(Sprite iconSprite, Transform trans, Vector2 offset, Canvas parent, Color col)
		{
			Icon icon = new Icon(imagePrefab, iconSprite, trans.position, iconDuration);
			icon.image.color = col;
			icon.image.transform.SetParent(parent.transform, false);
			icon.SetOffset(offset);
			_iconList.Add(icon);
		}

		private void CreateIconSeries(Sprite[] icons, Transform trans, Color[] col)
		{
			Vector2 temp = Vector2.zero;
			Canvas parent = CreateCanvas(trans);
			for(int i = 0; i < icons.Length; i++)
			{
				float count = icons.Length;
				temp.x = (i - (count - 1) / 2) * (icons[i].rect.width / 2 + seriesOffset);
				CreateIcon(icons[i], trans, temp, parent, col[i]);
			}
		}

		public void RequestIcons(Transform trans, Color col, params IconTypes[] icons){ RequestIcons(trans, col, new Color(1,1,1), icons); }

		public void RequestIcons(Transform trans, Color col, Color col2, params IconTypes[] icons) { 
			Color[] cols = new Color[icons.Length];
			
			bool firstSymbol = true;

			for(int i = 0; i < icons.Length; i++){
				switch(icons[i]){
					case IconTypes.QuestItem:
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

			RequestIcons(trans, cols, icons); 
		}

		public void RequestIcons(Transform trans, Color[] col, params IconTypes[] icons)
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
