using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Kontiki
{
	public sealed class ResourceSpawner : MonoBehaviour {

		public bool debug; // Will show resource areas as gizmos if on
		public GameObject prefab;

		private List<ResourceAreaCircle> areaList;

		public GameObject resourceContainer;

		// Use this for initialization
		void Start () {
			areaList = new List<ResourceAreaCircle>();
			PopulateListWithResourceAreas(new Vector3(0,179,0), 1000, 10, 100, 50, 10, 100);
			FillResourceAreas();
		}
	

		public void PopulateListWithResourceAreas(	Vector3 mapCenter, float mapRadius, float minAreaRadius, float maxAreaRadius, int areaAmount, float minAreaRichness, float maxAreRichness){
			Vector3 areaPos;
			float randomRadius;
			float randomRichness;
			float randomRotation;
			float randomPosScalar;

			for(int i = 0; i < areaAmount; i++){
				randomRotation = Random.Range(0, 360);
				randomPosScalar = Random.Range(0.1f, mapRadius-maxAreaRadius);
				areaPos = new Vector3(1f,0f,0f);
				areaPos = Quaternion.Euler(0, randomRotation, 0) * areaPos;
				areaPos *= randomPosScalar;
				areaPos += mapCenter;

				randomRadius = Random.Range(minAreaRadius, maxAreaRadius);
				randomRichness = Random.Range(minAreaRichness, maxAreRichness);

				ResourceAreaCircle g = new ResourceAreaCircle(areaPos, randomRadius, randomRichness);
				areaList.Add(g);
			}
		}

		public void FillResourceAreas(){// fills areas with resources
			Vector3 randomPos = new Vector3(0,0,0);
			float randomRotation;
			float randomPosScalar;

			for(int i = 0; i < areaList.Count; i++){
				Debug.Log(areaList[i].richness);
				for(int j = 0; j < areaList[i].richness; j++){
					randomRotation = Random.Range(0, 360);
					randomPosScalar = Random.Range(0.0f, areaList[i].radius);
					randomPos = new Vector3(1f,0f,0f);
					randomPos = Quaternion.Euler(0, randomRotation, 0) * randomPos;
					randomPos *= randomPosScalar;
					randomPos += areaList[i].centerPosition;

            		GameObject g = Instantiate(prefab, randomPos, Quaternion.identity) as GameObject;
            		g.transform.parent = resourceContainer.transform;

            		areaList[i].resourceList.Add(g);
				}
			}
		}

		public struct ResourceAreaCircle{
			public List<GameObject> resourceList;
			public Vector3 centerPosition;
			public float radius;
			public float richness; // Resource amount scalar
			//TODO: Resource re-generation over time

			public ResourceAreaCircle(Vector3 pos, float rad, float rich){
				centerPosition = pos;
				radius = rad;
				richness = rich;
				resourceList = new List<GameObject>();
			}
		};

    	void OnDrawGizmos() {
    		if(debug)
	    		if(areaList != null){
		    		for(int i = 0; i < areaList.Count; i++){
				        Gizmos.color = Color.white;
				        Gizmos.DrawWireSphere(areaList[i].centerPosition, areaList[i].radius);
			    	}
		    	}
	    }
	}
}