using Controller;
using UnityEngine;

namespace View
{
    public class RankView : MonoBehaviour
    {
        [SerializeField] private GameObject rankPanelItem;

        [SerializeField] private GameObject toast;

        public void OpenRankPanel()
        {
            Instantiate(rankPanelItem, this.transform, false);
        }

        /**
         * 提示框
         */
        public void ShowToast(string toastTxt)
        {
            if (ToastController.Singleton)
            {
                ToastController.Singleton.ShowText(toastTxt);
                return;
            }

            Instantiate(toast, transform, false);
            ToastController.Singleton.ShowText(toastTxt);
        }
    }
}