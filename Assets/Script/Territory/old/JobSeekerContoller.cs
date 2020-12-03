using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using LitJson;


public class JobSeekerContoller : MonoBehaviour
{


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
    int classnum=-1;
    int cost=-1;
    int myindex;
    
    [SerializeField]
    GameObject Me;
    void Start()
    {

        JsonIO.JsonTake();
       
        if (StartSign == 100)
        {
            
            for (int i = 0 ; i < GlobalVariable.MaxjobSeekerIndex ; i++)
            {
                if(GlobalVariable.jobSeekerIndexCheck[i] != -1)
                {
                    myindex = i;
                    break;
                }
            }
            LvText.text = "레벨 : " + JsonIO.save.JobSeeker_savedata[myindex].Level.ToString();
            CostText.text = "비용 : " + (JsonIO.save.JobSeeker_savedata[myindex].Level * 100).ToString();
            ClsassNameText.text = "직업 : " + JsonIO.save.JobSeeker_savedata[myindex].Job;
            NameText.text = "이름 : " + JsonIO.save.JobSeeker_savedata[myindex].Name;
            
        }

        if(StartSign == 90)
        {
            for (int i = 0; i < GlobalVariable.MaxjobSeekerIndex; i++)
            {
                if (GlobalVariable.jobSeekerIndexCheck[i] != -1)
                {
                    GlobalVariable.jobSeekerIndexCheck[i] = -1;
                    myindex = i;
                    break;
                }
            }
            
            SelectPortrai = GetComponent<Image>();
            SelectPortrai.sprite = Portrai[JsonIO.save.JobSeeker_savedata[myindex].Number];
            
        }
        JsonIO.JsonExport();
    }

    public void Employment()
    {
        transform.parent.parent.parent.parent.GetComponent<AudioSource>().Play();


        JsonIO.JsonTake();
        Character character = new Character(classname);
        int i;
        for(i=1;i< Variable.RosterSize; i++)//Variable.RosterSize
        {
            if( JsonIO.save.Roster[i] == null || JsonIO.save.Roster[i].Number == 0)
            {
                if (JsonIO.save.Roster[i] == null)
                    JsonIO.save.Roster[i] = new SaveData_Character();

                LV = JsonIO.save.JobSeeker_savedata[myindex].Level;
                JsonIO.save.Roster[i].Name = JsonIO.save.JobSeeker_savedata[myindex].Name;
                JsonIO.save.Roster[i].State = "asd";
                JsonIO.save.Roster[i].Job = JsonIO.save.JobSeeker_savedata[myindex].Job;
                JsonIO.save.Roster[i].Level = JsonIO.save.JobSeeker_savedata[myindex].Level;
                JsonIO.save.Roster[i].Number = JsonIO.save.JobSeeker_savedata[myindex].Number;
                JsonIO.save.Roster[i].Health = JsonIO.save.JobSeeker_savedata[myindex].Health;
                JsonIO.save.Roster[i].Morale = JsonIO.save.JobSeeker_savedata[myindex].Morale;
                for (int j=1; j< Variable.SkillMany; j++)
                {
                    JsonIO.save.Roster[i].Skill_Levels[j] = 1;
                }
                JsonIO.save.Roster[i].EquipLevel = 1;
                JsonIO.save.Roster[i].AccessoryIndex = 0;
           
                JsonIO.save.JobSeeker_savedata[myindex].Number = 0;
            
                
                JsonIO.JsonExport();
                //GameObject.Find("MercenaryScroll["+i+"]").SendMessage("MakeMersemary");
                GameObject.Find("JobSeekerScroll").SendMessage("Resize_jobSeeker");
                GameObject.Find("MercenaryScroll_Guild").SendMessage("Resize_GuildLeftScroll");
                // GameObject.Find("MercenaryScroll_Acc").SendMessage("Acc_Resize_mersenary");


                //아래 라인도 수정함
                GlobalVariable.jobSeeker[myindex].SetActive(false);
                goto jmp;
            }
        }
        Debug.Log("자리가 꽉참");
    jmp:;
    }
  
    public void Portraichange()
    {

    }
}


