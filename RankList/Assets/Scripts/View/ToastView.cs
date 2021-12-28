using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class ToastView : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField] public Text text;

        public void ShowText(string txt)
        {
            this.gameObject.SetActive(true);
            this.text.text = txt;
            CancelInvoke(nameof(HideText));
            Invoke(nameof(HideText), 2);
        }

        public void HideText()
        {
            this.gameObject.SetActive(false);
        }

        // Update is called once per frame
    }
}