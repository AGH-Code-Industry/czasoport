using Dialogues;
using Items;
using LevelTimeChange.LevelsLoader;
using NPC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarcusEnding : MonoBehaviour
{
    [SerializeField] private ItemSO firstPart1;
    [SerializeField] private ItemSO firstPart2;
    [SerializeField] private ItemSO firstPart3;

    private bool part1Provided = false;
    private bool part2Provided = false;
    private bool part3Provided = false;

    private void Start() {
        FindAnyObjectByType<DialogueManager>()._choicesProcessor.onEchange += (object sender, OnEchangeEventArgs e)
            => CheckObject(e.itemSO);
    }

    private void CheckObject(ItemSO exchangingItem) {
        if (exchangingItem != firstPart1 &&
            exchangingItem != firstPart2 &&
            exchangingItem != firstPart3) { return; }

        if (exchangingItem == firstPart1) {
            part1Provided = true;
        } else if (exchangingItem == firstPart2) {
            part2Provided = true;
        } else if (exchangingItem == firstPart3) {
            part3Provided = true;
        }
        CheckForEndGame();
    }

    private void CheckForEndGame() {
        if (part1Provided && part2Provided && part3Provided) {
            LevelsManager.Instance.EndGame();
        }
    }
}
