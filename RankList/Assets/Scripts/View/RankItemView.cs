using UnityEngine;
using Utils;
using EventType = Entity.EventType;

namespace View
{
    public class RankItemView : MonoBehaviour
    {
        public void ShowUserInfo(string toastTxt)
        {
            EventCenter.PostEvent(EventType.ShowToast, toastTxt);
        }
    }
}