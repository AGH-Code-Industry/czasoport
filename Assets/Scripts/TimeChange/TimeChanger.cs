using CoinPackage.Debugging;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class TimeChanger : MonoBehaviour{

    private Transform _transform;
    private List<CheckCollider> boxes;
    [SerializeField]private Vector3 TimeJump;
    private InputActions InputActions;
    private Transform PlayerTransform;

    [SerializeField]private Animator animator;
    private delegate void changeTime();
    private changeTime TCT;
    [SerializeField]private float timeToChange = 0.3f;
    private float counterToChange;
    private TimeMachine new_id;
    private int change;
    

    public enum TimeMachine : int{
        Past = 0,
        Present = 1,
        Future = 2
    }

    public TimeMachine actualTime = TimeMachine.Present;

    private void Start(){
        boxes = new List<CheckCollider>();
        InputActions = InputSystem.CInput.InputActions;
        _transform = GetComponent<Transform>();

        for(int i=-2;i<=2;i++){
            if(i==0){
                boxes.Add(null);
                continue;
            }
            GameObject objectToSpawn = new GameObject("ColliderToSpawn");
            objectToSpawn.transform.parent = this.gameObject.transform;
            objectToSpawn.AddComponent<BoxCollider2D>();
            
            objectToSpawn.transform.position = i * TimeJump + _transform.position;
            boxes.Add(objectToSpawn.AddComponent<CheckCollider>());
        }

        InputActions.Teleport.TeleportBack.performed += TimeBack;
        InputActions.Teleport.TeleportForward.performed += TimeForward;
        counterToChange = timeToChange;
    }

    private void Update() {
        if(TCT != null)TCT();
    }

    private void OnDisable(){
        InputActions.Teleport.TeleportBack.performed -= TimeBack;
        InputActions.Teleport.TeleportForward.performed -= TimeForward;
    }

    private void TimeBack(InputAction.CallbackContext ctx){
        change = -1;
        TCT += TryChange;
        InputActions.Teleport.TeleportBack.performed -= TimeBack;
        InputActions.Teleport.TeleportForward.performed -= TimeForward;
    }

    private void TimeForward(InputAction.CallbackContext ctx){
        change = 1;
        TCT += TryChange;
        InputActions.Teleport.TeleportBack.performed -= TimeBack;
        InputActions.Teleport.TeleportForward.performed -= TimeForward;
    }

    private void TryChange(){
        if(actualTime == 0 && change == -1) change = 2;
        new_id = (TimeMachine)(((int)actualTime+change)%3);
        //CDebug.Log(new_id,Colorize.Magenta);
        change = 0;
        if(CanChangeTime(new_id-actualTime)){
            animator.SetTrigger("Start");
            TCT += ChangeTime;
        }
        else{
            InputActions.Teleport.TeleportBack.performed += TimeBack;
            InputActions.Teleport.TeleportForward.performed += TimeForward;
        }
        TCT -= TryChange;
    }

    private void ChangeTime(){
        counterToChange -= Time.deltaTime;
        if(counterToChange < 0f){
            _transform.Translate(TimeJump * (int)(new_id-actualTime));
            actualTime = new_id;
            animator.SetTrigger("End");
            
            counterToChange = timeToChange;
            
            InputActions.Teleport.TeleportBack.performed += TimeBack;
            InputActions.Teleport.TeleportForward.performed += TimeForward;
            TCT -= ChangeTime;
        }
    }

    private bool CanChangeTime(int when){
        return boxes[when+2].isNotTouching();
    }
}
