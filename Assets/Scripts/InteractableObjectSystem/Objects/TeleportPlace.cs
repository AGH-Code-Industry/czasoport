using CoinPackage.Debugging;
using UnityEngine;

public class TeleportPlace : MonoBehaviour {
    [SerializeField] private TeleportPlace destinationPlace;
    private bool active = true;
    public Vector3 StartTeleportation() {
        active = false;
        return transform.position;
    }
    private void OnTriggerEnter2D(Collider2D col) {
        //CDebug.Log("TELEPORT?");
        if (col.CompareTag("Player") & active) {
            //CDebug.Log("TELEPORT!");
            col.gameObject.transform.position = destinationPlace.StartTeleportation();
        }
    }

    private void OnTriggerExit2D(Collider2D col) {
        if (col.CompareTag("Player")) {
            active = true;
        }
    }
}