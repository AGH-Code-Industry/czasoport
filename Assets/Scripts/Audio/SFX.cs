using System;
using InventorySystem;
using InventorySystem.EventArguments;
using UnityEngine;

namespace AudioSystem {
    public class SFX : MonoBehaviour {
        public AudioClip itemPickedSound;
        public AudioClip itemRemovedSound;

        private AudioSource _audioSource;

        void Start() {
            BindSoundsWithEvents();
            _audioSource = Camera.main.GetComponent<AudioSource>();
        }

        private void BindSoundsWithEvents() {
            Inventory.Instance.ItemInserted += CreateSoundPlayingEventHandler<ItemInsertedEventArgs>(itemPickedSound);
            Inventory.Instance.ItemRemoved += CreateSoundPlayingEventHandler<ItemRemovedEventArgs>(itemRemovedSound);
        }

        private EventHandler<T> CreateSoundPlayingEventHandler<T>(AudioClip clip) {
            return (sender, args) => {
                PlaySFX(clip);
            };
        }

        private void PlaySFX(AudioClip clip) {
            _audioSource.clip = clip;
            _audioSource.Play();
        }
    }
}
