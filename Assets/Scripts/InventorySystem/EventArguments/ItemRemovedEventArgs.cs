using System;
using Items;

namespace InventorySystem.EventArguments {
    public class ItemRemovedEventArgs : EventArgs {
        // Slot from which item was removed
        public int Slot;
        // Removed item
        public Item Item;
    }
}