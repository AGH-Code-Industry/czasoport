using Dialogues;
using Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class ChangeAnimOnChange : MonoBehaviour
{
    [SerializeField] private RuntimeAnimatorController animatorController;
    [SerializeField] private ItemSO exchangingItem;

    private void Start() {
        FindAnyObjectByType<DialogueManager>()._choicesProcessor.onEchange += (object sender, OnEchangeEventArgs e)
            => CheckObject(e.itemSO);
    }

    private void CheckObject(ItemSO exchangingItem) {
        if (exchangingItem == this.exchangingItem) {
            this.GetComponent<Animator>().runtimeAnimatorController = animatorController;
        }
    }

}
