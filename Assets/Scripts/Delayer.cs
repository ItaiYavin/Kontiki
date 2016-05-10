using UnityEngine;
using System.Collections;

public class Delayer : MonoBehaviour{

    static public Delayer Instance;

    public delegate void Callback();

    void Awake(){

        Instance = this; 

    }
    IEnumerator Perform(Callback callback, float delay){

        yield return new WaitForSeconds(delay);
        callback();
    }

    static public void Start(Callback callback, float delay){

        Instance.StartCoroutine(Instance.Perform(callback, delay)); 

    }

}