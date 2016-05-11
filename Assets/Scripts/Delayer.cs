using UnityEngine;
using System.Collections;

public class Delayer : MonoBehaviour{

    static public Delayer Instance;

    public delegate void Callback();

    void Awake(){
        // First we check if there are any other instances conflicting
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        
        Instance = this;

        DontDestroyOnLoad(gameObject);
    }
    
    
    IEnumerator Perform(Callback callback, float delay){

        yield return new WaitForSeconds(delay);
        callback();
    }

    static public void Start(Callback callback, float delay){

        if(Instance != null)
            Instance.StartCoroutine(Instance.Perform(callback, delay));  
        else
            Debug.LogError("cant find the Instance of Delayer?");
    }

}