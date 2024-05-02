using InteractableObjectSystem;
using InventorySystem;
using Items;
using System.Collections;
using System.Collections.Generic;
using DataPersistence;
using DataPersistence.DataTypes;
#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#endif
using UnityEngine;

public class Photocopier : PersistentInteractableObject {
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

    public override void LoadPersistentData(GameData gameData) {
        if (!gameData.ContainsObjectData(ID))
            return;

        var photocopierData = gameData.GetObjectData<PhotocopierData>(ID);
        _stage = photocopierData.data.stage;
        _itemTaken = photocopierData.data.itemTaken;
        VisualizeStage();
    }

    public override void SavePersistentData(ref GameData gameData) {
        if (gameData.ContainsObjectData(ID)) {
            var photocopierData = gameData.GetObjectData<PhotocopierData>(ID);
            photocopierData.data.stage = _stage;
            photocopierData.data.itemTaken = _itemTaken;
            photocopierData.SerializeInheritance();
            gameData.SetObjectData(photocopierData);
        }
        else {
            var photocopierData = new PhotocopierData {
                data = new PhotocopierData.PhotocopierSubData() {
                    stage = _stage,
                    itemTaken = _itemTaken
                },
                id = ID
            };
            photocopierData.SerializeInheritance();
            gameData.SetObjectData(photocopierData);
        }
    }

    private void CopyDocument() {
        _stage += 1;

        VisualizeStage();

        LeanTween.moveY(_go, _go.transform.position.y + _go.transform.localScale.y / 4, 1f).setEase(easeType);
    }

    private void VisualizeStage() {
        if (_stage == 1) {
            _go = Instantiate(_resultItem, transform.position, Quaternion.identity).gameObject;
            _go.layer = 0;
            _go.GetComponent<Item>().enabled = false;
            _go.GetComponent<CircleCollider2D>().enabled = false;
            GetComponent<PhotocotierUI>().setCounter(_stage.ToString() + "/2");
        }
    }
}