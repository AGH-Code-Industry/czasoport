using UnityEngine;
/// <summary>
/// API to play song with fade in effect and specified delay;
/// </summary>
public class MusicPlayer : MonoBehaviour {
    public float FadeDuration;
    public AudioSource audioSource;

    private float _timer = 0;
    [SerializeField][Range(0, 1)] private float _musicVolume = 1f;
    public void Play(AudioClip clip, float delay = 0.0f) {
        audioSource.volume = 0;
        audioSource.clip = clip;
        audioSource.time = delay;
        audioSource.Play();
        _timer = 0;
    }

    public float ClipTime() {
        return audioSource.time;
    }
    public void ChangeVolume(float value) {
        _musicVolume = value;
        audioSource.volume = _musicVolume;
    }
    void Update() {
        if (_timer <= FadeDuration) {
            _timer += Time.deltaTime;
            audioSource.volume = _timer / FadeDuration * _musicVolume;
        }
    }
}