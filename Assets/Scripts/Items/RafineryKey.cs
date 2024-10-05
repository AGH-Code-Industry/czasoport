using System;
using System.Collections;
using System.Collections.Generic;
using Items;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class RafineryKey : MonoBehaviour {
    private Item _item;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private Light2D _light2D;
    [SerializeField] private ItemSO _goodKeySO;

    private void Awake() {
        _item = GetComponent<Item>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _item.CanPickup = false;
    }

    public void SetGoodItem() {
        _item.CanPickup = true;
        _item.SetNewItemSO(_goodKeySO);
        _light2D.color = Color.red;
        _spriteRenderer.color = Color.red;
        Destroy(GetComponent<RafineryKeyObject>());
    }

}
