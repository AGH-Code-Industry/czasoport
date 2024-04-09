using System;
using InventorySystem;
using InventorySystem.EventArguments;
using UnityEngine;

public class Audio : MonoBehaviour {
    public AudioClip itemPickedSound;
    public AudioClip itemRemovedSound;

    void Start() {
        BindSoundsWithEvents();
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
        var audioSource = Camera.main.GetComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.Play();
    }
}