using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtl : MonoBehaviour
{
    public float jumVelocity;
    public float fallSpeed;
    public float fallDownSpeed;

    private float speed;
    public Rigidbody Rb { private set; get; }

    private void Awake()
    {
        //bien ORI_speed nam trong class Const nen phai viet nhu ben duoi
        speed = Const.ORI_speed;
    }
    // Start is called before the first frame update
    void Start()
    {
        //lay doi tuong Rigibody
        Rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //neu doi tuong khac trang thai playing thi se khong chay
        if (MyGameManager.instance.currGameState != GameState.playing)
        {
            return; 
        }
        //check death
        if (IsDeath())
        {
            //endgame
            Debug.Log("<color=red>Death </color>");
            MyGameManager.instance.currGameState = GameState.death;
            //sau khi chet thi khong con trong luc nua
            Rb.useGravity = false;
            //fix truong hop khong con trong luc nhung van roi xuong
            Rb.velocity = Vector3.zero;
        }

        // move by Z
        transform.Translate(0, 0, speed * Time.deltaTime);

        //jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if(Rb.velocity.y < 0)
        {
            Rb.velocity += Vector3.up * Physics.gravity.y * (fallSpeed - 1) * Time.deltaTime;
            Rb.AddForce(Vector3.down * fallDownSpeed);
        }
        if (MyGameManager.instance.envilctl.SpawendObs != null)
        {
            for (int i = 0; i < MyGameManager.instance.envilctl.SpawendObs.Count; i++)
            {
                if (transform.position.z > MyGameManager.instance.envilctl.SpawendObs[i].gameObject.transform.position.z + 15f)
                {
                    MyGameManager.instance.envilctl.SpawnNextObs();
                    break;
                }
            }
        }

        //tinh diem
        MyGameManager.instance.UpdateScore((long)transform.position.z);
    }
    
    void Jump()
    {
        if (IsGround())
        {
            Debug.Log("<color=yellow>Playing </color>");
            //set gia tri mac dinh cho van toc
            Rb.velocity = Vector3.zero;
            Rb.angularVelocity = Vector3.zero;

            Rb.velocity = Vector3.up * jumVelocity;
        }
    }

    //kiem tra xem co o tren mat dat hay khong
    bool IsGround()
    {
        return transform.position.y < -0.8f && transform.position.y > -1.1f;
    }

    //kiem tra chet
    bool IsDeath()
    {
        return transform.position.y < Const.Death_Pos_Y;
    }
}
