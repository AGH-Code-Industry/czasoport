using CoinPackage.Debugging;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class TimeChanger : MonoBehaviour{
    [SerializeField]private Transform TimeSpace;
    [SerializeField]private PlayerTTCtest Player;
    [SerializeField]private int TimeJump = 100;
    private InputActions InputActions;
    private Transform PlayerTransform;
    private List<Transform> Space;
    private List<Transform> Times;

    public int ActualTimeId = 1;
    private int[] handRotate = {0,-45,-90};
    [SerializeField] private Transform Hand;

    private void Start(){
        InputActions = InputSystem.CInput.InputActions;
        FindTime();
        PlayerTransform = Player.GetComponent<Transform>();
        InputActions.Teleport.TeleportBack.performed += TimeBack;
        InputActions.Teleport.TeleportForward.performed += TimeForward;
    }

    private void OnDisable(){
        InputActions.Teleport.TeleportBack.performed -= TimeBack;
        InputActions.Teleport.TeleportForward.performed -= TimeForward;
    }

    private void TimeBack(InputAction.CallbackContext ctx){ChangeTime(-1);}
    private void TimeForward(InputAction.CallbackContext ctx){ChangeTime(1);}

    public void FindTime(){
        Times = new List<Transform>();
        Space = new List<Transform>();
        foreach(Transform child in TimeSpace){
            if(!child.CompareTag("Space")) continue;
            if(child.gameObject.activeSelf)Space.Add(child);
        }

        foreach(Transform s in Space){
            foreach(Transform child in s){
                if(!child.CompareTag("Time")) continue;
                Times.Add(child);
            }
        }
        /*
        foreach(Transform t in Times){
            CDebug.Log("Czas " + t.name,Colorize.Magenta);
        }*/
        
    }

    private void ChangeTime(int change){
        if(ActualTimeId == 0 && change == -1) change = 2;
        int new_id = (ActualTimeId+change)%3;
        //CDebug.Log(new_id,Colorize.Magenta);
        if(!Player.CanChangeTime(new_id-ActualTimeId)) return;
        PlayerTransform.position = new Vector2(PlayerTransform.position.x + TimeJump * (new_id-ActualTimeId),PlayerTransform.position.y + TimeJump * (new_id-ActualTimeId));
        Hand.rotation = Quaternion.Euler(0,0,handRotate[new_id]);
        ActualTimeId = new_id;
    }
}
