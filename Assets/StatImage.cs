using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Kontiki;

public class StatImage : MonoBehaviour {
    [Range(0.1f, 1f)]
	public float widthRatio;
    [Range(0.1f, 1f)]
	public float heightRatio;

	public float minSize;
	public float maxSize;

	public float cornerMargin;

    [Range(0.1f, 1f)]
	public float minTransparency;
    [Range(0.1f, 1f)]
	public float maxTransparency;

	public Character character;
	
	private Image _image;
	private Color _imageCol;
	private RectTransform _rt;

	private Rect _rect;
	private Vector2 _position;
	private Vector2 _originalSize;
	
	public enum Type 
	{
		Hunger,
		Thirst
	};

	public Type statType;
	
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
		SetImageAccordingToStatType();
	}

	public void SetImageAccordingToStatType(){
		float percent = 0;

		switch(statType){
			case Type.Hunger:
			percent = character.hunger / Character.hungerMax;
			break;

			case Type.Thirst:
			break;

			default:
			break;
		}
		float newSize = minSize+percent*(maxSize-minSize);
		_rt.sizeDelta = new Vector2(newSize*widthRatio, newSize*heightRatio);
		float newTrans = minTransparency+(percent*(maxTransparency-minTransparency));
		_image.color = new Color(_imageCol.r, _imageCol.b, _imageCol.g, newTrans);

		CalculateImagePosition();
	}

	public void CalculateImagePosition(){
		Vector2 positionVector = new Vector2(_rt.rect.width + _rt.rect.position.x + cornerMargin, _rt.rect.height + _rt.rect.position.y + cornerMargin);
		Rect tempRect = _rt.rect;
		tempRect.position = positionVector;
		_rt.position = tempRect.position;
	}
}
