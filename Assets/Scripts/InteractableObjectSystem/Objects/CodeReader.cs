using Interactions;
using UnityEngine;

namespace InteractableObjectSystem.Objects {
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(HighlightInteraction))]
    public class CodeReader : InteractableObject {

        public override void InteractionHand() {
            base.InteractionHand();
        }
    }
}