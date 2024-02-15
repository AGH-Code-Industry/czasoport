using UnityEngine;
using UnityEngine.UI;

namespace  UI {
    public class TimeUIStroke : MonoBehaviour {
        [SerializeField] private Sprite active;
        [SerializeField] private Sprite disactive;
        private Image _image;

        private void Start() {
            _image = GetComponent<Image>();
        }

        public void IsEnable(bool enable) {
            if (enable) _image.sprite = active;
            else _image.sprite = disactive;
        }
    }
}
