using InventorySystem;
using InventorySystem.EventArguments;
using LevelTimeChange.TimeChange;
using UnityEngine;
using static LevelTimeChange.TimeChange.TimeChanger;

namespace AudioSystem {
    public class GameSFX : SFX {
        public AudioClip itemPickedSound;
        public AudioClip itemRemovedSound;
        public AudioClip timeTravelSound;

        protected override void BindSoundsWithEvents() {
            Inventory.Instance.ItemInserted += CreateSoundPlayingEventHandler<ItemInsertedEventArgs>(itemPickedSound);
            Inventory.Instance.ItemRemoved += CreateSoundPlayingEventHandler<ItemRemovedEventArgs>(itemRemovedSound);
            TimeChanger.Instance.OnTimeChange += CreateSoundPlayingEventHandler<OnTimeChangeEventArgs>(timeTravelSound);
        }
    }
}