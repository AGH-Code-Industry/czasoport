using DataPersistence.DataTypes;
using DataPersistence;
using Dialogues;
using Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class ObjectRemover : MonoBehaviour
{
    [field: SerializeField] public SerializableGuid ID { get; set; }
    [SerializeField] private List<GameObject> objectsToDisable;
    private bool _collid = true;
    [SerializeField] private ItemSO exchangingItem;
    [SerializeField] private TypeSelector exchangingWithNPC = TypeSelector.YES;

    private void Start() {
        FindAnyObjectByType<DialogueManager>()._choicesProcessor.onEchange += (object sender, OnEchangeEventArgs e)
            => CheckObject(e.itemSO, e.itemExchangedWithNPC);
    }

    private void CheckObject(ItemSO exchangingItem, bool b) {
        if (exchangingItem != this.exchangingItem) return;
        if (exchangingWithNPC == TypeSelector.YES && !b) return;
        if (exchangingWithNPC == TypeSelector.NO && b) return;

        Collid(false);
    }

    private void Collid(bool collid) {
        if (collid) _collid = true;
        else _collid = false;

        foreach (GameObject go in objectsToDisable) {
            go.SetActive(_collid);
        }
    }

    public bool SceneObject { get; } = true;

    public void LoadPersistentData(GameData gameData) {
        if (!gameData.ContainsObjectData(ID))
            return;
        var data = gameData.GetObjectData<InteractableData>(ID);
        Collid(data.data.state == 1);
    }

    public void SavePersistentData(ref GameData gameData) {
        if (gameData.ContainsObjectData(ID)) {
            var data = gameData.GetObjectData<InteractableData>(ID);
            data.data.state = _collid ? 1 : 0;
            data.SerializeInheritance();
            gameData.SetObjectData(data);
        }
        else {
            var data = new InteractableData {
                data = new InteractableData.InteractableSubData {
                    state = _collid ? 1 : 0
                },
                id = ID
            };
            data.SerializeInheritance();
            gameData.SetObjectData(data);
        }
    }
}
