using Items;

namespace InventorySystem.EventArguments {
    public class ItemStateChangedEventArgs {
        // Slot that hold the item
        public int Slot;
        // Item which state has been changed
        public Item Item;
    }
}