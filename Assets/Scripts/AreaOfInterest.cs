using UnityEngine;
using System.Collections;

namespace Kontiki
{
	
	public class AreaOfInterest : MonoBehaviour {

		public Material material;
		
		public Transform target;
		
		public float areaOfInterestMaxSize = 50f;
		public float areaOfInterestMinSize = 5f;
		public float animationDuration = 2f;
		
		public float AOIDistanceToPlayer = 1.25f;
		public bool colorChanging = false;
		
		public float maxAlpha = 0.5f;
		
		private MeshRenderer cylinderRenderer;
		public Transform cylinder;
		
		// Use this for initialization
		void Awake () {
			if(material == null){
				material = new Material(GetComponent<MeshRenderer>().material);
				GetComponent<MeshRenderer>().material = material;
				material.color = new Color(1,1,1,0);
			}
			cylinderRenderer = cylinder.GetComponent<MeshRenderer>();
			cylinderRenderer.material = material;
			
			transform.localScale = new Vector3(areaOfInterestMaxSize, areaOfInterestMaxSize, areaOfInterestMaxSize);
		}
		
		
		void FixedUpdate(){
			float distance = (Settings.player.transform.position - target.position).magnitude;
			float limit = AOIDistanceToPlayer * transform.localScale.x;
			if(distance > limit){
				AdjustAreaOfInterestInWorld(0f, target.position);
				if(!colorChanging && material.color.a != maxAlpha){
					
					Color c = material.color;
					c.a = maxAlpha;
					
					ChangeColor(c);
				}
			}else if(!colorChanging && material.color.a != 0){
				Color c = material.color;
				c.a = 0f;
				
				ChangeColor(c);
			}
		}
		
		public void RandomlyMoveGameObjectWithinRange(float scale, Vector3 position){
			//float ranX = Random.Range(-scale/4, scale/4);
			//float ranZ = Random.Range(-scale/4, scale/4);
			// / + new Vector3(ranX, 0, ranZ)
			ChangePosition(position);
		}

		

		public void AdjustAreaOfInterestInWorld(float reduction, Vector3 position){
			float newScale = transform.localScale.x * (1 - reduction); //Resize areaOfInterest
			if(newScale < areaOfInterestMinSize)
				newScale = areaOfInterestMinSize;
			
			if(reduction != 0)
				ChangeScale(newScale);
			transform.position = position;
			//RandomlyMoveGameObjectWithinRange(newScale, position); //Move areaOfInterest so that position is still wihtin areaOfInterest
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
            colorChanging = true;
            float startTime = Time.time;
            float endTime = startTime + duration;
            
            while(endTime > Time.time){
                float t = (Time.time - startTime)/duration;
                material.color = Color.Lerp(startColor, endColor, t);
                yield return null;
            }
            colorChanging = false;
            material.color = endColor;
        }
        
        IEnumerator Routine_ChangeScale(Vector3 endScale, float duration){
            Vector3 startScale = transform.localScale;
            
            float startTime = Time.time;
			cylinder.localScale = new Vector3(1,100 / endScale.y,1);
			cylinder.localPosition = new Vector3(0,cylinder.localScale.y,0);
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