using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MyGameManager : MonoBehaviour
{
    //set trang thai game
    public GameState currGameState { get; set; }

    public float LastSpownPos { get; set; }

    public static MyGameManager instance { private set; get; }

    public PlayerCtl playerctl;
    public EnvilCtl envilctl;
    public UICtl UICtl;
    public Const Const;
    
    private void Awake()
    {
        instance = this;
        //xet gia tri mac dinh cho vi tri cua doi tuong
        LastSpownPos = Const.Last_Pos_Ori;
        //dinh nghia trang thai game    
        currGameState = GameState.ready;
    }
    // Start is called before the first frame update
    void Start()
    {
        //Invoke("StartTheGame", 2f); //game chay sau 2 giay
        //Invoke("Test", 1f); 
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    public void StartTheGame()
    {
        UICtl.gojs[2].SetActive(true);
        UICtl.Scoretext.text = "";

        StartCoroutine(CountDown(delegate
        {
            UICtl.Scoretext.text = "999";
            currGameState = GameState.playing;
            //UICtl.Scoretext.text = "999";

        }));
    }

    public void Death()
    {
        UICtl.gojs[1].SetActive(true);
        //currGameState = GameState.death;
        StartCoroutine(CountDown(delegate
        {
            currGameState = GameState.death;
            UICtl.ShowGameOver();
        }));
        //playerctl.Rb.useGravity = false;

        //show game over
        //UiCtl.ShowGameOver();
        //UICtl.ShowGameOver();   
    }

    public void NewGame()
    {
        UICtl.gojs[2].SetActive(true);
        
        LastSpownPos = Const.Last_Pos_Ori;
        envilctl.SqawnFirstObs();
        playerctl.transform.position = new Vector3(0, 2, 0);
        UICtl.gojs[1].SetActive(false);
        StartCoroutine(CountDown(delegate
        {
            UICtl.Scoretext.text = "999";
            currGameState = GameState.playing;
      
            playerctl.Rb.useGravity = true;
            //envilctl.SqawnFirstObs();
        }));

    }

    //ve tim hieu Action
    IEnumerator CountDown(Action callback)
    {
        UICtl.Scoretext.text = "";
        int time = 3;
        while( time > 0)
        {
            UICtl.countDownText.text = time.ToString();
            yield return new WaitForSeconds(1f);
            time--;
            
        }

        UICtl.countDownText.text = "";
        //Invoke("hienthi", 0.5f);
        if (callback != null)
        {
            callback();
        }
    }

    public void UpdateScore(long score)
    {
        UICtl.Scoretext.text = score.ToString();
        
    }
    
    public void Test()
    {
        UICtl.gojs[0].SetActive(false);  
        UICtl.ShowDialog("Do quang huy...", delegate
        {
            UICtl.gojs[0].SetActive(true);
            UICtl.ShowNoti("Show noti after click ok");
         
        });
    }
}


//tim hieu Invoke

public enum GameState
{
    ready, playing, death
}
