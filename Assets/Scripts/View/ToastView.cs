using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class ToastView : MonoBehaviour
    {
        [SerializeField] private Text toastTxt;

        public void ShowToast(string txt)
        {
            transform.SetAsLastSibling();
            toastTxt.text = txt;
        }

        public void HideToast()
        {
            Destroy(gameObject);
        }
    }
}