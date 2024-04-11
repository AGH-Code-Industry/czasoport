using System.Collections;
using System.Collections.Generic;
using Interactions;
using Items;
using Notifications;
using UnityEngine;

namespace InteractableObjectSystem.Objects {
    [RequireComponent(typeof(BoxCollider2D))]
    public class ChangeInto : InteractableObject {
        /// <summary>
        /// Id interactedWith odpowiada Id GameObject w który się zmieni
        /// </summary>
        [SerializeField] private List<ItemSO> interactedWith;
        [SerializeField] private List<GameObject> result;

        public override void InteractionHand() {
            if (interactedWith.Count == 0) {
                NotificationManager.Instance.RaiseNotification(new Notification(definition.successfulHandInterNotification.message, definition.successfulHandInterNotification.displayTime));
                Change();
            }
            else {
                NotificationManager.Instance.RaiseNotification(new Notification(
                    definition.failedHandInterNotification.message,
                    definition.failedHandInterNotification.displayTime));
            }

        }

        public override bool InteractionItem(Item item) {
            if (interactedWith.Contains(item.ItemSO)) {
                NotificationManager.Instance.RaiseNotification(new Notification(definition.successfulItemInterNotification.message, definition.successfulItemInterNotification.displayTime));
                Change(interactedWith.IndexOf(item.ItemSO));
                return true;
            }
            else {
                NotificationManager.Instance.RaiseNotification(new Notification(definition.failedItemInterNotification.message, definition.failedItemInterNotification.displayTime));
            }
            return false;
        }

        private void Change(int id = 0) {
            Instantiate(result[id], transform.position, Quaternion.identity);
            StartCoroutine(Hide());
        }

        private IEnumerator Hide() {
            transform.position = transform.position + new Vector3(100, 100, 100);
            yield return new WaitForSeconds(1);
            Destroy(gameObject);
        }
    }
}