using InteractableObjectSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : InteractableObject {
    [SerializeField] private GameObject _bigMap;

    private void Start() {
        CloseMap();
        Interactions.Interactions.Instance.onFocusChange += (object sender, EventArgs e) => CloseMap();
    }

    public override void InteractionHand() {
        _bigMap.SetActive(!_bigMap.activeSelf);
    }

    private void CloseMap() {
        _bigMap.SetActive(false);
    }
}