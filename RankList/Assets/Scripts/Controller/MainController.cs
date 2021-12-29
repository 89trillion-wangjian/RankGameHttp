using System;
using System.Collections;
using System.Collections.Generic;
using Model;
using SimpleJSON;
using UnityEngine;
using View;

namespace Controller
{
    public class MainController : MonoBehaviour
    {
        [SerializeField] private MainController mainCtrl;

        [SerializeField] private MainView mainView;

        private readonly MainModel _mainModel = MainModel.CreateInstance();

        private int _countDownValue;

        private void Awake()
        {
            _mainModel.MainCtrl = mainCtrl;
        }

        /// <summary>
        /// 请求排行榜数据
        /// </summary>
        public void ReqRankData()
        {
            _mainModel.ReqRankData(gameObject);
        }

        public void ReadJson(string str)
        {
            var simpleJson = JSON.Parse(str);
            var list = new List<JsonModel>();
            var listData = simpleJson["data"]["list"];
            for (int i = 0; i < listData.Count; i++)
            {
                var jsonModel = new JsonModel(listData[i]["uid"], listData[i]["nickName"],
                    listData[i]["avatar"],
                    listData[i]["trophy"]
                );
                list.Add(jsonModel);
            }

            list.Sort((a, b) => Convert.ToInt32(b.trophy) - Convert.ToInt32(a.trophy));
            MainModel.CreateInstance().JsonList = list;
            MainModel.CreateInstance().CountDownValue = 2048;
            mainView.ShowRankPanel();
        }

        public void RenderMyRankInfo()
        {
            List<JsonModel> json = MainModel.CreateInstance().JsonList;
            for (int i = 0; i < json.Count; i++)
            {
                if (json[i].id != DataManager.CreateInstance().mySelfId)
                {
                    continue;
                }

                mainView.ChangeRankStatus(i < 3, json);
            }

            _countDownValue = MainModel.CreateInstance().CountDownValue;
            StopCoroutine(nameof(StartCutDown));
            StartCoroutine(nameof(StartCutDown));
        }

        private IEnumerator StartCutDown()
        {
            while (_countDownValue > 0)
            {
                _countDownValue--;
                mainView.UpdateCountDownTxt(_countDownValue);
                yield return new WaitForSeconds(1.0f);
            }
        }

        public int GetCountDownValue()
        {
            return MainModel.CreateInstance().CountDownValue;
        }
    }
}