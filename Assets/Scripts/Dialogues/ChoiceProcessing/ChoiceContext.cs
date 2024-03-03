using Items;
using JetBrains.Annotations;

namespace Dialogues.ChoiceProcessing {
    public class ChoiceContext {
        public bool RequiresItem { get; private set; }
        [CanBeNull] private ItemSO _requiredItem;
        public ItemSO RequiredItem {
            get {
                if (!RequiresItem) {
                    return null;
                }
                return _requiredItem;
            }
            set {
                _requiredItem = value;
                RequiresItem = true;
            }
        }
        
        public bool GetsItem { get; private set; }
        [CanBeNull] private ItemSO _getItem;
        public ItemSO GetItem {
            get {
                if (!GetsItem) {
                    return null;
                }
                return _getItem;
            }
            set {
                _getItem = value;
                GetsItem = true;
            }
        }
    }
}