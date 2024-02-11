using InteractableObjectSystem;
using InventorySystem;
using Items;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Photocopier : InteractableObject
{
    [SerializeField] private Item _resultItem;
    [SerializeField] private ItemSO _firstItem;
    [SerializeField] private ItemSO _secondItem;
    private int _stage = 0;
    private bool _itemTaken = false;

    public override void InteractionHand() {
        if (_stage != 2 || _itemTaken) return;
        Inventory.Instance.InsertItem(_resultItem);
        _itemTaken = true;
    }

    public override bool InteractionItem(Item item) {
        Debug.Log("interacting");
        if (_stage == 2) return false;
        if ((_stage == 0 && _firstItem == item.ItemSO) || (_stage == 1 && _secondItem == item.ItemSO)) {
            CopyDocument();
            return true;
        }
        return false;
    }   

    private void CopyDocument() {
        _stage += 1;
        Debug.Log("printing");
    }
}
