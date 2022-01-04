using System;
using System.Collections;
using System.Collections.Generic;
using Entity;
using Model;
using UnityEngine;
using View;

namespace Controller
{
    public class RankPanelController : MonoBehaviour
    {
        [SerializeField] private RankPanelView rankView;

        private int countDownValue = 0;

        public void Start()
        {
            countDownValue = RankModel.CreateInstance().CountDownValue;
            RenderRankInfo();
        }

        private void FreshMyRankInfo(bool isTopThree, List<JsonModel> json)
        {
            rankView.ChangeRankStatus(isTopThree, json);
        }

        private void RenderRankInfo()
        {
            var json = RankModel.CreateInstance().JsonList;
            for (int i = 0; i < json.Count; i++)
            {
                if (json[i].id == RankModel.CreateInstance().MySelfId)
                {
                    FreshMyRankInfo(i < 3, json);
                    break;
                }
            }

            StopCoroutine(nameof(StartCutDown));
            StartCoroutine(nameof(StartCutDown));
        }

        private IEnumerator StartCutDown()
        {
            while (countDownValue > 0)
            {
                countDownValue--;
                UpdateCountDownTxt(countDownValue);
                yield return new WaitForSeconds(1.0f);
            }
        }

        /// <summary>
        /// 更新倒计时
        /// </summary>
        /// <param name="value"></param>
        private void UpdateCountDownTxt(int value)
        {
            this.rankView.ShowCutDown($"Ends in: {FormatTime(value)}");
        }

        /// <summary>
        /// 格式化时间
        /// </summary>
        /// <param name="seconds">秒</param>
        /// <returns></returns>
        private static string FormatTime(float seconds)
        {
            TimeSpan ts = new TimeSpan(0, 0, Convert.ToInt32(seconds));
            string str = "";

            if (ts.Hours > 0)
            {
                str = $"{ts.Hours:00}时{ts.Minutes:00}分{ts.Seconds:00}秒";
            }

            if (ts.Hours == 0 && ts.Minutes > 0)
            {
                str = $"{ts.Minutes:00}分{ts.Seconds:00}秒";
            }

            if (ts.Hours == 0 && ts.Minutes == 0)
            {
                str = $"00:00:{ts.Seconds:00}秒";
            }

            return str;
        }

        public void ClosePanel()
        {
            Destroy(this.gameObject);
        }
    }
}