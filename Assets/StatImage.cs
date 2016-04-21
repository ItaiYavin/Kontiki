using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Kontiki;

public class StatImage : MonoBehaviour {

	public float minSize;
	public float maxSize;

	public float cornerMargin;

	public float minTransparency;
	public float maxTransparency;

	public Character character;
	
	private Image _image;
	private Color _imageCol;
	private RectTransform _rt;

	private Rect _rect;
	private Vector2 _position;
	private Vector2 _originalSize;
	
	// Use this for initialization
	void Start () {
		_image = GetComponent<Image>();
		_imageCol = _image.color;
		_rt = _image.GetComponent<RectTransform>();

		_position = _rect.position;
		_originalSize = _rect.size;

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		SetImageAccordingToHunger();
	}

	public void SetImageAccordingToHunger(){
		float percent;
		percent = character.hunger / Character.hungerMax;
		float newSize = minSize+percent*(maxSize-minSize);
		_rt.sizeDelta = new Vector2(newSize, newSize);
		float newTrans = minTransparency+(percent*(maxTransparency-minTransparency));
		_image.color = new Color(_imageCol.r, _imageCol.b, _imageCol.g, newTrans);

		CalculateImagePosition();
	}

	public void CalculateImagePosition(){
		float sizeChange = _rt.rect.size.x - _originalSize.x;
		Vector2 positionVector = new Vector2(sizeChange + _rt.rect.position.x + cornerMargin, sizeChange + _rt.rect.position.y + cornerMargin);
		Rect tempRect = _rt.rect;
		tempRect.position = positionVector;
		_rt.position = tempRect.position;
	}
}
