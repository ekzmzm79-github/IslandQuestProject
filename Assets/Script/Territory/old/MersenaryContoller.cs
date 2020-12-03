using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using LitJson;


public class MersenaryContoller : MonoBehaviour
{/*
    string name = "x";
    string classname = "x";
    [SerializeField]
    Text LvText, CostText, ClsassNameText, NameText;
    [SerializeField]
    int StartSign;
    [SerializeField]
    Sprite[] Portrai = new Sprite[5];

    Image SelectPortrai;


    int randtmep;
    int LV = -1;
    int classnum = -1;
    int cost = -1;
    int myindex = GlobalVariable.jobSeekerIndex;

    [SerializeField]
    GameObject Me;
    void Start()
    {
        JsonIO.JsonTake();
        if (StartSign == 100)
        {
            for (int i = 1; i < Variable.RosterSize; i++)
            {
                if (GlobalVariable.MersenaryIndexCheck[i] != -1 )//-1이면 채워진거
                {
                    myindex = i;
                    break;
                }
            }

            Debug.Log("sex" + myindex);
            LvText.text = "레벨 : " + JsonIO.save.Roster[myindex].Level.ToString();
            CostText.text = "비용 : " + (JsonIO.save.Roster[myindex].Level * 100).ToString();
            ClsassNameText.text = "직업 : " + JsonIO.save.Roster[myindex].Job;
            NameText.text = "이름 : " + JsonIO.save.Roster[myindex].Name;
                   
              
        }

        if (StartSign == 90)
        {
            for (int i = 1; i < Variable.RosterSize; i++)
            {
                if (GlobalVariable.MersenaryIndexCheck[i] != -1 )//-1이면 채워진거
                {
                    GlobalVariable.MersenaryIndexCheck[i] = -1;
                    myindex = i;
                    break;
                }
            }
            SelectPortrai = GetComponent<Image>();
            
            SelectPortrai.sprite = Portrai[JsonIO.save.Roster[myindex].Number];
            

        }
        JsonIO.JsonExport();
    }

    public void Fire()
    {
        JsonIO.JsonTake();
       

        JsonIO.save.Roster[myindex].Number = 0;
        JsonIO.JsonExport();
        GlobalVariable.MersenaryIndexCheck[myindex] = 0;
       
        GlobalVariable.Mersenary[myindex].SetActive(false);
        
        GameObject.Find("MercenaryScroll").SendMessage("Resize_mersenary");
        
        // GameObject.Find("MercenaryScroll_Acc").SendMessage("Acc_Resize_mersenary");
    }

    public void Portraichange()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }*/
}


