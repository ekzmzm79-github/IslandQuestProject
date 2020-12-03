using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using LitJson;

public class RosterScroll : MonoBehaviour
{
    public Expedition_Setting_Controller expedition_Setting_Controller;

    [SerializeField]
    GameObject Roster_EachChar_Prefab;
    [SerializeField]
    ScrollRect scrollRect;
    [SerializeField]
    GameObject Content;

    ArrayList RosterList = new ArrayList();



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }



    public void RosterScroll_Setting()
    {
        Delete_List();

        JsonIO.JsonTake();

        float ini_x = Content.transform.localPosition.x + 710f, ini_y = Content.transform.localPosition.y;

        float nwidth = 0, nheight = 0;

        nwidth = Roster_EachChar_Prefab.GetComponent<RectTransform>().rect.width;

        int Roster_Index = 1;

        for (int i = 1, k = 1; i < Variable.RosterSize && k < Variable.RosterSize; i++, k++)
        {

            if (JsonIO.save.Roster[k] == null || JsonIO.save.Roster[k].Number == 0)
            {
                i--;
                continue;
            }


            //스크롤 컨텐츠 사이즈 세팅
            nheight += Roster_EachChar_Prefab.GetComponent<RectTransform>().rect.height;

            //Instantiate
            GameObject temp = Instantiate(Roster_EachChar_Prefab) as GameObject;
            temp.transform.SetParent(Content.transform, true);
            temp.transform.localPosition = new Vector2(ini_x, ini_y - ((i - 1) * Roster_EachChar_Prefab.GetComponent<RectTransform>().rect.height));

            //소스 줄이기용
            Character cha = new Character(JsonIO.save.Roster[k].Job);
            int lev = JsonIO.save.Roster[k].Level;

            temp.transform.GetChild(0).GetComponent<Button>().image.sprite = cha.BasicSets[lev].Portrait_Horizontal;
            temp.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "\'" + JsonIO.save.Roster[k].Title + "\'";
            temp.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = JsonIO.save.Roster[k].Name;
            temp.transform.GetChild(1).GetChild(2).GetComponent<Text>().text = "Lv. " + JsonIO.save.Roster[k].Level.ToString();
            temp.transform.GetChild(1).GetChild(3).GetComponent<Text>().text = JsonIO.save.Roster[k].Job;

            temp.transform.GetChild(4).GetComponent<Text>().text = k.ToString();
            temp.transform.name = "Roster_EachChar" + Roster_Index++;

            //이미 원정대인 캐릭터는 블라인드 처리
            for (int j = 1; j < Variable.ExpeditionSize; j++)
            {
                if (k == JsonIO.save.Expeditions[j])
                {
                    temp.transform.GetChild(3).gameObject.SetActive(true);
                    temp.transform.GetComponent<Each_Mercenary>().enabled = false;
                    temp.transform.GetComponent<BoxCollider2D>().enabled = false;
                    break;
                }
            }

            RosterList.Add(temp);



        }

        scrollRect.content.sizeDelta = new Vector2(nwidth, nheight);


    }


    public void Delete_List()
    {
        for (int i = 0; i < RosterList.Count; i++)
        {

            Destroy((GameObject)RosterList[i]);
        }

        RosterList.Clear();
    }

    public void Swap_List(int Sour, int Des)
    {
        //Debug.Log("스왑 실행함 (로스터)" + Sour +" , "+ Des);

        JsonIO.JsonTake();

        SaveData_Character temp = JsonIO.save.Roster[Sour];
        JsonIO.save.Roster[Sour] = JsonIO.save.Roster[Des];
        JsonIO.save.Roster[Des] = temp;

        JsonIO.JsonExport();

        RosterScroll_Setting();
    }

}