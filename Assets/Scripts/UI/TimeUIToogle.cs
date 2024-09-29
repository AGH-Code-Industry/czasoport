using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class TimeUIToogle : MonoBehaviour {
        [SerializeField] private Sprite active;
        [SerializeField] private Sprite disactive;
        [SerializeField] private Image image;
        [SerializeField] private bool tak;
        [SerializeField] private float activeScale = 3f;

        public void SetStroke(bool isActive) {
            if (isActive) {
                image.sprite = active;
                if (tak) image.gameObject.transform.localScale = new Vector3(activeScale, activeScale, 1f);
                if (tak) image.color = new Color(1f, 1f, 1f, 1f);
                //if (tak) _image.color = Color.black;
            }
            else {
                image.sprite = disactive;
                if (tak) image.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
                if (tak) image.color = new Color(0f, 0f, 0f, 0f);
                //_image.color = Color.white;
            }
        }
    }
}