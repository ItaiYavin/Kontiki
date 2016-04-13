using UnityEngine;
using System.Collections;

public class Seat : MonoBehaviour {
    
    [SerializeField]
    private BoatController boat;
    
    
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
    
    void OnTriggerStay(Collider coll){
        if(coll.gameObject.tag == "Player"){
            PlayerInteract player = coll.GetComponent<PlayerInteract>();
            if(player.buttonDown){
                player.StartInteraction(boat);
            }
        }
    }
}
