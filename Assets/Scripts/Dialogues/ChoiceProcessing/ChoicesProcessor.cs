using CoinPackage.Debugging;
using Dialogues.ChoiceProcessing;
using Ink.Runtime;
using InventorySystem;
using Items;
using UnityEngine;

namespace Dialogues {
    public class ChoicesProcessor {
        private GameObject _choicesPanel;
        private GameObject _choicesPrefab;
        
        public ChoicesProcessor(GameObject choicesPanel, GameObject choicesPrefab) {
            _choicesPanel = choicesPanel;
            _choicesPrefab = choicesPrefab;
        }

        public bool ProcessChoices(Story story) {
            foreach (Transform child in _choicesPanel.transform) {
                UnityEngine.Object.Destroy(child.gameObject);
            }

            if(story.currentChoices.Count < 1) {
                return false;
            }

            var choices = story.currentChoices;
            foreach (var choice in choices) {
                CreateChoiceButton(story, choice,
                    choice.tags != null ? ChoicesTagProcessor.ProcessTagsForChoice(choice.tags.ToArray()) : new ChoiceContext());
            }

            return true;
        }
        
        private void CreateChoiceButton(Story story, Choice choice, ChoiceContext choiceContext) {
            var choiceObject = UnityEngine.Object.Instantiate(_choicesPrefab, _choicesPanel.transform);
            var choiceButton = choiceObject.GetComponent<ChoiceButton>();
            choiceButton.button.onClick.AddListener(() => {
                story.ChooseChoiceIndex(choice.index);
                DialogueManager.I.ContinueDialogue();
                
                // Item should be always available to remove, since this option is only available if the item is in the inventory
                // But we can TODO add check here later
                if (choiceContext.RequiresItem) {
                    Inventory.Instance.RemoveItem(choiceContext.RequiredItem);
                }
                
                // TODO: Add what will happen when the choice is actually chosen
                // Do we always want to instantiate new item? Maybe take it from the pool?
                if (choiceContext.GetsItem) {
                    Inventory.Instance.InsertItem(
                        UnityEngine.Object.Instantiate(choiceContext.GetItem.prefab).GetComponent<Item>()
                        );
                }
            });
            choiceButton.SetText(choice.text);
            if (choiceContext.RequiresItem) {
                choiceButton.SetIcon(choiceContext.RequiredItem.image);
                if(!Inventory.Instance.ContainsItem(choiceContext.RequiredItem)) {
                    choiceButton.button.interactable = false;
                }
            }
            
            if (choiceContext.GetsItem) {
                choiceButton.SetIcon2(choiceContext.GetItem.image);
            }
        }
    }
}