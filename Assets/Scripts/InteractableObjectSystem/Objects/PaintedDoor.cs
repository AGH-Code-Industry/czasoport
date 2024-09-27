using System;
using System.Collections;
using System.Collections.Generic;
using DataPersistence;
using DataPersistence.DataTypes;
using InventorySystem;
using Items;
using UnityEngine;

namespace InteractableObjectSystem.Objects {
    public class PaintedDoor : PersistentInteractableObject {
        public enum DoorColor {
            Gray = 0,
            Blue = 1,
            Yellow = 2,
            Green = 3
        }


        [SerializeField] private ItemSO bluePaint;
        [SerializeField] private ItemSO yellowPaint;

        private SpriteRenderer _spriteRenderer;

        private DoorColor _state = DoorColor.Gray;

        private void Awake() {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public override void InteractionHand() {
            NotificationManager.Instance.RaiseNotification(definition.failedHandInterNotification);
        }

        public override bool InteractionItem(Item item) {
            if (_state == DoorColor.Green) {
                return false;
            }

            if (bluePaint == item.ItemSO) {
                if (_state == DoorColor.Yellow) ChangeState(DoorColor.Green);
                else ChangeState(DoorColor.Blue);
                NotificationManager.Instance.RaiseNotification(definition.successfulItemInterNotification);
                return true;
            }
            if (yellowPaint == item.ItemSO) {
                if (_state == DoorColor.Blue) ChangeState(DoorColor.Green);
                else ChangeState(DoorColor.Yellow);
                NotificationManager.Instance.RaiseNotification(definition.successfulItemInterNotification);
                return true;
            }

            NotificationManager.Instance.RaiseNotification(definition.failedItemInterNotification);
            return false;
        }

        private void ChangeState(DoorColor newState) {
            _state = newState;
            switch (_state) {
                case DoorColor.Gray:
                    _spriteRenderer.color = new Color(1f, 1f, 1f);
                    break;
                case DoorColor.Blue:
                    _spriteRenderer.color = new Color(0f, 0f, 1f);
                    break;
                case DoorColor.Yellow:
                    _spriteRenderer.color = new Color(0.9f, 0.9f, 0.2f);
                    break;
                case DoorColor.Green:
                    _spriteRenderer.color = new Color(0f, 1f, 0f);
                    break;
            }
        }

        public override void LoadPersistentData(GameData gameData) {
            if (!gameData.ContainsObjectData(ID))
                return;

            var doorData = gameData.GetObjectData<InteractableData>(ID);
            switch (doorData.data.state) {
                case 0:
                    ChangeState(DoorColor.Gray);
                    break;
                case 1:
                    ChangeState(DoorColor.Blue);
                    break;
                case 2:
                    ChangeState(DoorColor.Yellow);
                    break;
                case 3:
                    ChangeState(DoorColor.Green);
                    break;
            }
        }

        public override void SavePersistentData(ref GameData gameData) {
            if (gameData.ContainsObjectData(ID)) {
                var doorData = gameData.GetObjectData<InteractableData>(ID);
                doorData.data.state = (int)_state;
                doorData.SerializeInheritance();
                gameData.SetObjectData(doorData);
            }
            else {
                var doorData = new InteractableData {
                    data = new InteractableData.InteractableSubData {
                        state = (int)_state
                    },
                    id = ID
                };
                doorData.SerializeInheritance();
                gameData.SetObjectData(doorData);
            }
        }
    }
}
