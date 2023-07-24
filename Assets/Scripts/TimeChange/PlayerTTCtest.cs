using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTTCtest : MonoBehaviour
{
    
    private Transform _transform;
    Vector2 movement;
    void Start()
    {
        _transform = GetComponent<Transform>();
    }

    void Update()
    {
        movement = InputSystem.CInput.NavigationAxis;
        _transform.Translate(new Vector2(movement.x,movement.y) * Time.deltaTime * 5);
    }
}
