using UnityEngine;
using UnityEngine.UI;

namespace  UI {
    public class TimeUIToogle : MonoBehaviour {
        [SerializeField] private Sprite active;
        [SerializeField] private Sprite disactive;
        private Image _image;
        [SerializeField] private bool tak;

        private void Awake() {
            _image = GetComponent<Image>();
        }

        public void SetStroke(bool isActive) {
            if (isActive) {
                _image.sprite = active;
                if (tak)
                    _image.color = Color.black;
            }
            else {
                _image.sprite = disactive;
                _image.color = Color.white;
            }
        }
    }
}
