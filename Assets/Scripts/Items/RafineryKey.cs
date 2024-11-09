using System;
using System.Collections;
using System.Collections.Generic;
using Items;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class RafineryKey : MonoBehaviour {
    private Item _item;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private ItemSO _goodKeySO;

    private void Awake() {
        _item = GetComponent<Item>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.enabled = false;
        _item.transform.GetChild(0).gameObject.SetActive(false);
        _item.CanPickup = false;
    }

    public void SetGoodItem() {
        _spriteRenderer.enabled = true;
        _item.CanPickup = true;
        _item.transform.GetChild(0).gameObject.SetActive(true);
    }

}