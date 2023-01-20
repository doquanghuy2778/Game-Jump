using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvilCtl : MonoBehaviour
{
    
    //goi doi tuong con
    public GameObject Obs_Prefab;
    //lay doi tuong cha
    public Transform Obs_Parent;

    public List<ObsCtl> SpawendObs { private set; get;  }

    public GameObject START_ORI_OBS;
    // Start is called before the first frame update
    void Start() 
    {
        SqawnFirstObs();
    }

    public void SqawnFirstObs()
    {
        if (SpawendObs == null)
        {
            SpawendObs = new List<ObsCtl>();
            //sinh ra cac doi tuong giong nhau
            for (int i = 0; i < 6; i++)
            {
                SpawnNextObs();
            }
        } else
        {
            for(int i = 0; i < SpawendObs.Count; i++)
            {
                SpawendObs[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < 6; i++)
            {
                SpawnNextObs();
            }
        }
    }

    public void SpawnNextObs()
    {
        //random length
        int rand = UnityEngine.Random.Range(4, 10); // rand do dai cua obs 
        int rand_dis = UnityEngine.Random.Range(2, 3); // rand do dai khoang cach giua moi obs
        GameObject goj = SpawnObs(rand);
        goj.transform.position = new Vector3(0, -3.9f,
            MyGameManager.instance.LastSpownPos
            + rand // do dai obs 
            + rand_dis);

        MyGameManager.instance.LastSpownPos += (rand + rand_dis);
    }
    GameObject SpawnObs(int rand_length)
    {
         //neu như có 1 vật có cùng độ dài và khoảng cách rand thì sẽ sử dụng lại nó nhằm tiết kiệm dữ liệu với gánh nặng cho cpu 
          for(int i = 0; i < SpawendObs.Count; i++)
          {
           
            if (SpawendObs[i].ObsLenght == rand_length 
                && MyGameManager.instance.playerctl.transform.position.z > MyGameManager.instance.envilctl.SpawendObs[i].gameObject.transform.position.z + 15f)
            {
                return SpawendObs[i].gameObject;
            }
            //neu xuat hien obs giong voi cai da tung xuat hien thi tra ve no, neu khong thif phai tao cai moi
           
          }
        //if not
        GameObject goj = Instantiate(Obs_Prefab, Obs_Parent, false);
        goj.transform.localScale = new Vector3(goj.transform.localScale.x, goj.transform.localScale.y, rand_length);
        ObsCtl obsCtl = goj.GetComponent<ObsCtl>();
        obsCtl.ObsLenght = rand_length;
        SpawendObs.Add(obsCtl);
        return goj;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    //obj pooling  : sử dụng lại 

}




