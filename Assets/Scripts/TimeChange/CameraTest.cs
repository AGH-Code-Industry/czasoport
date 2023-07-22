using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTest : MonoBehaviour
{
    [SerializeField] private Transform _player;
    private Transform _transform;
    void Start(){
        _transform = GetComponent<Transform>();
    }

    void Update(){
        _transform.position = _player.position + new Vector3(0,0,-10);
    }
}
