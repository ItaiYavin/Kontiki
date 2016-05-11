using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;
using System.Collections;

namespace Kontiki
{	
	public class HungerScreenPenalty : MonoBehaviour {

		public Character player;
		public Image hungerSliderBackground;
		public Color hungerSliderFlashColor;

		private Color originalColor;

		[Range(0,100)]
		public float threshold;
		private BlurOptimized blur;
		private Fisheye fisheye;

		// Use this for initialization
		void Start () {
			blur = GetComponent<BlurOptimized>();
			fisheye = GetComponent<Fisheye>();
			originalColor = hungerSliderBackground.color;
		}
		
		// Update is called once per frame
		void Update () {
			if(player.hunger > threshold){
				HungerEffect();
			} else if (player.hunger < threshold && hungerSliderBackground.color != originalColor){
				hungerSliderBackground.color = originalColor;
			}
		}

		public void HungerEffect(){
			float percent = (player.hunger-threshold)/(Settings.hungerRange.max-threshold);
			SetBlurSize(percent*3f);

			float x = ((Mathf.Sin(Time.time)+2)/10)*percent;
			SetFisheyeStrength(x, x);	

			float t = ((Mathf.Sin(Time.time*(percent*10))+2)/2)*percent;
			SliderColorFlash(t);
		}

		public void SliderColorFlash(float t){
			hungerSliderBackground.color = Color.Lerp(originalColor, hungerSliderFlashColor, t);
		}

		public void SetBlurSize(float value){
			blur.blurSize = value;
		}

		public void SetFisheyeStrength(float x, float y){
			fisheye.strengthX = x;
			fisheye.strengthY = y;
		}
	}
}