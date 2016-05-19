using UnityEngine;
using System.Collections;

namespace Kontiki
{
	
	public class AreaOfInterest : MonoBehaviour {

		public Material material;
		
		public Transform target;
		
		public float areaOfInterestMaxSize = 50f;
		public float areaOfInterestMinSize = 5f;
		public float animationDuration = 10f;
		
		public float AOIDistanceToPlayer = 1.25f;
		public bool colorChanging = false;
		
		public float maxAlpha = 0.5f;
		public bool isVisible = true;
		
		private MeshRenderer cylinderRenderer;
		private bool stopNextColorChange = false;
		public Transform cylinder;
		
		public float startUpdateTime = 0f;
		
		// Use this for initialization
		void Awake () {
			if(material == null){
				material = new Material(transform.GetChild(0).GetComponent<MeshRenderer>().material);
				transform.GetChild(0).GetComponent<MeshRenderer>().material = material;
				material.color = new Color(1,1,1,0);
			}
			cylinderRenderer = cylinder.GetComponent<MeshRenderer>();
			cylinderRenderer.material = material;

		    transform.localScale = Vector3.zero;
		}
		
		
		void FixedUpdate(){
			if(startUpdateTime < Time.time){
				float distance = (Settings.player.transform.position - target.position).magnitude;
				float limit = AOIDistanceToPlayer * transform.localScale.x;
				AdjustAreaOfInterestInWorld(0f, target.position);
			
				if(isVisible){
					if(distance < limit && !colorChanging && material.color.a != 0f){
						Color c = material.color;
						c.a = 0f;
						
						ChangeColor(c);
					}
				}else{
					if(distance > limit && !colorChanging && material.color.a != maxAlpha){
						Color c = material.color;
						c.a = maxAlpha;
						
						ChangeColor(c);
					}
				}
			}
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
		
		public void ChangeColor(Color color){ChangeColor(color, animationDuration);}
		public void ChangeColor(Color color, float duration){
			if(material == null){
				material = new Material(GetComponent<MeshRenderer>().material);
				GetComponent<MeshRenderer>().material = material;
				material.color = new Color(1,1,1,0);
			}
			StartCoroutine(Routine_ChangeColor(color, duration));
			
		}
		
		public void ChangeScale(float scale){ChangeScale(scale, animationDuration);}
		public void ChangeScale(float scale, float duration){
			StartCoroutine(Routine_ChangeScale(new Vector3(scale,scale,scale), duration));
		}
		
		public void ChangePosition(Transform target){ChangePosition(target, animationDuration);}
		public void ChangePosition(Transform target, float duration){
			StartCoroutine(Routine_ChangePosition(target, duration));
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
			
			isVisible = material.color.a != 0f;
        }
        
        IEnumerator Routine_ChangeScale(Vector3 endScale, float duration){
            Vector3 startScale = transform.localScale;
            
            float startTime = Time.time;
			cylinder.localScale = new Vector3(1,500 / endScale.y,1);
			cylinder.localPosition = new Vector3(0,-cylinder.localScale.y / 2,0);
            float endTime = startTime + duration;
            
            while(endTime > Time.time){
                float t = (Time.time - startTime)/duration;
                transform.localScale = Vector3.Lerp(startScale, endScale, t);
                yield return null;
            }
            
                transform.localScale = endScale;
        }
        
        IEnumerator Routine_ChangePosition(Transform endTarget, float duration){
			Vector3 startPosition = transform.position;            
            float startTime = Time.time;
            float endTime = startTime + duration;
            
            while(endTime > Time.time){
				if(stopNextColorChange) endTime = Time.time;
                float t = (Time.time - startTime)/duration;
                transform.position = Vector3.Lerp(startPosition, endTarget.position, t);
                yield return null;
            }
            
            transform.position = endTarget.position;
        }
    }
}