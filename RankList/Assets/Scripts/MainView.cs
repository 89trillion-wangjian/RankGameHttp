using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using SimpleJSON;
using UnityEngine;
using UnityEngine.UI;
using Object = System.Object;

public class MainView : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject rankPanel;
    public Image rankimg;
    public Text rankNumTxt;
    public Text userName;
    public Text cupCountTxt;
    public Text countDownTxt;

    private int countDownValue;
    private float freshSpeed = 0;
    void Start()
    {
        // rankPanel.SetActive(false);
        // StreamReader streamReader = new StreamReader(string.Concat(Application.dataPath , "/Data/ranklist.json"));
        // string str = streamReader.ReadToEnd();
        // var simpleJson = JSON.Parse(str);
        // Debug.Log("json数据" + simpleJson["list"][0]["uid"]);
        
        // DataManager.CreateInstance().JsonNode = simpleJson["list"];
        // List<JsonModel> list = new List<JsonModel>();
        // for(int i = 0; i < simpleJson["list"].Count; i++){
        //     list.Add(simpleJson["list"][i]);
        // }
        // list.Sort((a, b) => { return Convert.ToInt32(a.trophy) - Convert.ToInt32(b.trophy); });
        
        // Debug.Log(list);
        // countDownValue = simpleJson["countDown"];

    }

    public void OnOpenRank()
    {
        //
        // JSONNode jsonNode = DataManager.CreateInstance().JsonNode;
        // for (int i = 0; i < jsonNode.Count; i++)
        // {   
        //     if (jsonNode[i]["uid"] == DataManager.CreateInstance().mySelfId)
        //     {
        //         if (i < 3)
        //         {
        //             rankimg.gameObject.SetActive(true);
        //             rankNumTxt.gameObject.SetActive(false);
        //             rankimg.sprite = Resources.Load(string.Concat("ranking/rank_", (i + 1)) , typeof(Sprite)) as Sprite;
        //             rankimg.rectTransform.sizeDelta = new Vector2(rankimg.sprite.rect.width,rankimg.sprite.rect.height);
        //         }
        //         else
        //         {
        //             rankimg.gameObject.SetActive(false);
        //             rankNumTxt.gameObject.SetActive(true);
        //             rankNumTxt.text = i + 1 + "";
        //         }
        //         userName.text = jsonNode[i]["nickName"];
        //         cupCountTxt.text = jsonNode[i]["trophy"];
        //     }
        //     
        // }
        reqRankData();
        

        // DataManager.CreateInstance().JsonNode = newData;

        // 
    }

    public void onOpenRankPanel(string data)
    {
        
        var simpleJson = JSON.Parse(data);
        DataManager.CreateInstance().JsonNode = simpleJson["data"]["list"];
        JSONNode jsonNode = DataManager.CreateInstance().JsonNode;
        for (int i = 0; i < jsonNode.Count; i++)
        {   
            if (jsonNode[i]["uid"] == DataManager.CreateInstance().mySelfId)
            {
                if (i < 3)
                {
                    rankimg.gameObject.SetActive(true);
                    rankNumTxt.gameObject.SetActive(false);
                    rankimg.sprite = Resources.Load(string.Concat("ranking/rank_", (i + 1)) , typeof(Sprite)) as Sprite;
                    rankimg.rectTransform.sizeDelta = new Vector2(rankimg.sprite.rect.width,rankimg.sprite.rect.height);
                }
                else
                {
                    rankimg.gameObject.SetActive(false);
                    rankNumTxt.gameObject.SetActive(true);
                    rankNumTxt.text = i + 1 + "";
                }
                userName.text = jsonNode[i]["nickName"];
                cupCountTxt.text = jsonNode[i]["trophy"];
            }
            
        }
        
        rankPanel.SetActive(true);

        countDownValue = 2048;
        StopCoroutine("startCutDown");
        StartCoroutine("startCutDown");
    }



    IEnumerator startCutDown()
    {
        while (countDownValue > 0)
        {
            countDownValue--;
            this.countDownTxt.text = string.Concat("Ends in:" , countDownValue , "秒");
            yield return new WaitForSeconds(1.0f);
        }
    }

    public void onHideRank()
    {
        rankPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // freshSpeed += Time.deltaTime;
        // if (freshSpeed >= 1.0f)
        // {
        //     freshSpeed = 0;
        //     countDownValue--;
        //     this.countDownTxt.text = "Ends in:" + countDownValue + "秒";
        // }
    }

    public void reqRankData()
    {

        RankData rankData = new RankData(gameObject)
        {
            OnSuccess = OnRequestSuccess,
            OnCacheRestore = OnRequestSuccess,
            OnError = OnRequestError,
            ForceRequest = IsForceRequest,
        };
        Object group = new Object();
        HttpClientBuilder clientBuilder = new HttpClientBuilder(DomainType.rankHttp)
            .Path("admin/rankList")
            .Param("type", 1)
            .Param("page", 1)
            .Param("season", 18)
            .Param("token", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1aWQiOiI0MzY4NjY1MjcifQ.drFj2OtLEjgE452sgtHPG73xU-yQ-OXvbz4Utxl2M1k")
            .Method(HttpMethod.Get);
        rankData.SendRequest(clientBuilder);
    }

    public void OnRequestSuccess(string data)
    {
        Debug.Log("请求成功" + data);
        onOpenRankPanel(data);
    }
    public void OnRequestError(string data, int code)
    {
        Debug.Log("请求失败" + data);
    }

    public bool IsForceRequest;

}


public class JsonModel
{
    public string id;
    public string nickName;
    public string avatar;
    public string trophy;
    public int ranking;

    public JsonModel(string id, string nickName, string avatar, string trophy, int ranking  )
    {
        this.id = id;
        this.nickName = nickName;
        this.avatar = avatar;
        this.trophy = trophy;
        this.ranking = ranking;
        
    }
}
