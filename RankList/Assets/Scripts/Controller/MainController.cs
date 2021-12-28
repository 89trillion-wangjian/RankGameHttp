using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Model;
using SimpleJSON;
using UnityEngine;
using UnityEngine.Serialization;
using View;
using Object = System.Object;

namespace Controller
{
    public class MainController : MonoBehaviour
    {
        [SerializeField] private MainController mainCtrl;
        public MainView view;

        private readonly MainModel _mainModel = MainModel.CreateInstance();

        private int _countDownValue;

        private void Awake()
        {
            _mainModel.MainCtrl = mainCtrl;
        }

        /**
         * 请求排行榜数据
         */
        public void ReqRankData()
        {
            _mainModel.ReqRankData(gameObject);
        }

        public void ReadJson(string str)
        {
            var simpleJson = JSON.Parse(str);
            List<JsonModel> list = new List<JsonModel>();
            JSONNode listData = simpleJson["data"]["list"];
            for (int i = 0; i < listData.Count; i++)
            {
                JsonModel jsonModel = new JsonModel(listData[i]["uid"], listData[i]["nickName"],
                    listData[i]["avatar"],
                    listData[i]["trophy"]
                );
                list.Add(jsonModel);
            }

            list.Sort((a, b) => Convert.ToInt32(b.trophy) - Convert.ToInt32(a.trophy));
            MainModel.CreateInstance().JsonList = list;
            MainModel.CreateInstance().CountDownValue = 2048;
            view.OnRespRankData();
        }


        public void RenderMyRankInfo()
        {
            List<JsonModel> json = MainModel.CreateInstance().JsonList;
            for (int i = 0; i < json.Count; i++)
            {
                if (json[i].id == DataManager.CreateInstance().mySelfId)
                {
                    if (i < 3)
                    {
                        view.OnChangeRankStatus(true, json);
                    }
                    else
                    {
                        view.OnChangeRankStatus(false, json);
                    }
                }
            }

            _countDownValue = MainModel.CreateInstance().CountDownValue;
            StopCoroutine("StartCutDown");
            StartCoroutine("StartCutDown");
        }

        IEnumerator StartCutDown()
        {
            while (_countDownValue > 0)
            {
                _countDownValue--;
                view.UpdateCountDownTxt(_countDownValue);
                yield return new WaitForSeconds(1.0f);
            }
        }

        public int GetCountDownValue()
        {
            return MainModel.CreateInstance().CountDownValue;
        }
    }
}