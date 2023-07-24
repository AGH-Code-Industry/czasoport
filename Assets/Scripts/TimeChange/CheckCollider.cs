using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CoinPackage.Debugging;

public class CheckCollider : MonoBehaviour
{
    bool isTouching = false;
    BoxCollider2D box;
    private void Awake(){
        box = GetComponent<BoxCollider2D>();
        box.isTrigger = true;
    }

    public bool isNotTouching(){
        return !isTouching;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(!other.isTrigger) isTouching = true;    
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(!other.isTrigger) isTouching = false;    
    }
}
