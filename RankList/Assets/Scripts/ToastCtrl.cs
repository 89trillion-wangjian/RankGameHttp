using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToastCtrl : MonoBehaviour
{
    // Start is called before the first frame update
    public Text text;
    void Start()
    {
        
    }

    public void ShowText(string txt)
    {
        this.gameObject.SetActive(true);
        this.text.text = txt;
        Invoke("HideText", 2);
    }

    public void HideText()
    {
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
