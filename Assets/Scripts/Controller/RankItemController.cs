using UnityEngine;
using View;

namespace Controller
{
    public class RankItemController : MonoBehaviour
    {
        [SerializeField] private RankItemView view;

        public static RankItemController Singleton;

        private MyListItemModel itemData;

        public void Awake()
        {
            Singleton = this;
        }

        public void SetData(MyListItemModel model)
        {
            itemData = model;
        }

        public void ShowToast()
        {
            view.ShowUserInfo($"user: {itemData.NickName}   Rank: {itemData.Ranking}");
        }
    }
}