using System;
using Items;

namespace InventorySystem.EventArguments {
    public class ItemInsertedEventArgs : EventArgs {
        // Slot to which item was inserted
        public int Slot;
        // Inserted item
        public Item Item;
    }
}