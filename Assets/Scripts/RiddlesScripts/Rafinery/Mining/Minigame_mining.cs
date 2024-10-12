using PlayerScripts;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Minigame_mining : MonoBehaviour 
{
    [SerializeField] private Button[] _stones;
    [SerializeField] private GameObject _minigameCanvas;
    private int _gemPosition; // random gem position on the start of minigame
    private string _minigameState;
    [SerializeField] private Sprite _empty;
    [SerializeField] private Sprite _gemSprite;
    [SerializeField] private Transform _player;
    [SerializeField] private GameObject _gemPrefab;
    [SerializeField] private AudioClip[] _sfxes = new AudioClip[2];
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Sprite _shovelDone;
    // Start is called before the first frame update
    void Start()
    {
        _gemPosition = Random.Range(0, 7);
        Debug.Log("GEM POSITIONl: " + _gemPosition);
        _player = FindObjectOfType<Player>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MineItUp(int id) {
        Button _btn = _stones[id];
        Debug.Log("Clicked" + id.ToString());
        if (_btn.CompareTag("Q_Stone")) {
            _audioSource.clip = _sfxes[0];
            _btn.image.sprite = (id == _gemPosition) ? _gemSprite : _empty;
            if (id == _gemPosition) _btn.tag = "Q_Gem";
        }
        else if (_btn.CompareTag("Q_Gem")) {
            Debug.Log("U have picked up the gem");
            _audioSource.clip = _sfxes[1];
            InstantiateTheGem();
            _minigameCanvas.SetActive(false);
            FindObjectOfType<MiningQuestInit>().transform.tag = "Untagged";
            FindObjectOfType<MiningQuestInit>().GetComponent<SpriteRenderer>().sprite = _shovelDone;
        }
        _audioSource.Play();
    }
    private void InstantiateTheGem() {
        Instantiate(_gemPrefab, new Vector3(_player.position.x + 1f, _player.position.y + 1f, _player.position.z), Quaternion.identity);
    }
}
