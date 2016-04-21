using UnityEngine;
using System.Collections;
using Kontiki;

public class Interact : MonoBehaviour {

	private Character character;

	// Use this for initialization
	void Start () {
		character = GetComponent<Character>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.X))
			GiveApple();

	}

	public void GiveApple(){
		Collider[] colliders = Physics.OverlapSphere(transform.position, character.pickupRange);

        foreach (Collider c in colliders) {
            GameObject foundObject = c.gameObject;
            if (foundObject.tag == "NPC")
            {
                for(int i = 0; i < character.GetInventory().inventorySize; i++){
                	if(character.GetInventory().GetInventoryItem(i) is EdibleItem){
                		character.GetInventory().GetInventoryItems()[i] = null;
                		c.transform.GetComponent<Rigidbody>().AddForce(Vector3.up*300);
                		Debug.Log("YAY, thanks for the apple!");
                	}
                }
            }
        }
	}
}
