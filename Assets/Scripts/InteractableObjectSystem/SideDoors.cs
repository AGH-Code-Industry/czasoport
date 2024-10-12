using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideDoors : MonoBehaviour {
    [SerializeField] private GameObject doors;
    [SerializeField] private Collider2D _collider;
    public void Open() {
        Vector3 currentPos = doors.transform.localPosition;
        Vector3 targetPos = new Vector3(currentPos.x + 1f, currentPos.y + 0.46f, currentPos.z);
        LeanTween.moveLocal(doors, targetPos, 1f).setEase(LeanTweenType.easeInOutSine);
        _collider.enabled = false;
    }
}