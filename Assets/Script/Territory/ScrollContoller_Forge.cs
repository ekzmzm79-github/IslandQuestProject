using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using LitJson;

public class ScrollContoller_Forge : MonoBehaviour
{
    [SerializeField]
    Text explanationText, materials1Text, materials2Text, materials3Text, reinforceCostText, AttackText, AccuracyText, SpeedText, ArmorText, AvoidanceText;
    [SerializeField]
    Image nextWeapon, nowWepon, materials1, materials2, materials3;//강화재료 이미지 아직 추가안햇으,ㅁ
    [SerializeField]
    int StartSign;
    [SerializeField]
    AudioSource SusuccessSound, FailureSound;
    public int GetRosterindexnum;
    // Start is called before the first frame update
    void Start()
    {
        if(StartSign==100)
        {
            if (GlobalVariable.mercenaryDatas[0].IndexNum == 0)//고용된 영웅이 하나도 없을때 다른 좋은방법 생각해내기
                return;
            else
                Resize_Forge(GlobalVariable.mercenaryDatas[0].IndexNum);
        }
       
       
    }

    // Update is called once per frame
    public void Resize_Forge(int Rosterindexnum)
    {
        GetRosterindexnum = Rosterindexnum;
        JsonIO.JsonTake();
        SaveData_Character tmp = JsonIO.save.Roster[Rosterindexnum];
        Character character = new Character(tmp.Job);

        reinforceCostText.text = (tmp.EquipLevel * 300).ToString();//최고레벨일때 뜰 문구 생각해두기
        materials1Text.text = (tmp.EquipLevel -1).ToString();//나중에 재료 정해지면 수정
        materials2Text.text = (tmp.EquipLevel -1).ToString();
        materials3Text.text = (tmp.EquipLevel -1).ToString();
        int EquipLevelPlus= tmp.EquipLevel+1;
        if (tmp.EquipLevel == 4)
            EquipLevelPlus--;
        explanationText.text = character.EquipmentInfo[tmp.EquipLevel].ExplainText;
        AttackText.text = "Attack" + character.EquipmentInfo[tmp.EquipLevel].Attack + "->" + character.EquipmentInfo[EquipLevelPlus].Attack;
        AccuracyText.text = "Accuracy" + character.EquipmentInfo[tmp.EquipLevel].Accuracy + "->" + character.EquipmentInfo[EquipLevelPlus].Accuracy;
        SpeedText.text = "Speed" + character.EquipmentInfo[tmp.EquipLevel].Speed + "->" + character.EquipmentInfo[EquipLevelPlus].Speed;
        ArmorText.text = "Armor" + character.EquipmentInfo[tmp.EquipLevel].Armor + "->" + character.EquipmentInfo[EquipLevelPlus].Armor;
        AvoidanceText.text = "Avoidance" + character.EquipmentInfo[tmp.EquipLevel].Avoidance + "->" + character.EquipmentInfo[EquipLevelPlus].Avoidance;
        nowWepon.sprite = character.EquipmentInfo[tmp.EquipLevel].Equip_Img;
        nextWeapon.sprite = character.EquipmentInfo[EquipLevelPlus].Equip_Img;
       
    }
    public void Reinforce()//강화하기
    {
        JsonIO.JsonTake();
        int Rosterindexnum;
        Rosterindexnum = transform.parent.GetComponent<ScrollContoller_Forge>().GetRosterindexnum;

        SaveData_Character tmp = JsonIO.save.Roster[Rosterindexnum];
        Character character = new Character(tmp.Job);
        
        if(tmp.EquipLevel > 3)
        {
            FailureSound.Play();
            Debug.Log("장비가 이미 최고레벨 입니다.");
        }
        else if (JsonIO.save.Money> tmp.EquipLevel * 300 )
        {
            JsonIO.save.Money -= (tmp.EquipLevel * 300);
            tmp.EquipLevel++;
            JsonIO.JsonExport();
            transform.parent.GetComponent<ScrollContoller_Forge>().Resize_Forge(Rosterindexnum);
            SusuccessSound.Play();
            Debug.Log("강화성공");
        }
        else
        {
            FailureSound.Play();
            Debug.Log("재화 및 재료가 부족합니다.");
        }
   

    }
}
