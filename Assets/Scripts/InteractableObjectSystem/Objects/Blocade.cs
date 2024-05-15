using System.Collections;
using System.Collections.Generic;
using InteractableObjectSystem;
using InventorySystem;
using Items;
using UnityEngine;

public class Blocade : MonoBehaviour {
    [SerializeField] private List<ItemSO> interactedWith;
    [SerializeField] private InteractableObject npcToTalk;
    [SerializeField] private bool opened = false;
    private Collider2D _collider;
    private void Awake() {
        _collider = GetComponent<Collider2D>();
        _collider.isTrigger = opened;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (opened) Destroy(this);
        if (collision.gameObject.CompareTag("Player")) {
            bool canGo = false;
            Inventory inventory = collision.gameObject.GetComponent<Inventory>();
            foreach (var i in interactedWith) {
                if (inventory.ContainsItem(i)) {
                    canGo = true;
                    break;
                }
            }

            if (canGo) {
                opened = true;
                _collider.isTrigger = true;
            }
            else {
                npcToTalk.InteractionHand();
            }
        }
    }
}