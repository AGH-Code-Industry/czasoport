using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class TimeUIToogle : MonoBehaviour {
        [SerializeField] private Sprite active;
        [SerializeField] private Sprite disactive;
        private Image _image;
        [SerializeField] private bool tak;
        [SerializeField] private float activeScale = 3f;

        private void Awake() {
            _image = GetComponent<Image>();
        }

        public void SetStroke(bool isActive) {
            if (isActive) {
                _image.sprite = active;
                if (tak) _image.gameObject.transform.localScale = new Vector3(activeScale, activeScale, 1f);
                if (tak) _image.color = new Color(1f, 1f, 1f, 1f);
                //if (tak) _image.color = Color.black;
            }
            else {
                _image.sprite = disactive;
                if (tak) _image.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
                if (tak) _image.color = new Color(0f, 0f, 0f, 0f);
                //_image.color = Color.white;
            }
        }
    }
}