using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using LitJson;

public class Expedition : MonoBehaviour
{
    public CharacterInfo characterInfo;

    [SerializeField]
    Canvas ExpeditionCanvas;

    [SerializeField]
    GameObject[] Expedition_Characters = new GameObject[5];

    // Start is called before the first frame update
    void Start()
    {
        ExpeditionCanvas.sortingOrder = -1;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickExpedition()
    {
        JsonIO.JsonTake();

        for (int i = Variable.ExpeditionSize - 1; i > 0; i--)
        {
            if (JsonIO.save.Expeditions[i] == 0)
            {
                transform.GetChild(0).GetChild(i - 1).gameObject.SetActive(true);
                Expedition_Characters[i].gameObject.SetActive(false);

            }
            else
            {
                transform.GetChild(0).GetChild(i - 1).gameObject.SetActive(false);
                Expedition_Characters[i].gameObject.SetActive(true);


                //소스 줄이기용
                Character cha = new Character(JsonIO.save.Roster[JsonIO.save.Expeditions[i]].Job);
                int lev = JsonIO.save.Roster[JsonIO.save.Expeditions[i]].Level;

                Expedition_Characters[i].transform.GetChild(0).GetComponent<Button>().image.sprite = cha.BasicSets[lev].Portrait_Horizontal;
                Expedition_Characters[i].transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "\'" + JsonIO.save.Roster[JsonIO.save.Expeditions[i]].Title + "\' " + JsonIO.save.Roster[JsonIO.save.Expeditions[i]].Name;
                Expedition_Characters[i].transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "Lv." + JsonIO.save.Roster[JsonIO.save.Expeditions[i]].Level.ToString() + " " + JsonIO.save.Roster[JsonIO.save.Expeditions[i]].Job;
            }

            



        }

        ExpeditionCanvas.sortingOrder = 1;

    }

    public void Swap_Expeditions(int Sour, int Des)
    {
        JsonIO.JsonTake();

        SaveData_Character temp = JsonIO.save.Roster[Sour];
        JsonIO.save.Roster[Sour] = JsonIO.save.Roster[Des];
        JsonIO.save.Roster[Des] = temp;

        JsonIO.JsonExport();

        ClickExpedition();
    }

    public void ClickExit()
    {
        ExpeditionCanvas.sortingOrder = -1;
    }
}
