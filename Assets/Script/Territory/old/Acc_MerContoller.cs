using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using LitJson;

public class Acc_MerContoller : MonoBehaviour
{
    [SerializeField]
    Slider health;
    [SerializeField]
    Slider morale;
    [SerializeField]
    Text LvText, HealthText, MoraleText, NameText;
    [SerializeField]
    int StartSign;
    [SerializeField]
    Sprite[] Portrai = new Sprite[Variable.JobsMany];
    [SerializeField]
    GameObject RelaxButton;
    [SerializeField]
    GameObject EndRelaxButton;
    [SerializeField]
    GameObject RelaxFlag;
    [SerializeField]
    GameObject Mer_Acc;
    
    

    Image SelectPortrai;

    public int myindex;

    void Start()
    {
        JsonIO.JsonTake();
        if (StartSign == 100)
        {
           
            for (int i = 1; i < Variable.RosterSize; i++)
            {
                if (GlobalVariable.Acc_MersenaryIndexCheck[i] != -1)
                {
                    myindex = i;
                    break;
                }
            }
            Character Acc_Mer = new Character(JsonIO.save.Roster[myindex].Job);

            health.maxValue = Acc_Mer.BasicSets[JsonIO.save.Roster[myindex].Level].Health;
            health.value = JsonIO.save.Roster[myindex].Health;
            morale.maxValue = Acc_Mer.BasicSets[JsonIO.save.Roster[myindex].Level].Morale;
            morale.value = JsonIO.save.Roster[myindex].Morale;
            NameText.text = "이름 : " + JsonIO.save.Roster[myindex].Name;
            LvText.text = "레벨 : " + JsonIO.save.Roster[myindex].Level.ToString();
            HealthText.text = JsonIO.save.Roster[myindex].Health + "/" + Acc_Mer.BasicSets[JsonIO.save.Roster[myindex].Level].Health.ToString();
            MoraleText.text = JsonIO.save.Roster[myindex].Morale + "/ " + Acc_Mer.BasicSets[JsonIO.save.Roster[myindex].Level].Morale.ToString();
            if (JsonIO.save.Roster[myindex].State == "Relax")
            {
                RelaxFlag.SetActive(true);
                EndRelaxButton.SetActive(true);
                RelaxButton.SetActive(false);
            }



        }
        else if (StartSign == 90)
        {
            for (int i = 1; i < Variable.RosterSize; i++)
            {
                if (GlobalVariable.Acc_MersenaryIndexCheck[i] != -1)
                {
                    GlobalVariable.Acc_MersenaryIndexCheck[i] = -1;
                    myindex = i;
                    break;
                }
            }
            SelectPortrai = GetComponent<Image>();

            SelectPortrai.sprite = Portrai[JsonIO.save.Roster[myindex].Number];
         

        }
        JsonIO.JsonExport();
    }

    public void Relax()
    {
        JsonIO.JsonTake();
        Acc_MerContoller acc_MerContoller;
        acc_MerContoller = Mer_Acc.GetComponent<Acc_MerContoller>();
        myindex = acc_MerContoller.myindex;
        Debug.Log("릴렉스" + myindex+"숙소레벨:"+ JsonIO.save.AccommdationLV);
       
        switch (JsonIO.save.AccommdationLV)
        {
            case 1:
                for(int i=0;i<2;i++)
                {
                    if(JsonIO.save.AccRoom[i]==0)
                    {
                        JsonIO.save.AccRoom[i] = myindex;
                        JsonIO.save.Roster[myindex].State = "Relax";
                        RelaxFlag.SetActive(true);
                        EndRelaxButton.SetActive(true);
                        RelaxButton.SetActive(false);
                        break;
                    }
                }
                Debug.Log("숙소꽉참");
                break;
            case 2:
                for (int i = 0; i < 4; i++)
                {
                    if (JsonIO.save.AccRoom[i] == 0)
                    {
                        JsonIO.save.AccRoom[i] = myindex;
                        JsonIO.save.Roster[myindex].State = "Relax";
                        RelaxFlag.SetActive(true);
                        EndRelaxButton.SetActive(true);
                        RelaxButton.SetActive(false);

                        break;
                    }
                }
                Debug.Log("숙소꽉참");
                break;
            case 3:
                for (int i = 0; i < 6; i++)
                {
                    if (JsonIO.save.AccRoom[i] == 0)
                    {
                        JsonIO.save.AccRoom[i] = myindex;
                        JsonIO.save.Roster[myindex].State = "Relax";
                        RelaxFlag.SetActive(true);
                        EndRelaxButton.SetActive(true);
                        RelaxButton.SetActive(false);
                       
                        break;
                    }
                }
                Debug.Log("숙소꽉참");
                break;
        }

       
        JsonIO.JsonExport();
    }
    public void EndRelax()
    {
        JsonIO.JsonTake();

        Acc_MerContoller acc_MerContoller;
        acc_MerContoller = Mer_Acc.GetComponent<Acc_MerContoller>();
        myindex = acc_MerContoller.myindex;


        Debug.Log("끝" + myindex + "숙소레벨:" + JsonIO.save.AccommdationLV);

        JsonIO.save.Roster[myindex].State = "Stand";
        for(int i=0;i<Variable.RosterSize;i++)
        {
            if (JsonIO.save.AccRoom[i] == myindex)
            {
                JsonIO.save.AccRoom[i] = 0;
                break;
            }

        }
        EndRelaxButton.SetActive(false);
        RelaxButton.SetActive(true);
        RelaxFlag.SetActive(false);

        JsonIO.JsonExport();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
