using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWorld : MonoBehaviour
{
    [SerializeField]
    public Transform lookAt;
    [SerializeField]
    public Vector3 offset;

    private Camera _cam;

    private void Start() {
        _cam = Camera.main;
    }

    private void Update() {
        Vector3 pos = _cam.WorldToScreenPoint(lookAt.position + offset);

        if (transform.position != pos) {
            transform.position = pos;
        }
    }
}
