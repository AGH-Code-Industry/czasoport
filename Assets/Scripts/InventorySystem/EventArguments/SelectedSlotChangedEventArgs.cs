using System;

namespace InventorySystem.EventArguments {
    public class SelectedSlotChangedEventArgs : EventArgs {
        // New selected slot
        public int Slot;
    }
}