using AudioSystem;
using InventorySystem;
using InventorySystem.EventArguments;
using UnityEngine;

public class GameSFX : SFX {
    public AudioClip itemPickedSound;
    public AudioClip itemRemovedSound;

    protected override void BindSoundsWithEvents() {
        Inventory.Instance.ItemInserted += CreateSoundPlayingEventHandler<ItemInsertedEventArgs>(itemPickedSound);
        Inventory.Instance.ItemRemoved += CreateSoundPlayingEventHandler<ItemRemovedEventArgs>(itemRemovedSound);
    }
}
