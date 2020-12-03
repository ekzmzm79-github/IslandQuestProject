using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using LitJson;


public class ScrollContoller_TrainingGround : MonoBehaviour
{
    [SerializeField]
    Text SkillNameText, SkillTypeText, SkillExplainText, SkillDMG, SkillDuration, SkillCoefficient;
    [SerializeField]
    Image[] SkillIMG = new Image[10];
    [SerializeField]
    int StartSign;
    [SerializeField]
    AudioSource SusuccessSound, FailureSound;
    public int GetRosterindexnum, NowSkill;
    // Start is called before the first frame update
    void Start()
    {
        if(StartSign==100)
        {
            Resize_TrainingGround(1, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Resize_TrainingGround(int Rosterindexnum, int SkillNum)
    {
        NowSkill = SkillNum;
        GetRosterindexnum = Rosterindexnum;
        JsonIO.JsonTake();
        SaveData_Character tmp = JsonIO.save.Roster[GetRosterindexnum];
        Character character = new Character(tmp.Job);
        int skillLVPLUS = tmp.Skill_Levels[SkillNum] + 1;
        if (tmp.Skill_Levels[SkillNum] == 5)
            skillLVPLUS--;
        SkillNameText.text = character.SkillsInfo[SkillNum, tmp.Skill_Levels[SkillNum]].Name;
        SkillTypeText.text = character.SkillsInfo[SkillNum, tmp.Skill_Levels[SkillNum]].Type;
        SkillExplainText.text = character.SkillsInfo[SkillNum, tmp.Skill_Levels[SkillNum]].ExplainText;
        SkillDMG.text = "DMG :"+character.SkillsInfo[SkillNum, tmp.Skill_Levels[SkillNum]].Damage+"->"+ character.SkillsInfo[SkillNum, skillLVPLUS].Damage;
        SkillDuration.text = "Duration :" + character.SkillsInfo[SkillNum, tmp.Skill_Levels[SkillNum]].Duration + "->" + character.SkillsInfo[SkillNum, skillLVPLUS].Duration;
        SkillCoefficient.text = "Coefficient :" + character.SkillsInfo[SkillNum, tmp.Skill_Levels[SkillNum]].Coefficient + "->" + character.SkillsInfo[SkillNum, skillLVPLUS].Coefficient;
        for(int i=1;i<3 ;i++ )
        {
            SkillIMG[i].sprite = character.SkillsInfo[i,1].Skill_Img;
        }
        for (int i = 7; i < 8; i++)
        {
            SkillIMG[i].sprite = character.SkillsInfo[i, 1].Skill_Img;
        }
    }

    public void Resize_Skill(int SkillNum)
    {
        NowSkill = SkillNum;
        JsonIO.JsonTake();
        SaveData_Character tmp = JsonIO.save.Roster[GetRosterindexnum];
        Character character = new Character(tmp.Job);
        int skillLVPLUS = tmp.Skill_Levels[SkillNum] + 1;
        if (tmp.Skill_Levels[SkillNum] == 5)
            skillLVPLUS--;
        SkillNameText.text = character.SkillsInfo[SkillNum, tmp.Skill_Levels[SkillNum]].Name;
        SkillTypeText.text = character.SkillsInfo[SkillNum, tmp.Skill_Levels[SkillNum]].Type;
        SkillExplainText.text = character.SkillsInfo[SkillNum, tmp.Skill_Levels[SkillNum]].ExplainText;
        SkillDMG.text = "DMG :" + character.SkillsInfo[SkillNum, tmp.Skill_Levels[SkillNum]].Damage + "->" + character.SkillsInfo[SkillNum, skillLVPLUS].Damage;
        SkillDuration.text = "Duration :" + character.SkillsInfo[SkillNum, tmp.Skill_Levels[SkillNum]].Duration + "->" + character.SkillsInfo[SkillNum, skillLVPLUS].Duration;
        SkillCoefficient.text = "Coefficient :" + character.SkillsInfo[SkillNum, tmp.Skill_Levels[SkillNum]].Coefficient + "->" + character.SkillsInfo[SkillNum, skillLVPLUS].Coefficient;
   
    }
    public void Skill_Upgrade()
    {
       
        JsonIO.JsonTake();
        int Rosterindexnum, SkillNum;
        Rosterindexnum = transform.parent.parent.GetComponent<ScrollContoller_TrainingGround>().GetRosterindexnum;
        SkillNum = transform.parent.parent.GetComponent<ScrollContoller_TrainingGround>().NowSkill;
        SaveData_Character tmp = JsonIO.save.Roster[Rosterindexnum];
        Character character = new Character(tmp.Job);

        if (tmp.Skill_Levels[SkillNum] > 4)
        {
            FailureSound.Play();
            Debug.Log("스킬이 이미 최고레벨 입니다.");
        }
        else if (JsonIO.save.Money > tmp.Skill_Levels[SkillNum] * 100)
        {
            JsonIO.save.Money -= (tmp.Skill_Levels[SkillNum] * 100);
            tmp.Skill_Levels[SkillNum]++;
            JsonIO.JsonExport();
            transform.parent.parent.GetComponent<ScrollContoller_TrainingGround>().Resize_Skill(SkillNum);
            SusuccessSound.Play();
            Debug.Log("스킬강화성공");
        }
        else
        {
            FailureSound.Play();
            Debug.Log("재화 및 재료가 부족합니다.");
        }
    }
}
