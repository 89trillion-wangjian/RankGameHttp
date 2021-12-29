using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace View
{
    public class ToastView : MonoBehaviour
    {
        [SerializeField] private Text toastTxt;

        public void ShowText(string txt)
        {
            this.gameObject.SetActive(true);
            this.toastTxt.text = txt;
            CancelInvoke(nameof(HideText));
            Invoke(nameof(HideText), 2);
        }

        public void HideText()
        {
            this.gameObject.SetActive(false);
        }
    }
}