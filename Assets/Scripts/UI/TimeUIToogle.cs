using UnityEngine;
using UnityEngine.UI;

namespace  UI {
    public class TimeUIToogle : MonoBehaviour {
        [SerializeField] private Sprite active;
        [SerializeField] private Sprite disactive;
        private Image _image;

        private void Awake() {
            _image = GetComponent<Image>();
        }

        public void SetStroke(bool isActive) {
            if (isActive) _image.sprite = active;
            else _image.sprite = disactive;
        }
    }
}
