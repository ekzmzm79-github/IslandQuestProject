using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using LitJson;

public class Expedition_Setting_Controller : MonoBehaviour
{

    [SerializeField]
    GameObject[] Expeditions = new GameObject[5];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Expedition_Setting()
    {
        JsonIO.JsonTake();


        //원정대 목록을 세팅함

        for (int i = Variable.ExpeditionSize - 1; i > 0; i--)
        {
            

            if (JsonIO.save.Expeditions[i] == 0)
                continue;

            //소스 줄이기용
            Character cha = new Character(JsonIO.save.Roster[JsonIO.save.Expeditions[i]].Job);
            int lev = JsonIO.save.Roster[JsonIO.save.Expeditions[i]].Level;

            Expeditions[i].gameObject.SetActive(true);
            Expeditions[i].transform.GetChild(0).GetComponent<Button>().image.sprite = cha.BasicSets[lev].Portrait_Vertical;
            Expeditions[i].transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "\'" + JsonIO.save.Roster[JsonIO.save.Expeditions[i]].Title + "\'" + " " + JsonIO.save.Roster[JsonIO.save.Expeditions[i]].Name;
            Expeditions[i].transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "Lv." + JsonIO.save.Roster[JsonIO.save.Expeditions[i]].Level.ToString() + " " + JsonIO.save.Roster[JsonIO.save.Expeditions[i]].Job;

            transform.GetChild(0).GetChild(i-1).gameObject.SetActive(false);
            

        }
    }

    public void Reset_ExpeditionList()
    {
        JsonIO.JsonTake();

        JsonIO.save.Now_ExpeditionMany = 0;

        for (int i = 1; i < Variable.ExpeditionSize; i++)
        {
            Expeditions[i].SetActive(false);
            transform.GetChild(0).GetChild(i - 1).gameObject.SetActive(true);
            JsonIO.save.Expeditions[i] = 0;
        }

        JsonIO.JsonExport();
        

    }

    public void Swap_Expeditions(int Sour, int Des)
    {
        
        JsonIO.JsonTake();

        int temp = JsonIO.save.Expeditions[Sour];
        JsonIO.save.Expeditions[Sour] = JsonIO.save.Expeditions[Des];
        JsonIO.save.Expeditions[Des] = temp;

        JsonIO.JsonExport();

        //빈칸과 스왑했다.
        if (JsonIO.save.Expeditions[Sour] == 0)
        {

            Debug.Log(Sour + "   " + Des);

            //Expeditions[Des].gameObject.SetActive(true);
            Expeditions[Sour].gameObject.SetActive(false);

            //transform.GetChild(0).GetChild(Des - 1).gameObject.SetActive(false);
            transform.GetChild(0).GetChild(Sour - 1).gameObject.SetActive(true);

            
        }
        else
        {
            
        }

        Expedition_Setting();

    }


}
