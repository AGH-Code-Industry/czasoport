using System;
using UnityEngine;
using UnityEngine.Audio;

namespace AudioSystem {
    /// <summary>
    /// Base class for associating events with sound effects.
    /// </summary>
    public abstract class SFX : MonoBehaviour {

        public AudioSource audioSource;
        public AudioMixer audioMixer;

        void Start() {
            ApplyAudioSettings();
            BindSoundsWithEvents();
        }

        private void ApplyAudioSettings() {
            float volumeInDb = PlayerPrefs.GetFloat("SFXVolume", 1.0f);
            audioMixer.SetFloat("SFXVolume", volumeInDb);
        }

        protected abstract void BindSoundsWithEvents();

        protected EventHandler<T> CreateSoundPlayingEventHandler<T>(AudioClip clip) {
            return (sender, args) => {
                PlaySFX(clip);
            };
        }

        private void PlaySFX(AudioClip clip) {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}
