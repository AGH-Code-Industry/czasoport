using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogues.ChoiceProcessing {
    public class ChoiceButton : MonoBehaviour {
        public Button button;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private Image icon;
        [SerializeField] private Image icon2;

        public void SetText(string newText) {
            text.SetText(newText);
        }
        
        public void SetIcon(Sprite newIcon) {
            icon.sprite = newIcon;
            icon.gameObject.SetActive(true);
        }
        
        public void SetIcon2(Sprite newIcon) {
            icon2.sprite = newIcon;
            icon2.gameObject.SetActive(true);
        }
    }
}