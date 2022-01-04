using System;
using System.Collections.Generic;
using Entity;
using Model;
using SimpleJSON;
using UnityEngine;
using Utils;
using View;
using EventType = Entity.EventType;

namespace Controller
{
    public class RankController : MonoBehaviour
    {
        [SerializeField] private RankView view;

        public static RankController Singleton;

        public void Awake()
        {
            Singleton = this;
            EventCenter.AddListener<string>(EventType.ShowToast, ShowToast);
        }

        /// <summary>
        /// 请求排行榜数据
        /// </summary>
        public void ReqRankData()
        {
            RankModel.CreateInstance().ReqRankData(gameObject);
        }

        public void ReadJsonData(string str)
        {
            var simpleJson = JSON.Parse(str);
            var list = new List<JsonModel>();
            var data = simpleJson["data"]["list"];
            for (int i = 0; i < data.Count; i++)
            {
                var jsonModel = new JsonModel(data[i]["uid"], data[i]["nickName"],
                    data[i]["avatar"],
                    data[i]["trophy"]
                );
                list.Add(jsonModel);
            }

            list.Sort((a, b) => Convert.ToInt32(b.trophy) - Convert.ToInt32(a.trophy));

            RankModel.CreateInstance().JsonList = list;
            RankModel.CreateInstance().CountDownValue = 2048;
            OpenRank();
        }

        private void OpenRank()
        {
            view.OpenRankPanel();
        }

        /**
         * 提示框
         */
        private void ShowToast(string toastTxt)
        {
            view.ShowToast(toastTxt);
        }
    }
}