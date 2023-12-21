using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WithinLevelPortal : MonoBehaviour
{
    private Transform _destinationPoint;

    private void Start() {
        _destinationPoint = transform.GetChild(0).transform;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            collision.transform.position = _destinationPoint.position;
        }
    }
}
