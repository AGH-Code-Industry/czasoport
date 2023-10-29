using System;
using InventorySystem;
using InventorySystem.EventArguments;
using UnityEngine;

namespace AudioSystem {
    public abstract class SFX : MonoBehaviour {

        private AudioSource _audioSource;

        void Start() {
            BindSoundsWithEvents();
            _audioSource = Camera.main.GetComponent<AudioSource>();
        }

        protected abstract void BindSoundsWithEvents();

        protected EventHandler<T> CreateSoundPlayingEventHandler<T>(AudioClip clip) {
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
