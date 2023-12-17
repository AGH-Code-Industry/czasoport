using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class TransitionX : MonoBehaviour {
    public float distance = 1;
    public float duration = 3;

    private Vector3 from;
    private Vector3 to;
    private Vector3 destination;
    private float timer;

    void Start() {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        from = transform.position;
        destination = from;
        to = new Vector3(transform.position.x + spriteRenderer.bounds.size.x * distance, transform.position.y);
        Debug.Log(spriteRenderer.bounds.size.x);
    }

    void Update() {
        if (transform.position != destination) {
            Move();
        }
    }

    private void Move() {
        timer += Time.deltaTime;
        if (timer >= duration) {
            transform.position = destination;
        } else {
            transform.position = Vector3.Lerp(transform.position, destination, timer / duration);
        }
    }

    public void StartTransition() {
        timer = 0;
        destination = to;
    }

    public void StopTransition() {
        timer = 0;
        destination = from;
    }
}
