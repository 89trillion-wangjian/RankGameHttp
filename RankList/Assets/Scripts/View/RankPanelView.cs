using System.Collections.Generic;
using Entity;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class RankPanelView : MonoBehaviour
    {
        [SerializeField] private Image rankimg;

        [SerializeField] private Text rankNumTxt;

        [SerializeField] private Text userName;

        [SerializeField] private Text cupCountTxt;

        [SerializeField] private Text countDownTxt;

        public void ClosePanel()
        {
            Destroy(gameObject);
        }

        /// <summary>
        /// 更新排名状态/图等
        /// </summary>
        /// <param name="isTopThree">是否是前三名</param>
        /// <param name="json">玩家数据</param>
        /// <param name="ranking">排名</param>
        public void ChangeRankStatus(bool isTopThree, List<JsonModel> json, int ranking = 0)
        {
            rankimg.gameObject.SetActive(isTopThree);
            rankNumTxt.gameObject.SetActive(!isTopThree);
            if (isTopThree)
            {
                rankimg.sprite =
                    Resources.Load<Sprite>($"RankLevel/rank_{ranking + 1}");
                if (rankimg.sprite != null)
                {
                    rankimg.rectTransform.sizeDelta =
                        new Vector2(rankimg.sprite.rect.width, rankimg.sprite.rect.height);
                }
            }

            if (!isTopThree)
            {
                rankNumTxt.text = $"{ranking + 1}";
            }

            userName.text = json[ranking].nickName;
            cupCountTxt.text = json[ranking].trophy;
        }


        public void ShowCutDown(string value)
        {
            this.countDownTxt.text = value;
        }
    }
}