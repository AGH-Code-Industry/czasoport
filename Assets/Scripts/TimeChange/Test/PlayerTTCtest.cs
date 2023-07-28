using CustomInput;
using UnityEngine;

namespace TimeChange.Test
{
    public class PlayerTTCtest : MonoBehaviour
    {
        private Transform _transform;
        private Vector2 _movement;

        private void Start() {
            _transform = GetComponent<Transform>();
        }

        private void Update() {
            _movement = CInput.NavigationAxis;
            _transform.Translate(Time.deltaTime * 5 * new Vector2(_movement.x, _movement.y));
        }
    }
}
