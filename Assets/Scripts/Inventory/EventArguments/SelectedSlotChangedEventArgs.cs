using System;

namespace Inventory.EventArguments {
    public class SelectedSlotChangedEventArgs : EventArgs {
        // New selected slot
        public int Slot;
    }
}