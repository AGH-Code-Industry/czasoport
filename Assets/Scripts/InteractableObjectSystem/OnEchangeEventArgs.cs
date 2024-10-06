using Items;
using System;

public class OnEchangeEventArgs : EventArgs {
    public ItemSO itemSO;
    public bool itemExchangedWithNPC = true;
}