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

		public Transform tempTrans;

		void Start()
		{
			_iconList 		= new List<Icon>();
			_canvasRect 	= canvasPrefab.GetComponent<RectTransform>().rect;
		}

		void FixedUpdate()
		{
			if(_iconList.Count > 0)
				UpdateIcons();

			if(Input.GetKeyDown(KeyCode.Space))
			{
				CreateIconSeries(iconSprites, tempTrans);
			}
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
						
						if(tempCanvas.childCount == 0)
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

		private void CreateIcon(Sprite iconSprite, Transform trans, Vector2 offset, Canvas parent)
		{
			Icon icon = new Icon(imagePrefab, iconSprite, trans.position, iconDuration);
			icon.image.transform.SetParent(parent.transform, false);
			icon.SetOffset(offset);
			_iconList.Add(icon);
		}

		private void CreateIconSeries(Sprite[] icons, Transform trans)
		{
			Vector2 temp = Vector2.zero;
			Canvas parent = CreateCanvas(trans);
			for(int i = 0; i < icons.Length; i++)
			{
				float count = icons.Length;
				temp.x = (i - (count - 1) / 2) * (icons[i].rect.width / 2 + seriesOffset);
				CreateIcon(icons[i], trans, temp, parent);
			}
		}

		public void CreateIcon_Eat(Transform trans)
		{

		}

		public void CreateIcon_Food(Transform trans)
		{

		}

	}
}
