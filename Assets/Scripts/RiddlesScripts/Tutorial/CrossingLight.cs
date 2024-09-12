using System;
using System.Collections;
using System.Collections.Generic;
using CoinPackage.Debugging;
using DataPersistence;
using DataPersistence.DataTypes;
using InteractableObjectSystem;
using LevelTimeChange.TimeChange;
using NPC;
using PlayerScripts;
using Settings;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;

public class CrossingLight : MonoBehaviour, IPersistentObject {
    [field: SerializeField] public SerializableGuid ID { get; set; }
    public bool SceneObject { get; }

    public float lightChangeInterval = 5f;

    [SerializeField] private Animator lightAnimator;
    [SerializeField] private List<PathWalking> pathWalkings = new List<PathWalking>();
    [SerializeField] private BoxCollider2D collider;
    [SerializeField] private Transform returnPosition;
    [SerializeField] private CheckPlayerInArea areaEnd;

    private AudioSource _audioSource;

    private bool _playerInEndArea {
        get { return areaEnd.IsInArea; }
    }
    [SerializeField] private bool _blocked = false;

    private Animator _blackScreenAnimator;
    private float _blackScreenAnimatorTimer;

    private enum CrossingState {
        Opened,
        Closed
    }

    private CrossingState _crossingState;

    private void Start() {
        _blackScreenAnimator = TimeChanger.Instance.Animator;
        _blackScreenAnimatorTimer = DeveloperSettings.Instance.tpcSettings.timelineChangeAnimLength;
        _audioSource = GetComponent<AudioSource>();
        CloseCrossing();
        InvokeRepeating(nameof(ToggleLightsState), lightChangeInterval, lightChangeInterval);
    }

    private void Update() {
        if (!_blocked && _playerInEndArea) BlockWay();
    }

    public void BlockWay() {
        if (_blocked) return;
        _blocked = true;
        returnPosition = areaEnd.transform;
        CancelInvoke(nameof(ToggleLightsState));
        if (_crossingState == CrossingState.Opened) CloseCrossing();
    }

    private void ToggleLightsState() {
        switch (_crossingState) {
            case CrossingState.Opened:
                CloseCrossing();
                break;
            case CrossingState.Closed:
                OpenCrossing();
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        StartCoroutine(SetPlayerToStart());
    }

    private void OpenCrossing() {
        lightAnimator.SetTrigger("Open");
        lightAnimator.ResetTrigger("Close");
        collider.enabled = false;
        _crossingState = CrossingState.Opened;
        foreach (PathWalking pw in pathWalkings) {
            pw.StartWalk();
        }
        _audioSource.Stop();
    }

    private void CloseCrossing() {
        lightAnimator.SetTrigger("Close");
        lightAnimator.ResetTrigger("Open");
        collider.enabled = true;
        _crossingState = CrossingState.Closed;
        foreach (PathWalking pw in pathWalkings) {
            pw.StopWalk();
        }
        _audioSource.Play();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (_crossingState == CrossingState.Opened) return;
        if (other.CompareTag("NPC")) {
            PathWalking pw = other.GetComponent<PathWalking>();
            pw.StopWalk();
            if (pw != null) pathWalkings.Add(pw);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("NPC")) {
            PathWalking pw = other.GetComponent<PathWalking>();
            if (pw != null) pathWalkings.Remove(pw);
        }
    }

    private IEnumerator SetPlayerToStart() {
        _blackScreenAnimator.SetTrigger("Start");
        yield return new WaitForSeconds(_blackScreenAnimatorTimer / 2);
        Player.Instance.transform.position = returnPosition.position;
        yield return new WaitForSeconds(_blackScreenAnimatorTimer / 2);
        _blackScreenAnimator.SetTrigger("End");
    }

    public void LoadPersistentData(GameData gameData) {
        if (!gameData.ContainsObjectData(ID))
            return;

        var _data = gameData.GetObjectData<InteractableData>(ID);
        if (_data.data.state == 1) BlockWay();
    }

    public void SavePersistentData(ref GameData gameData) {
        if (gameData.ContainsObjectData(ID)) {
            var crossingData = gameData.GetObjectData<InteractableData>(ID);
            crossingData.data.state = _blocked ? 1 : 0;
            crossingData.SerializeInheritance();
            gameData.SetObjectData(crossingData);
        }
        else {
            var crossingData = new InteractableData {
                data = new InteractableData.InteractableSubData {
                    state = _blocked ? 1 : 0
                },
                id = ID
            };
            crossingData.SerializeInheritance();
            gameData.SetObjectData(crossingData);
        }
    }
}