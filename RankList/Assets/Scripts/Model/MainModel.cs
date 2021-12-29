using System.Collections.Generic;
using Controller;
using UnityEngine;

namespace Model
{
    public class MainModel
    {
        public MainController MainCtrl;

        private static MainModel singleton;

        public int CountDownValue { get; set; }

        public List<JsonModel> JsonList { get; set; }

        public bool isForceRequest = false;

        public static MainModel CreateInstance()
        {
            return singleton ?? (singleton = new MainModel());
        }


        /// <summary>
        /// 请求排行榜数据
        /// </summary>
        /// <param name="gameObject"></param>
        public void ReqRankData(GameObject gameObject)
        {
            RankData rankData = new RankData(gameObject)
            {
                OnSuccess = OnRequestSuccess,
                OnCacheRestore = OnRequestSuccess,
                OnError = OnRequestError,
                ForceRequest = isForceRequest,
            };
            HttpClientBuilder clientBuilder = new HttpClientBuilder(DomainType.rankHttp)
                .Path("admin/rankList")
                .Param("type", 1)
                .Param("page", 1)
                .Param("season", 18)
                .Param("token",
                    "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1aWQiOiI0MzY4NjY1MjcifQ.drFj2OtLEjgE452sgtHPG73xU-yQ-OXvbz4Utxl2M1k")
                .Method(HttpMethod.Get);
            rankData.SendRequest(clientBuilder);
        }

        private void OnRequestSuccess(string data)
        {
            Debug.Log("请求成功" + data);
            MainCtrl.ReadJson(data);
        }

        private void OnRequestError(string data, int code)
        {
            Debug.Log("请求失败" + data);
        }
    }
}