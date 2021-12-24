using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Your.Namespace.Here.UniqueStringHereToAvoidNamespaceConflicts.Lists;

public class RankItemCtrl : MonoBehaviour
{
    // Start is called before the first frame update
    public MyListItemModel model;
    public ToastCtrl toastCtrl;
    void Start()
    {
        
    }

    public void OnBtnClick()
    {
        Text userName = GameObject.Find("userName").GetComponent<Text>();
        Debug.Log("item点击" + model.ranking);
        toastCtrl.ShowText("User: " + model.nickName + "    Rank: " + model.ranking);
    }

    public void SetData(MyListItemModel model)
    {
        this.model = model;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
