using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using LitJson;

public class MercenaryContoller_Acc : MonoBehaviour
{
    [SerializeField]
    int IndexNum;
    [SerializeField]
    Slider health, morale;
    [SerializeField]
    Text LvText, HealthText, MoraleText, NameText;
    [SerializeField]
    GameObject PortaiObj;
    [SerializeField]
    GameObject RelaxButton;
    [SerializeField]
    GameObject EndRelaxButton;
    [SerializeField]
    GameObject RelaxFlag;
    [SerializeField]
    GameObject Acc;
    [SerializeField]
    AudioSource AccIn, AccOut, FailureSound;


    public void Resize_MerAcc()
    {

        JsonIO.JsonTake();

        int Rosterindexnum = GlobalVariable.mercenaryDatas[IndexNum].IndexNum;

        Character Acc_Mer = new Character(JsonIO.save.Roster[Rosterindexnum].Job);
        Image SelectPortrai;

        SelectPortrai = PortaiObj.GetComponent<Image>();
        Character character = new Character(JsonIO.save.Roster[Rosterindexnum].Job);
        SelectPortrai.sprite = character.BasicSets[JsonIO.save.Roster[Rosterindexnum].Level].Portrait;

        health.maxValue = Acc_Mer.BasicSets[JsonIO.save.Roster[Rosterindexnum].Level].Health;
        health.value = JsonIO.save.Roster[Rosterindexnum].Health;
        morale.maxValue = Acc_Mer.BasicSets[JsonIO.save.Roster[Rosterindexnum].Level].Morale;
        morale.value = JsonIO.save.Roster[Rosterindexnum].Morale;
   
        
        LvText.text = "레벨 : " + JsonIO.save.Roster[Rosterindexnum].Level.ToString();
        NameText.text = "이름 : " + JsonIO.save.Roster[Rosterindexnum].Name;
        HealthText.text = JsonIO.save.Roster[Rosterindexnum].Health + "/" + Acc_Mer.BasicSets[JsonIO.save.Roster[Rosterindexnum].Level].Health.ToString();
        MoraleText.text = JsonIO.save.Roster[Rosterindexnum].Morale + "/ " + Acc_Mer.BasicSets[JsonIO.save.Roster[Rosterindexnum].Level].Morale.ToString();

        if (JsonIO.save.Roster[Rosterindexnum].State == "Relax")
        {
            RelaxFlag.SetActive(true);
            EndRelaxButton.SetActive(true);
            RelaxButton.SetActive(false);
        }
        else
        {
            RelaxButton.SetActive(true);
            RelaxFlag.SetActive(false);
            EndRelaxButton.SetActive(false);
            
        }
        JsonIO.JsonExport();
    }


    public void Relax()
    {
        JsonIO.JsonTake();

        IndexNum = transform.parent.GetComponent<MercenaryContoller_Acc>().IndexNum;
        int Rosterindexnum = GlobalVariable.mercenaryDatas[IndexNum].IndexNum;
        int tmpcount = 0;
        switch (JsonIO.save.AccommdationLV)
        {

            case 1:
                for (int i = 0; i < 2; i++)
                {
                    if (JsonIO.save.AccRoom[i] == 0)
                    {
                        transform.parent.GetComponent<MercenaryContoller_Acc>().AccIn.Play();
                      
                        JsonIO.save.AccRoom[i] = Rosterindexnum;
                        JsonIO.save.Roster[Rosterindexnum].State = "Relax";
                        RelaxFlag.SetActive(true);
                        EndRelaxButton.SetActive(true);
                        RelaxButton.SetActive(false);
                        tmpcount++;
                        break;
                    }
                }
                break;
            case 2:
                for (int i = 0; i < 4; i++)
                {
                    if (JsonIO.save.AccRoom[i] == 0)
                    {
                        transform.parent.GetComponent<MercenaryContoller_Acc>().AccIn.Play();
                        JsonIO.save.AccRoom[i] = Rosterindexnum;
                        JsonIO.save.Roster[Rosterindexnum].State = "Relax";
                        RelaxFlag.SetActive(true);
                        EndRelaxButton.SetActive(true);
                        RelaxButton.SetActive(false);
                        tmpcount++;
                        break;
                    }
                }
                break;
            case 3:
                for (int i = 0; i < 6; i++)
                {
                    if (JsonIO.save.AccRoom[i] == 0)
                    {
                        transform.parent.GetComponent<MercenaryContoller_Acc>().AccIn.Play();
                        JsonIO.save.AccRoom[i] = Rosterindexnum;
                        JsonIO.save.Roster[Rosterindexnum].State = "Relax";
                        RelaxFlag.SetActive(true);
                        EndRelaxButton.SetActive(true);
                        RelaxButton.SetActive(false);
                        tmpcount++;
                        break;
                    }
                }
               
                break;
        }
        if(tmpcount==0)
        {
            transform.parent.GetComponent<MercenaryContoller_Acc>().FailureSound.Play();

            Debug.Log("숙소꽉참");
        }

        
       // Acc.GetComponent<ScrollContoller_Acc>().SendMessage("Resize_AccRoom()");
        JsonIO.JsonExport();
        transform.parent.parent.parent.parent.parent.Find("Accommodation").SendMessage("Resize_AccRoom");
    }

    public void RelaxEnd()
    {
        transform.parent.GetComponent<MercenaryContoller_Acc>().AccOut.Play();

        JsonIO.JsonTake();
        
        IndexNum = transform.parent.GetComponent<MercenaryContoller_Acc>().IndexNum;
        int Rosterindexnum = GlobalVariable.mercenaryDatas[IndexNum].IndexNum;
        for (int i = 0; i < 6; i++)
        {
            if (JsonIO.save.AccRoom[i] == Rosterindexnum)
                JsonIO.save.AccRoom[i] = 0;
        }
        JsonIO.save.Roster[Rosterindexnum].State = "Stand";
        RelaxFlag.SetActive(false);
        EndRelaxButton.SetActive(false);
        RelaxButton.SetActive(true);
        JsonIO.JsonExport();
        //transform.parent.parent.parent.parent.parent.FindChild("Accommodation").GetComponent<MercenaryContoller_Acc>().SendMessage("Resize_AccRoom");
        transform.parent.parent.parent.parent.parent.Find("Accommodation").SendMessage("Resize_AccRoom");
    }
}
