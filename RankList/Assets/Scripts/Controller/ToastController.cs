using UnityEngine;
using View;

namespace Controller
{
    public class ToastController : MonoBehaviour
    {
        [SerializeField] private ToastView view;

        public static ToastController Singleton;

        public void Awake()
        {
            Singleton = this;
        }

        public void ShowText(string txt)
        {
            view.ShowToast(txt);
            CancelInvoke(nameof(HideText));
            Invoke(nameof(HideText), 2);
        }

        public void HideText()
        {
            view.HideToast();
        }
    }
}