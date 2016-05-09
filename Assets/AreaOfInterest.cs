using UnityEngine;
using System.Collections;

namespace Kontiki
{
	
	public class AreaOfInterest : MonoBehaviour {

		public Material material;
		
		public float areaOfInterestMaxSize = 50f;
		public float areaOfInterestMinSize = 5f;
		public float animationDuration = 2f;
		
		// Use this for initialization
		void Awake () {
			if(material == null){
				material = new Material(GetComponent<MeshRenderer>().material);
				GetComponent<MeshRenderer>().material = material;
				material.color = new Color(1,1,1,0);
			}
			transform.localScale = new Vector3(areaOfInterestMaxSize, areaOfInterestMaxSize, areaOfInterestMaxSize);
		}
		
		public void RandomlyMoveGameObjectWithinRange(float scale, Vector3 position){
			float ranX = Random.Range(-scale/4, scale/4);
			float ranZ = Random.Range(-scale/4, scale/4);
			
			ChangePosition(position + new Vector3(ranX, 0, ranZ));
		}

		

		public void AdjustAreaOfInterestInWorld(float reduction, Vector3 position){
			float newScale = transform.localScale.x * (1 - reduction); //Resize areaOfInterest
			if(newScale < areaOfInterestMinSize)
				newScale = areaOfInterestMinSize;

			ChangeScale(newScale);
			RandomlyMoveGameObjectWithinRange(newScale, position); //Move areaOfInterest so that position is still wihtin areaOfInterest
		}
		
		public void ChangeColor(Color color){
			if(material == null){
				material = new Material(GetComponent<MeshRenderer>().material);
				GetComponent<MeshRenderer>().material = material;
				material.color = new Color(1,1,1,0);
			}
			StartCoroutine(Routine_ChangeColor(color, animationDuration));
			
		}
		
		public void ChangeScale(float scale){
			StartCoroutine(Routine_ChangeScale(new Vector3(scale,scale,scale), animationDuration));
		}
		
		public void ChangePosition(Vector3 position){
			StartCoroutine(Routine_ChangePosition(position, animationDuration));
		}
		
        
        IEnumerator Routine_ChangeColor(Color endColor, float duration){
            Color startColor = material.color;
            
            float startTime = Time.time;
            float endTime = startTime + duration;
            
            while(endTime > Time.time){
                float t = (Time.time - startTime)/duration;
                material.color = Color.Lerp(startColor, endColor, t);
                yield return null;
            }
            
            material.color = endColor;
        }
        
        IEnumerator Routine_ChangeScale(Vector3 endScale, float duration){
            Vector3 startScale = transform.localScale;
            
            float startTime = Time.time;
            float endTime = startTime + duration;
            
            while(endTime > Time.time){
                float t = (Time.time - startTime)/duration;
                transform.localScale = Vector3.Lerp(startScale, endScale, t);
                yield return null;
            }
            
                transform.localScale = endScale;
        }
        
        IEnumerator Routine_ChangePosition(Vector3 endPosition, float duration){
			Vector3 startPosition = transform.position;            
            float startTime = Time.time;
            float endTime = startTime + duration;
            
            while(endTime > Time.time){
                float t = (Time.time - startTime)/duration;
                transform.position = Vector3.Lerp(startPosition, endPosition, t);
                yield return null;
            }
            
            transform.position = endPosition;
        }
    }
}