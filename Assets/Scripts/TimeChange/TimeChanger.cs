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

    public enum Time : int{
        Past = 0,
        Present = 1,
        Future = 2
    }

    public Time actualTime = Time.Present;

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
    }

    private void OnDisable(){
        InputActions.Teleport.TeleportBack.performed -= TimeBack;
        InputActions.Teleport.TeleportForward.performed -= TimeForward;
    }

    private void TimeBack(InputAction.CallbackContext ctx){ChangeTime(-1);}
    private void TimeForward(InputAction.CallbackContext ctx){ChangeTime(1);}

    private void ChangeTime(int change){
        if(actualTime == 0 && change == -1) change = 2;
        Time new_id = (Time)(((int)actualTime+change)%3);
        //CDebug.Log(new_id,Colorize.Magenta);
        if(!CanChangeTime(new_id-actualTime)) return;
        _transform.Translate(TimeJump * (int)(new_id-actualTime));
        actualTime = new_id;
    }

    private bool CanChangeTime(int when){
        return boxes[when+2].isNotTouching();
    }
}
