using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CoinPackage.Debugging;

public class CheckCollider : MonoBehaviour
{
    [SerializeField]private float id;
    private BoxCollider2D box;
    public float GetId(){return id;}
    private void Start(){
        box = GetComponent<BoxCollider2D>();
    }
    public bool IsTouching(){return box.IsTouchingLayers();}
}
