using UnityEngine;

public class MusicPlayer : MonoBehaviour {
    static public MusicPlayer Instance;
    public float FadeDuration;

    private AudioSource _audioSource;
    private float _timer = 0;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    private void Start() {
        _audioSource = Camera.main.GetComponent<AudioSource>();
    }   

    public void Play(AudioClip clip, float delay = 0.0f) {
        _audioSource.volume = 0;
        _audioSource.clip = clip;
        _audioSource.time = delay;
        _audioSource.Play();
        _timer = 0;
    }

    public float ClipTime() {
        return _audioSource.time;
    }

    void Update() {
        if (_timer <= FadeDuration) {
            _timer += Time.deltaTime;
            _audioSource.volume = _timer / FadeDuration;
        }
    }
}
