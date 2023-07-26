using UnityEngine;

namespace TimeChange.Test
{
    public class CameraTest : MonoBehaviour
    {
        [SerializeField] private Transform player;
        private Transform _transform; 
        
        private void Start() {
            _transform = GetComponent<Transform>();
        }

        private void Update() {
            _transform.position = player.position + new Vector3(0,0,-10);
        }
    }
}
