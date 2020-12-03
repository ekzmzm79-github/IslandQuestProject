using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using LitJson;

public class MercenaryContoller_Guild : MonoBehaviour
{
    [SerializeField]
    int IndexNum;
    [SerializeField]
    Text LvText, CostText, ClsassNameText, NameText;
    [SerializeField]
    GameObject PortaiObj;

    public void Resize_MerGuild()
    {
        
        JsonIO.JsonTake();

        int Rosterindexnum = GlobalVariable.mercenaryDatas[IndexNum].IndexNum;
        LvText.text = "레벨 : " + JsonIO.save.Roster[Rosterindexnum].Level.ToString();
        CostText.text = "비용 : " + (JsonIO.save.Roster[Rosterindexnum].Level * 100).ToString();
        ClsassNameText.text = "직업 : " + JsonIO.save.Roster[Rosterindexnum].Job;
        NameText.text = "이름 : " + JsonIO.save.Roster[Rosterindexnum].Name;

        Image SelectPortrai;
        SelectPortrai = PortaiObj.GetComponent<Image>();
        Character character = new Character(JsonIO.save.Roster[Rosterindexnum].Job);
        SelectPortrai.sprite = character.BasicSets[JsonIO.save.Roster[Rosterindexnum].Level].Portrait;

    }


    public void Fire()
    {
        JsonIO.JsonTake();
        IndexNum = transform.parent.GetComponent<MercenaryContoller_Guild>().IndexNum;
        int Rosterindexnum = GlobalVariable.mercenaryDatas[IndexNum].IndexNum;
        JsonIO.save.Roster[Rosterindexnum].Number=0;
        for(int i=0;i<7;i++)//휴식중이라면 초기화
        {
            if (JsonIO.save.AccRoom[i] == Rosterindexnum)
                JsonIO.save.AccRoom[i] = 0;
        }
        JsonIO.JsonExport();
        GameObject.Find("MercenaryScroll_Guild").SendMessage("Resize_GuildLeftScroll");
    }
}
