using UnityEngine;
using LevelTimeChange.TimeChange;

public class GameMusicManager : MonoBehaviour {
    public AudioClip PastMusic;
    public AudioClip PresentMusic;
    public AudioClip FutureMusic;
    public MusicPlayer musicPlayer;

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
    private LevelTimeChange.TimeLine _timeline = LevelTimeChange.TimeLine.Present;

    void Start() {
        _pastMusicState = new MusicState(PastMusic);
        _presentMusicState = new MusicState(PresentMusic);
        _futureMusicState = new MusicState(FutureMusic);

        TimeChanger.Instance.OnTimeChange += OnTimeChangeHandler;

        ChangeMusic(_timeline);
    }

    private void OnTimeChangeHandler(object sender, TimeChanger.OnTimeChangeEventArgs args) {
        ChangeMusic(args.time);
    }

    private void ChangeMusic(LevelTimeChange.TimeLine newTimeline) {
        SaveCurrentMusicState();
        _timeline = newTimeline;
        PlayMusic(newTimeline);
    }

    private void SaveCurrentMusicState() {
        switch (_timeline) {
            case LevelTimeChange.TimeLine.Past:
                _pastMusicState.delay = musicPlayer.ClipTime();
                break;
            case LevelTimeChange.TimeLine.Present:
                _presentMusicState.delay = musicPlayer.ClipTime();
                break;
            case LevelTimeChange.TimeLine.Future:
                _futureMusicState.delay = musicPlayer.ClipTime();
                break;
        }
    }

    private void PlayMusic(LevelTimeChange.TimeLine newTimeline) {
        switch(newTimeline) {
            case LevelTimeChange.TimeLine.Past:
                musicPlayer.Play(_pastMusicState.clip, _pastMusicState.delay);
                break;
            case LevelTimeChange.TimeLine.Present:
                musicPlayer.Play(_presentMusicState.clip, _presentMusicState.delay);
                break;
            case LevelTimeChange.TimeLine.Future:
                musicPlayer.Play(_futureMusicState.clip, _futureMusicState.delay);
                break;
        }
    }
}
