using System.Collections.Generic;
using Controller;
using Model;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class MainView : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField] private GameObject rankPanel;
        [SerializeField] private Image rankimg;
        [SerializeField] private Text rankNumTxt;
        [SerializeField] private Text userName;
        [SerializeField] private Text cupCountTxt;
        [SerializeField] private Text countDownTxt;
        [SerializeField] private MainController mainCtrl;
        /**
         * 点击排行榜按钮请求数据
         */
        public void OnReqRankData()
        {
            mainCtrl.ReqRankData();
        }
        /**
         * response
         */
        public void OnRespRankData()
        {
            rankPanel.SetActive(true);
            mainCtrl.RenderMyRankInfo();
        }

        /**
         * 更新排名状态/图等
         */
        public void OnChangeRankStatus(bool ranking, List<JsonModel> json, int i = 0)
        {
            rankimg.gameObject.SetActive(ranking);
            rankNumTxt.gameObject.SetActive(!ranking);
            if (ranking)
            {
                rankimg.sprite =
                    Resources.Load(string.Concat("ranking/rank_", (i + 1)), typeof(Sprite)) as Sprite;
                if (rankimg.sprite != null)
                    rankimg.rectTransform.sizeDelta =
                        new Vector2(rankimg.sprite.rect.width, rankimg.sprite.rect.height);
            }

            if (!ranking)
            {
                rankNumTxt.text = i + 1 + "";
            }

            userName.text = json[i].nickName;
            cupCountTxt.text = json[i].trophy;
        }

        public void UpdateCountDownTxt(int value)
        {
            this.countDownTxt.text = string.Concat("Ends in:", value, "秒");
        }


        public void OnHideRank()
        {
            rankPanel.SetActive(false);
        }
    }
}