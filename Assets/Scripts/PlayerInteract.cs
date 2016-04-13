using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityStandardAssets.Cameras;

public class PlayerInteract : MonoBehaviour {

    public bool buttonDown = false;
    public bool isInteracting = false;
    
    private BoatController boat;
    private Vector3 boatPosition;
    
    private ThirdPersonUserControl userControl;
    
    [SerializeField]
    private AutoCam autoCamera;
    
    void Start(){
        userControl = GetComponent<ThirdPersonUserControl>();
        
    }

	
	// Update is called once per frame
	void Update () {
        if(isInteracting){
            transform.localPosition = boatPosition;
            if(Input.GetKeyDown(KeyCode.E)){
                StopInteraction();
            }
        }else
	        buttonDown = Input.GetKey(KeyCode.E);
    }
    
    public void StartInteraction(BoatController boat){
        isInteracting = true;
        userControl.enabled = false;
        this.boat = boat;
        boat.StartControl();
        GetComponent<ThirdPersonCharacter>().Move(Vector3.zero, true, false);
        autoCamera.SetTarget(boat.transform);
        transform.parent = boat.transform;
        boatPosition = transform.localPosition;
    }
    
    public void StopInteraction(){
                isInteracting = false;

        userControl.enabled = true;
        boat.StopControl();
            transform.localPosition = boatPosition;
        transform.parent = null;
        GetComponent<ThirdPersonCharacter>().Move(Vector3.zero, false, false);
        autoCamera.SetTarget(transform);
    }
    
    
}
