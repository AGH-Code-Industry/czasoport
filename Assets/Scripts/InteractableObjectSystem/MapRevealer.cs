using InteractableObjectSystem.Objects;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRevealer : MonoBehaviour {
    [SerializeField] private GameObject _objectToReveal;

    private LockedDoor _lockedDoor;
    void Start() {
        _lockedDoor = GetComponent<LockedDoor>();
        _lockedDoor.doorsOpened += (object sender, EventArgs e) => RevealObject();
        _lockedDoor.doorsClosed += (object sender, EventArgs e) => HideObject();
    }


    private void RevealObject() {
        _objectToReveal.SetActive(false);
    }

    private void HideObject() {
        _objectToReveal.SetActive(true);
    }
}