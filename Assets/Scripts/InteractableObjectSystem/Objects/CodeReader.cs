using System;
using CustomInput;
using Interactions;
using Items;
using Notifications;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace InteractableObjectSystem.Objects {
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(HighlightInteraction))]
    public class CodeReader : InteractableObject {

        [SerializeField] private ItemSO codeItem;
        [SerializeField] private GameObject codeUI;
        [SerializeField] private ScreenUI screenUI;
        [SerializeField] private TextMeshProUGUI codeText;
        [SerializeField] private GameObject codeTextGO;

        private bool _haveCode;
        private int _code;
        private void Awake() {
            codeUI.SetActive(false);
            codeTextGO.SetActive(false);
            _code = Random.Range(1000, 10000);
            codeText.text = _code.ToString();
            screenUI.SetCode(_code);
        }

        public override void InteractionHand() {
            codeUI.SetActive(true);
            codeTextGO.SetActive(_haveCode);
            CInput.InputActions.Movement.Disable();
        }

        public override bool InteractionItem(Item item) {
            if (item.ItemSO == codeItem) {
                NotificationManager.Instance.RaiseNotification(new Notification("Code: " + _code, 5f));
                _haveCode = true;
                return true;
            }
            InteractionHand();
            return false;
        }
    }
}