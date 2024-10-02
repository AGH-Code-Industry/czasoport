using System.Collections;
using System.Collections.Generic;
using DataPersistence;
using DataPersistence.DataTypes;
using Interactions;
using Items;
using UnityEngine;
using UnityEngine.Events;

namespace InteractableObjectSystem.Objects {
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(HighlightInteraction))]
    public class Pedesta2 : PersistentInteractableObject {
        [SerializeField] private ItemSO goodCrystal;
        [SerializeField] private Sprite onGoodCrystal;

        private bool _done = false;
        private SpriteRenderer _spriteRenderer = null;

        public UnityEvent OnGoodRock;

        private void Awake() {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public override bool InteractionItem(Item item) {
            if (item.ItemSO == goodCrystal) {
                doIt();
                return true;
            }

            return false;
        }

        private void doIt() {
            if (!_spriteRenderer) _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.sprite = onGoodCrystal;
            _done = true;
            OnGoodRock?.Invoke();
        }

        public override void LoadPersistentData(GameData gameData) {
            if (!gameData.ContainsObjectData(ID))
                return;

            var doorData = gameData.GetObjectData<InteractableData>(ID);
            if (doorData.data.state == 1) Invoke("doIt",1f);
        }

        public override void SavePersistentData(ref GameData gameData) {
            if (gameData.ContainsObjectData(ID)) {
                var doorData = gameData.GetObjectData<InteractableData>(ID);
                doorData.data.state = _done ? 1 : 0;
                doorData.SerializeInheritance();
                gameData.SetObjectData(doorData);
            }
            else {
                var doorData = new InteractableData {
                    data = new InteractableData.InteractableSubData {
                        state = _done ? 1 : 0
                    },
                    id = ID
                };
                doorData.SerializeInheritance();
                gameData.SetObjectData(doorData);
            }
        }
    }
}