using InventorySystem;
using InventorySystem.EventArguments;
using LevelTimeChange.TimeChange;
using System;
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

        public void ChangeVolume(float value) {
            Debug.Log(value);
            audioMixer.SetFloat("SFXVolume", value);
            if (value <= -24f) audioMixer.SetFloat("SFXVolume", -80f);
            
        }
    }
}