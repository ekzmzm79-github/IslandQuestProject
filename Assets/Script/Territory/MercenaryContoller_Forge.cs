using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using LitJson;

public class MercenaryContoller_Forge : MonoBehaviour
{
    [SerializeField]
    int IndexNum;
    [SerializeField]
    Text LvText, EquipLeveltText, ClsassNameText, NameText;
    [SerializeField]
    GameObject PortaiObj;
    [SerializeField]
    GameObject Forge;
    public void Resize_MerForge()
    {

        JsonIO.JsonTake();

        int Rosterindexnum = GlobalVariable.mercenaryDatas[IndexNum].IndexNum;

        NameText.text = "이름 : " + JsonIO.save.Roster[Rosterindexnum].Name;
        LvText.text = "레벨 : " + JsonIO.save.Roster[Rosterindexnum].Level.ToString();
        ClsassNameText.text = "직업 : " + JsonIO.save.Roster[Rosterindexnum].Job;
        EquipLeveltText.text = "장비레벨 : " + JsonIO.save.Roster[Rosterindexnum].EquipLevel.ToString();
        Image SelectPortrai;
        SelectPortrai = PortaiObj.GetComponent<Image>();
        Character character = new Character(JsonIO.save.Roster[Rosterindexnum].Job);
        SelectPortrai.sprite = character.BasicSets[JsonIO.save.Roster[Rosterindexnum].Level].Portrait;


    }


    public void Select()
    {
        JsonIO.JsonTake();
        IndexNum = transform.parent.GetComponent<MercenaryContoller_Forge>().IndexNum;
        int Rosterindexnum = GlobalVariable.mercenaryDatas[IndexNum].IndexNum;
        Forge.SendMessage("Resize_Forge", Rosterindexnum);
        JsonIO.JsonExport();
    }
   
}
