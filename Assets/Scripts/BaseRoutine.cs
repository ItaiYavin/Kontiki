using UnityEngine;
using System.Collections;
using Kontiki.AI;
using Kontiki;

public class BaseRoutine : MonoBehaviour {
    public Transform home;
    public Trader trader;

    public bool hasQuestToOffer;
    public Quest questOffer;
    
    void Start()
    {
        hasQuestToOffer = Random.Range(0f, 1f) >= 0.5f;


    }
}
