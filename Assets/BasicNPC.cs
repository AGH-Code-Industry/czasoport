using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicNPC : MonoBehaviour {
    private Material _material;
    private SpriteRenderer _sprite;

    void Start() {
        _sprite = GetComponent<SpriteRenderer>();
        _material = _sprite.material;
        _material.SetTexture("_texture", _sprite.sprite.texture);
    }
}