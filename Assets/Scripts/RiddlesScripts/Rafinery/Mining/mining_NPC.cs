using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventorySystem.EventArguments;
using InventorySystem;
using Items;
using NPC;

public class mining_NPC : MonoBehaviour
{
    // Start is called before the first frame update
    private DialogueNPC _dialogueScript;
    [SerializeField] private TextAsset[] _dialogue = new TextAsset[3];
    void Start()
    {
        _dialogueScript = GetComponent<DialogueNPC>();
        Inventory.Instance.ItemInserted += checkIfGem;
        Inventory.Instance.ItemRemoved += checkIfGemRemoved;
    }
    private void checkIfGem(object sender, ItemInsertedEventArgs args) {
        if(args.Item.ItemSO.itemName == "Rafinery Gem") {
            _dialogueScript.SetDialogue(_dialogue[1]);
        }
        if(args.Item.ItemSO.itemName == "Rafinery Gem Opened") {
            _dialogueScript.SetDialogue(_dialogue[2]);
        }
        if (args.Item.ItemSO.itemName == "Markus Cpt Part") {
            _dialogueScript.SetDialogue(_dialogue[3]);
        }

    }
    private void checkIfGemRemoved(object sender, ItemRemovedEventArgs args) {
        if (args.Item.ItemSO.itemName == "Rafinery Gem") {
            _dialogueScript.SetDialogue(_dialogue[0]);
        }
        if(args.Item.ItemSO.itemName == "Rafinery Gem Opened") {
            _dialogueScript.SetDialogue(_dialogue[1]);
        }

    }
}
