using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace View
{
    public class RankItemCtrl : MonoBehaviour
    {
        [SerializeField] private RankItemCtrl rankItemCtrl;

        [SerializeField] private ToastView toastView;

        public static RankItemCtrl Singleton;

        private MyListItemModel itemData;


        public void Awake()
        {
            Singleton = rankItemCtrl;
        }

        public void ShowToast()
        {
            toastView.ShowText("User: " + itemData.NickName + "    Rank: " + itemData.Ranking);
        }

        public void SetData(MyListItemModel model)
        {
            this.itemData = model;
        }
    }
}