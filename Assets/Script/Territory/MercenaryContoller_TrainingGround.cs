using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using LitJson;
public class MercenaryContoller_TrainingGround : MonoBehaviour
{
    [SerializeField]
    int IndexNum;
    [SerializeField]
    Text LvText,  ClsassNameText, NameText;
    [SerializeField]
    GameObject PortaiObj;
    [SerializeField]
    GameObject TrainingGround;
    public void Resize_TrainingGround()
    {

        JsonIO.JsonTake();

        int Rosterindexnum = GlobalVariable.mercenaryDatas[IndexNum].IndexNum;

        NameText.text = "이름 : " + JsonIO.save.Roster[Rosterindexnum].Name;
        LvText.text = "레벨 : " + JsonIO.save.Roster[Rosterindexnum].Level.ToString();
        ClsassNameText.text = "직업 : " + JsonIO.save.Roster[Rosterindexnum].Job;
        Image SelectPortrai;
        SelectPortrai = PortaiObj.GetComponent<Image>();
        Character character = new Character(JsonIO.save.Roster[Rosterindexnum].Job);
        SelectPortrai.sprite = character.BasicSets[JsonIO.save.Roster[Rosterindexnum].Level].Portrait;


    }


    public void Select()
    {
        JsonIO.JsonTake();
        IndexNum = transform.parent.GetComponent<MercenaryContoller_TrainingGround>().IndexNum;
        int Rosterindexnum = GlobalVariable.mercenaryDatas[IndexNum].IndexNum;
        TrainingGround.GetComponent<ScrollContoller_TrainingGround>().Resize_TrainingGround(Rosterindexnum, 1);
        JsonIO.JsonExport();
    }

}
