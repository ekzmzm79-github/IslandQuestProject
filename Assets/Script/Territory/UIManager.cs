using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using LitJson;
using System;

public class UIManager : MonoBehaviour
{
    /*
        2019/05/07 수정자 : 이대경
        수정 내용: 배경 음악 실행 및 성우 사운드, 페이드 인 효과 추과
    */

    [SerializeField]
    FadeIn fadeIn;



    public Text day,money,labor;

    public GameObject guildUI;
    public GameObject forgeUI;
    public GameObject accommodationUI;

   


    void Awake()
    {
        //입출력이 너무 쓸떼없이 많다.?

       // JsonIO.JsonTake();
        guildUI.SetActive(false);
        forgeUI.SetActive(false);
        accommodationUI.SetActive(false);
  
       
        //JsonIO.JsonExport();
    }
    // Start is called before the first frame update
    void Start()
    {
       fadeIn.TriggerOn();//시작할때 페이드인
       transform.GetComponent<AudioSource>().Play();


        JsonIO.JsonTake();

       // MercenaryDataSort();
        

        money.text = JsonIO.save.Money.ToString();
        day.text = JsonIO.save.Day.ToString();
        labor.text = JsonIO.save.Labor[0].ToString() + "/" + JsonIO.save.Labor[1].ToString();

     


        JsonIO.JsonExport();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   

    public void MercenaryDataSort()//로스터 배열에서 값을 받아와 초기화 및 값변경 후 클래스(내림) 레벨(오름) 순으로 정렬
    {
        JsonIO.JsonTake();
        GlobalVariable.MercenaryCount = 0;
        for (int i=0, j=0;i<Variable.RosterSize ;i++ )
        {
            GlobalVariable.mercenaryDatas[i].ClassNum = 0;
            GlobalVariable.mercenaryDatas[i].LV = 0;
            GlobalVariable.mercenaryDatas[i].IndexNum = 0;
            if (JsonIO.save.Roster[i] == null || JsonIO.save.Roster[i].Number == 0)
                continue;

            GlobalVariable.mercenaryDatas[j].ClassNum = JsonIO.save.Roster[i].Number;
            GlobalVariable.mercenaryDatas[j].LV = JsonIO.save.Roster[i].Level;
            GlobalVariable.mercenaryDatas[j].IndexNum = i;
            j++;
            GlobalVariable.MercenaryCount++;

        }
        JsonIO.JsonExport();
        Array.Sort(GlobalVariable.mercenaryDatas, delegate (GlobalVariable.MercenaryData A, GlobalVariable.MercenaryData B) 
        {
            if (A.ClassNum > B.ClassNum)
                return -1;
            else if (A.ClassNum < B.ClassNum)
                return 1;
            else if (A.ClassNum == B.ClassNum)
            {
                if (A.LV > B.LV) 
                {
                    return 1;
                }
            }

            return 0;
        });
    }
}

public static class GlobalVariable
{
    public struct MercenaryData
    {

        public int ClassNum;
        public int IndexNum;
        public int LV;
    }

    public static int jobSeekerIndex = 0;
    public static int MercenaryIndex = 0;
    public static int MaxjobSeekerIndex = JsonIO.save.GuildLV*5;
    
    public static GameObject[] jobSeeker = new GameObject[30];
    public static int[] jobSeekerIndexCheck = new int[30];
    public static GameObject[] Mersenary = new GameObject[30];
    public static int[] MersenaryIndexCheck = new int[30];
    public static GameObject[] Acc_Mersenary = new GameObject[30];
    public static int[] Acc_MersenaryIndexCheck = new int[30];
    
    public static int MercenaryCount;
    public static MercenaryData[] mercenaryDatas = new MercenaryData[31];



}




