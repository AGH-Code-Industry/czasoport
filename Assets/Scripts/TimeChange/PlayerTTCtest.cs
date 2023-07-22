using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTTCtest : MonoBehaviour
{
    private List<CheckCollider> boxes;
    private Transform _transform;
    [SerializeField] private string checkBoxTag = "CheckCollision";
    Vector2 movement;
    void Start()
    {
        _transform = GetComponent<Transform>();
        boxes = new List<CheckCollider>();
        foreach(Transform child in _transform){
            if(child.CompareTag(checkBoxTag))boxes.Add(child.GetComponent<CheckCollider>());
        }
    }

    void Update()
    {
        movement = InputSystem.CInput.NavigationAxis;
        _transform.position += new Vector3(movement.x,movement.y,0) * Time.deltaTime * 5;
    }

    public bool CanChangeTime(int id){
        foreach(CheckCollider box in boxes){
            if(box.GetId() != id) continue;
            if(box.IsTouching()) return false;
            else return true;
        }
        return true;
    }
}
