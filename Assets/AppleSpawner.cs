using UnityEngine;
using System.Collections;

public sealed class AppleSpawner : MonoBehaviour {

	
	
	[SerializeField] private GameObject prefab_apple;
	
	
	[SerializeField] private float minSpawnRadius = 1;
	
	[SerializeField] private float maxSpawnRadius = 3;
	
	[SerializeField] private int initNumApples = 1;
	
	[SerializeField] private float appleSpawnDelay = 0.5f;
	private float nextSpawn = 0;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < initNumApples; i++)
		{
			spawnApple();
		}
	}
	
	void FixedUpdate(){
		if(Time.time > nextSpawn){
			
			spawnApple();
			nextSpawn = Time.time + appleSpawnDelay;
		}
	}
	
	private void spawnApple(){
		GameObject g = (GameObject)Instantiate(prefab_apple,transform.position + new Vector3( (minSpawnRadius + (maxSpawnRadius - minSpawnRadius)) * Mathf.Sin(Random.Range(-1f,1f)),0,(minSpawnRadius + (maxSpawnRadius - minSpawnRadius)) * Mathf.Cos(Random.Range(-1f,1f))), Random.rotation);		
		g.transform.parent = transform;
	}
}
