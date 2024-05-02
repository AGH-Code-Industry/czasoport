using System.Collections;
using System.Collections.Generic;
using DataPersistence;
using DataPersistence.DataTypes;
using Interactions;
using Items;
using NPC;
using UnityEngine;

namespace InteractableObjectSystem.Objects {
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(HighlightInteraction))]
    public class TapePlayer : PersistentInteractableObject {
        [SerializeField] private ItemSO _interactedWith;
        [SerializeField] private List<PathWalking> _NPCPathWalkings = new();

        private bool _playingMusic = false;

        public override void InteractionHand() {
            NotificationManager.Instance.RaiseNotification(definition.failedHandInterNotification);
        }

        public override bool InteractionItem(Item item) {
            if (item.ItemSO == _interactedWith) {
                PlayMusic();
                return true;
            }

            return false;
        }

        public override void LoadPersistentData(GameData gameData) {
            if (!gameData.ContainsObjectData(ID))
                return;

            var tapePlayerData = gameData.GetObjectData<TapePlayerData>(ID);
            _playingMusic = tapePlayerData.data.playingMusic;
            if (_playingMusic)
                PlayMusic();
        }

        public override void SavePersistentData(ref GameData gameData) {
            if (gameData.ContainsObjectData(ID)) {
                var tapePlayerData = gameData.GetObjectData<TapePlayerData>(ID);
                tapePlayerData.data.playingMusic = _playingMusic;
                tapePlayerData.SerializeInheritance();
                gameData.SetObjectData(tapePlayerData);
            }
            else {
                var tapePlayerData = new TapePlayerData();
                tapePlayerData.data = new TapePlayerData.TapePlayerSubData() {
                    playingMusic = _playingMusic
                };
                tapePlayerData.SerializeInheritance();
                gameData.SetObjectData(tapePlayerData);
            }
        }

        private void PlayMusic() {
            foreach (var pathWalking in _NPCPathWalkings) {
                pathWalking.StartWalk();
            }
        }
    }
}