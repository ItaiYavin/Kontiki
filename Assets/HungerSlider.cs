using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Kontiki;

public class HungerSlider : MonoBehaviour {

	public Color maxHungerColor = Color.red;
	public Color minHungerColor = Color.green;

	public Character character;
	public Image fill;

	private Slider slider;
	private Color col;

	// Use this for initialization
	void Start () {
		slider = GetComponent<Slider>();
		slider.maxValue = Character.hungerMax;
	}
	
	// Update is called once per frame
	void Update () {
		RefreshSlider();
	}

	public void RefreshSlider(){
		slider.value = character.hunger;
		fill.color = Color.Lerp(minHungerColor, maxHungerColor, (character.hunger/Character.hungerMax));
	}
}
