using InteractableObjectSystem;
using InventorySystem;
using Items;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#endif
using UnityEngine;

public class Photocopier : InteractableObject {
    [SerializeField] private Item _resultItem;
    [SerializeField] private ItemSO _firstItem;
    [SerializeField] private ItemSO _secondItem;
    [SerializeField] LeanTweenType easeType;
    private int _stage = 0;
    private bool _itemTaken = false;
    private GameObject _go;

    public override void InteractionHand() {
        if (_stage != 2 || _itemTaken) return;
        Inventory.Instance.InsertItem(_resultItem);
        _itemTaken = true;
        Destroy(_go);
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
        if (_stage == 0) {
            _go = Instantiate(_resultItem, this.transform.position, Quaternion.identity).gameObject;
            _go.layer = 0;
            _go.GetComponent<Item>().enabled = false;
            _go.GetComponent<CircleCollider2D>().enabled = false;
        }
        _stage += 1;
        LeanTween.moveY(_go, _go.transform.position.y + _go.transform.localScale.y / 4, 1f).setEase(easeType);
        GetComponent<PhotocotierUI>().setCounter(_stage.ToString() + "/2");
    }
}