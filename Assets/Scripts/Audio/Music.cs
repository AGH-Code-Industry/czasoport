using CustomInput;
using UnityEngine;
using UnityEngine.InputSystem;

public class Music : MonoBehaviour {
    public AudioClip PastMusic;
    public AudioClip PresentMusic;
    public AudioClip FutureMusic;

    class MusicState {
        public AudioClip clip;
        public float delay;

        public MusicState(AudioClip audioClip) {
            clip = audioClip;
            delay = 0.0f;
        }
    }

    private MusicState _pastMusicState;
    private MusicState _presentMusicState;
    private MusicState _futureMusicState;
    private LevelTimeChange.TimeLine _timeline = LevelTimeChange.TimeLine.Past;

    void Start() {
        _pastMusicState = new MusicState(PastMusic);
        _presentMusicState = new MusicState(PresentMusic);
        _futureMusicState = new MusicState(FutureMusic);

        CInput.InputActions.Teleport.TeleportBack.performed += past;
        CInput.InputActions.Teleport.TeleportForward.performed += future;

        ChangeMusic(_timeline);
        MusicPlayer.Instance.Play(_pastMusicState.clip);
    }

    private void past(InputAction.CallbackContext obj) {
        ChangeMusic(LevelTimeChange.TimeLine.Past);
    }

    private void future(InputAction.CallbackContext obj) {
        ChangeMusic(LevelTimeChange.TimeLine.Future);
    }

    private void ChangeMusic(LevelTimeChange.TimeLine newTimeline) {
        SaveCurrentMusicState();
        _timeline = newTimeline;
        PlayMusic(newTimeline);
    }

    private void SaveCurrentMusicState() {
        switch (_timeline) {
            case LevelTimeChange.TimeLine.Past:
                _pastMusicState.delay = MusicPlayer.Instance.ClipTime();
                break;
            case LevelTimeChange.TimeLine.Present:
                _presentMusicState.delay = MusicPlayer.Instance.ClipTime();
                break;
            case LevelTimeChange.TimeLine.Future:
                _futureMusicState.delay = MusicPlayer.Instance.ClipTime();
                break;
        }
    }

    private void PlayMusic(LevelTimeChange.TimeLine newTimeline) {
        switch(newTimeline) {
            case LevelTimeChange.TimeLine.Past:
                MusicPlayer.Instance.Play(_pastMusicState.clip, _pastMusicState.delay);
                break;
            case LevelTimeChange.TimeLine.Present:
                MusicPlayer.Instance.Play(_presentMusicState.clip, _presentMusicState.delay);
                break;
            case LevelTimeChange.TimeLine.Future:
                MusicPlayer.Instance.Play(_futureMusicState.clip, _futureMusicState.delay);
                break;
        }
    }
}
