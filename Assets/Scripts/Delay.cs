using UnityEngine;
using System.Collections;

namespace Kontiki{
	public class Delay : MonoBehaviour{

		public void StartDelay(float seconds){
			StartCoroutine(DelayForSeconds(seconds));
		}

	    IEnumerator DelayForSeconds(float seconds){
	        yield return new WaitForSeconds(seconds);
	        Destroy(this.gameObject);
	    }
	}
}
