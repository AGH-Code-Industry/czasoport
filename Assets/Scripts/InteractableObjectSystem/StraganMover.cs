using Dialogues;
using Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraganMover : MonoBehaviour {
    [SerializeField] private ItemSO _exchangingItem;
    [SerializeField] private GameObject _straganToMoveInFuture;

    private void Start() {
        FindAnyObjectByType<DialogueManager>()._choicesProcessor.onEchange += (object sender, OnEchangeEventArgs e)
            => CheckObject(e.itemSO);
    }

    private void CheckObject(ItemSO exchangingItem) {
        if (exchangingItem == _exchangingItem) {
            _straganToMoveInFuture.transform.position = new Vector3(_straganToMoveInFuture.transform.position.x + 16f,
                _straganToMoveInFuture.transform.position.y,
                _straganToMoveInFuture.transform.position.z);
        }
    }

}