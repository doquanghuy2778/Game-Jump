using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICtl : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] gojs;
    /*
     * 0: man hinh chinh
     * 1: man hinh chet
     * 2: in game
     * 3: man hinh cai dat
     * 4: thong bao
     * 5: noti
     */
    public Transform popTargetpos;
    public TextMeshProUGUI countDownText;
    public TextMeshProUGUI Scoretext;
    public Material[] mats;
    public Material[] objs;
    public TextMeshProUGUI dialogContent;
    private Action callbackOnBtl = null;
    public TextMeshProUGUI notiContenttext;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < gojs.Length; i++)
        {
            //khi chay game chi hien man hinh start, con cac man hinh khac se an di 
            gojs[i].SetActive(false);
            gojs[i].transform.position = popTargetpos.position;
            
        }
        gojs[0].SetActive(true);
    }

    public void PlayGame()
    {
        gojs[0].SetActive(false);
        MyGameManager.instance.StartTheGame();
    }

    public void ShowGameOver()
    {
        gojs[1].SetActive(true);
        MyGameManager.instance.Death();
        //gojs[1].transform.position = popTargetpos.position;   
    }

    public void ChoiceColorPlayer(int pyte)
    {
        MyGameManager.instance.playerctl.gameObject.GetComponent<Renderer>().material = mats[pyte];
    }

    public void ChoiceColorObj(int ma)
    {
        MyGameManager.instance.envilctl.START_ORI_OBS.gameObject.GetComponent<Renderer>().material = objs[ma];

        for (int i = 0; i < MyGameManager.instance.envilctl.SpawendObs.Count; i++)
        {
            MyGameManager.instance.envilctl.SpawendObs[i].gameObject.GetComponent<Renderer>().material = objs[ma];
        }      
    }

    public void ShowChoiceColorPop()
    {
        gojs[0].SetActive(false);
        gojs[3].SetActive(true);       
    }

    public void ShowDialog(string content, Action callBack = null)
    {
        dialogContent.text = content;
        if(callBack != null)
        {
            callbackOnBtl = callBack;
        }
        gojs[4].SetActive(true);

    }

    public void CloseDialog()
    {
        gojs[4].SetActive(false);
        if(callbackOnBtl != null)
        {
            callbackOnBtl(); 
        }
    }

    public void ShowNoti(string content)
    {
        notiContenttext.text = content;
        gojs[5].SetActive(true);

        Invoke("_ShowNoti", 2f);
    }

    void _ShowNoti()
    {
        gojs[5].SetActive(false);
    }
}
