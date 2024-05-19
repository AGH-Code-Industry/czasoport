using Items;
using JetBrains.Annotations;
using System.Collections.Generic;

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
        [CanBeNull] private List<ItemSO> _getItem = new List<ItemSO>();
        public List<ItemSO> GetItem {
            get {
                if (!GetsItem) {
                    return null;
                }
                return _getItem;
            }
        }

        public void AddItem(ItemSO itemToAdd) {
            _getItem.Add(itemToAdd);
            GetsItem = true;
        }
    }
}