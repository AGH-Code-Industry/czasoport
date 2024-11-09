using Interactions;
using InventorySystem;
using InventorySystem.EventArguments;
using Items;
using NPC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueDisabler : MonoBehaviour
{
    [SerializeField] private ItemSO _itemToReceive;
    private bool _canDisableDialogue = false;
    [SerializeField] private Material _startingMaterial;

    private void Start() {
        Inventory.Instance.ItemInserted += (object sender, ItemInsertedEventArgs e) => CheckItem(e);
    }

    private void CheckItem(ItemInsertedEventArgs e) {
        if (e.Item.GetItemSO() != _itemToReceive) {
            return;
        }

        _canDisableDialogue = true;
        DisableDialogue();
    }

    public void DisableDialogue() {
        if (!_canDisableDialogue) {
            return;
        }

        Destroy(GetComponent<DialogueNPC>());
        Destroy(GetComponent<HighlightInteraction>());

        //making the NPC not white
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        sprite.material = _startingMaterial;
        sprite.material.SetTexture("_texture", sprite.sprite.texture);
    }
}
