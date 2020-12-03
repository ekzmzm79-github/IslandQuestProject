using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using LitJson;

public class DataLoader : MonoBehaviour
{
    [SerializeField]
    Text Day;

    [SerializeField]
    FadeIn fadeIn;

    void Awake()
    {
        Screen.SetResolution(1920, 1080, true);
    }

    // Start is called before the first frame update
    void Start()
    {

        JsonIO.JsonTake();

        Day.text = "Day " + JsonIO.save.Day.ToString();

        fadeIn.TriggerOn();//시작할때 페이드인
        // JsonIO.ResetSave();
        //  JsonIO.JsonExport();
        return;

        JsonIO.save.Day = 2;

        //스테이지 정보

        JsonIO.save.StageProg[1] = 2;
        JsonIO.save.StageProg[2] = 8;
        JsonIO.save.StageProg[3] = 0;

        //
        //1번 크루세이더

        JsonIO.save.Roster[1] = new SaveData_Character();


        JsonIO.save.Roster[1].Name = "Lost";
        JsonIO.save.Roster[1].State = "Sel";

        JsonIO.save.Roster[1].Job = "Cru";
        JsonIO.save.Roster[1].Title = "HolyKnight";

        JsonIO.save.Roster[1].Level = 2;
        JsonIO.save.Roster[1].Number = 1;

        JsonIO.save.Roster[1].Health = 10;
        JsonIO.save.Roster[1].Morale = 10;


        JsonIO.save.Roster[1].Skill_Levels[1] = 1;
        JsonIO.save.Roster[1].Skill_Levels[2] = 2;
        JsonIO.save.Roster[1].Skill_Levels[7] = 1;

        JsonIO.save.Roster[1].EquipLevel = 2;
        JsonIO.save.Roster[1].AccessoryIndex = 1;
        JsonIO.save.Expeditions[1] = 1;
        //

        //2번 해적

        JsonIO.save.Roster[2] = new SaveData_Character();

        JsonIO.save.Roster[2].Name = "Remain";
        JsonIO.save.Roster[2].State = "Sel";

        JsonIO.save.Roster[2].Job = "Pir";
        JsonIO.save.Roster[2].Title = "Bastard";

        JsonIO.save.Roster[2].Level = 1;
        JsonIO.save.Roster[2].Number = 2;

        JsonIO.save.Roster[2].Health = 5;
        JsonIO.save.Roster[2].Morale = 5;

        JsonIO.save.Roster[2].Skill_Levels[1] = 1;
        JsonIO.save.Roster[2].Skill_Levels[2] = 2;
        JsonIO.save.Roster[2].Skill_Levels[7] = 1;

        JsonIO.save.Roster[2].EquipLevel = 1;

        JsonIO.save.Expeditions[2] = 2;
        //

        JsonIO.save.Now_ExpeditionMany = 2;

        JsonIO.JsonExport();


    }

    // Update is called once per frame
    void Update()
    {

    }


}

public static class Variable
{
    //배열의 0번째 인덱스는 사용하지 않으므로 실제 사이즈 +1로 할당해야함.
    public static int JobsMany = 5;
    public static int SkillMany = 10; //액티브 6개 + 패시브 3개 : 인덱스 1~6까지는 액티브, 인덱스 7~9까지는 패시브

    public static int StageSize = 15;
    public static int RosterSize = 5;

    public static int ExpeditionSize = 5;

    public static int JobsMaxLevel = 6;
    public static int SkillMaxLevel = 6;
    public static int EquipMaxLevel = 5;

    public static int StageMany = 6;
    public static int StageEnemyMany = 11;
    public static int Each_StageEnemyMax = 5;

    public static int EnemyLevelMax = 6;
    public static int EnemySkillMany = 5;

    public static int SkillScopeMax = 9;

}

//악세사리, 타이틀 정보는 따로 

public class Character //캐릭터의 정보를 저장해두는 클래스, 직업별로 분류 뒤에 레벨별로 분류된다.
{
    //캐릭터 기본 정보
    public Character_BasicSet[] BasicSets = new Character_BasicSet[Variable.JobsMaxLevel];

    //첫번째 스킬 종류(인덱스 1~6까지는 액티브, 인덱스 7~9까지는 패시브), 두번째 인덱스는 스킬 레벨
    public Character_SkillSet[,] SkillsInfo = new Character_SkillSet[Variable.SkillMany, Variable.SkillMaxLevel];

    //캐릭터 장비 정보
    public Character_EquipSet[] EquipmentInfo = new Character_EquipSet[Variable.EquipMaxLevel];


    public Character()
    {
        for (int i = 1; i < Variable.JobsMaxLevel; i++)
            BasicSets[i] = new Character_BasicSet();
        for (int i = 1; i < Variable.SkillMany; i++)
            for (int j = 1; j < Variable.SkillMaxLevel; j++)
                SkillsInfo[i, j] = new Character_SkillSet();
        for (int i = 1; i < Variable.EquipMaxLevel; i++)
            EquipmentInfo[i] = new Character_EquipSet();
    }

    public Character(string job)
    {
        for (int i = 1; i < Variable.JobsMaxLevel; i++)
            BasicSets[i] = new Character_BasicSet();
        for (int i = 1; i < Variable.SkillMany; i++)
            for (int j = 1; j < Variable.SkillMaxLevel; j++)
                SkillsInfo[i, j] = new Character_SkillSet();
        for (int i = 1; i < Variable.EquipMaxLevel; i++)
            EquipmentInfo[i] = new Character_EquipSet();

        switch (job)
        {


            case "Cru":
                //start - 크루세이더 기본 세팅

                BasicSets[1].Portrait = Resources.Load<Sprite>("Characters/CruPortrait");
                BasicSets[1].Portrait_Vertical = Resources.Load<Sprite>("Characters/CruPortrait_Vertical");
                BasicSets[1].Portrait_Horizontal = Resources.Load<Sprite>("Characters/CruPortrait_Horizontal");
                BasicSets[1].CharacterIntroText = "크루세이더 소개에 대한 텍스트 입니다. 크루세이더는 전방에서~";
                BasicSets[1].Health = 110;
                BasicSets[1].Morale = 110;
                BasicSets[1].Will = 21.1;

                BasicSets[2].Portrait = Resources.Load<Sprite>("Characters/CruPortrait");
                BasicSets[2].Portrait_Vertical = Resources.Load<Sprite>("Characters/CruPortrait_Vertical");
                BasicSets[2].Portrait_Horizontal = Resources.Load<Sprite>("Characters/CruPortrait_Horizontal");
                BasicSets[2].CharacterIntroText = "크루세이더 소개에 대한 텍스트 입니다. 크루세이더는 전방에서~";
                BasicSets[2].Health = 120;
                BasicSets[2].Morale = 120;
                BasicSets[2].Will = 22.1;

                BasicSets[3].Portrait = Resources.Load<Sprite>("Characters/CruPortrait");
                BasicSets[3].Portrait_Vertical = Resources.Load<Sprite>("Characters/CruPortrait_Vertical");
                BasicSets[3].Portrait_Horizontal = Resources.Load<Sprite>("Characters/CruPortrait_Horizontal");
                BasicSets[3].CharacterIntroText = "크루세이더 소개에 대한 텍스트 입니다. 크루세이더는 전방에서~";
                BasicSets[3].Health = 130;
                BasicSets[3].Morale = 130;
                BasicSets[3].Will = 23.1;

                BasicSets[4].Portrait = Resources.Load<Sprite>("Characters/CruPortrait");
                BasicSets[4].Portrait_Vertical = Resources.Load<Sprite>("Characters/CruPortrait_Vertical");
                BasicSets[4].Portrait_Horizontal = Resources.Load<Sprite>("Characters/CruPortrait_Horizontal");
                BasicSets[4].CharacterIntroText = "크루세이더 소개에 대한 텍스트 입니다. 크루세이더는 전방에서~";
                BasicSets[4].Health = 140;
                BasicSets[4].Morale = 140;
                BasicSets[4].Will = 24.1;

                BasicSets[5].Portrait = Resources.Load<Sprite>("Characters/CruPortrait");
                BasicSets[5].Portrait_Vertical = Resources.Load<Sprite>("Characters/CruPortrait_Vertical");
                BasicSets[5].Portrait_Horizontal = Resources.Load<Sprite>("Characters/CruPortrait_Horizontal");
                BasicSets[5].CharacterIntroText = "크루세이더 소개에 대한 텍스트 입니다. 크루세이더는 전방에서~";
                BasicSets[5].Health = 150;
                BasicSets[5].Morale = 150;
                BasicSets[5].Will = 25.1;

                //end - 크루세이더 기본 세팅
                //start - 크루세이더 스킬 세팅

                SkillsInfo[1, 1].Skill_Img = Resources.Load<Sprite>("Characters/CruActiveSkill1");
                SkillsInfo[1, 1].Name = "크루세이더 액티브 스킬 1 레벨 1";
                SkillsInfo[1, 1].Type = "Active";
                SkillsInfo[1, 1].ExplainText = "크루세이더 액티브 스킬 1의 레벨 1에 대한 설명입니다.";
                SkillsInfo[1, 1].Damage = 10.0;
                SkillsInfo[1, 1].Duration = 3;
                SkillsInfo[1, 1].Coefficient = 1.2;
                SkillsInfo[1, 1].Scope[1] = 1;
                SkillsInfo[1, 1].Scope[3] = 1;
                SkillsInfo[1, 1].Scope[5] = 1;
                SkillsInfo[1, 1].Scope[6] = 1;

                SkillsInfo[1, 2].Skill_Img = Resources.Load<Sprite>("Characters/CruActiveSkill1");
                SkillsInfo[1, 2].Name = "크루세이더 액티브 스킬 1 레벨 2";
                SkillsInfo[1, 2].Type = "Active";
                SkillsInfo[1, 2].ExplainText = "크루세이더 액티브 스킬 1의 레벨 2에 대한 설명입니다.";
                SkillsInfo[1, 2].Damage = 20.0;
                SkillsInfo[1, 2].Duration = 3;
                SkillsInfo[1, 2].Coefficient = 1.2;
                SkillsInfo[1, 2].Scope[1] = 1;
                SkillsInfo[1, 2].Scope[3] = 1;
                SkillsInfo[1, 2].Scope[5] = 1;
                SkillsInfo[1, 2].Scope[6] = 1;

                SkillsInfo[1, 3].Skill_Img = Resources.Load<Sprite>("Characters/CruActiveSkill1");
                SkillsInfo[1, 3].Name = "크루세이더 액티브 스킬 1 레벨 3";
                SkillsInfo[1, 3].Type = "Active";
                SkillsInfo[1, 3].ExplainText = "크루세이더 액티브 스킬 1의 레벨 3에 대한 설명입니다.";
                SkillsInfo[1, 3].Damage = 30.0;
                SkillsInfo[1, 3].Duration = 3;
                SkillsInfo[1, 3].Coefficient = 1.2;
                SkillsInfo[1, 3].Scope[1] = 1;
                SkillsInfo[1, 3].Scope[3] = 1;
                SkillsInfo[1, 3].Scope[5] = 1;
                SkillsInfo[1, 3].Scope[6] = 1;

                SkillsInfo[1, 4].Skill_Img = Resources.Load<Sprite>("Characters/CruActiveSkill1");
                SkillsInfo[1, 4].Name = "크루세이더 액티브 스킬 1 레벨 4";
                SkillsInfo[1, 4].Type = "Active";
                SkillsInfo[1, 4].ExplainText = "크루세이더 액티브 스킬 1의 레벨 4에 대한 설명입니다.";
                SkillsInfo[1, 4].Damage = 40.0;
                SkillsInfo[1, 4].Duration = 3;
                SkillsInfo[1, 4].Coefficient = 1.2;
                SkillsInfo[1, 4].Scope[1] = 1;
                SkillsInfo[1, 4].Scope[3] = 1;
                SkillsInfo[1, 4].Scope[5] = 1;
                SkillsInfo[1, 4].Scope[6] = 1;

                SkillsInfo[1, 5].Skill_Img = Resources.Load<Sprite>("Characters/CruActiveSkill1");
                SkillsInfo[1, 5].Name = "크루세이더 액티브 스킬 1 레벨 5";
                SkillsInfo[1, 5].Type = "Active";
                SkillsInfo[1, 5].ExplainText = "크루세이더 액티브 스킬 1의 레벨 5에 대한 설명입니다.";
                SkillsInfo[1, 5].Damage = 50.0;
                SkillsInfo[1, 5].Duration = 3;
                SkillsInfo[1, 5].Coefficient = 1.2;
                SkillsInfo[1, 5].Scope[1] = 1;
                SkillsInfo[1, 5].Scope[3] = 1;
                SkillsInfo[1, 5].Scope[5] = 1;
                SkillsInfo[1, 5].Scope[6] = 1;

                SkillsInfo[2, 1].Skill_Img = Resources.Load<Sprite>("Characters/CruActiveSkill2");
                SkillsInfo[2, 1].Name = "크루세이더 액티브 스킬 2 레벨 1";
                SkillsInfo[2, 1].Type = "Active";
                SkillsInfo[2, 1].ExplainText = "크루세이더 액티브 스킬 2의 레벨 1에 대한 설명입니다.";
                SkillsInfo[2, 1].Damage = 5.0;
                SkillsInfo[2, 1].Duration = 5;
                SkillsInfo[2, 1].Coefficient = 2.2;
                SkillsInfo[2, 1].Scope[5] = 1;

                SkillsInfo[2, 2].Skill_Img = Resources.Load<Sprite>("Characters/CruActiveSkill2");
                SkillsInfo[2, 2].Name = "크루세이더 액티브 스킬 2 레벨 2";
                SkillsInfo[2, 2].Type = "Active";
                SkillsInfo[2, 2].ExplainText = "크루세이더 액티브 스킬 2의 레벨 2에 대한 설명입니다.";
                SkillsInfo[2, 2].Damage = 6.0;
                SkillsInfo[2, 2].Duration = 5;
                SkillsInfo[2, 2].Coefficient = 2.2;
                SkillsInfo[2, 2].Scope[5] = 1;

                SkillsInfo[2, 3].Skill_Img = Resources.Load<Sprite>("Characters/CruActiveSkill2");
                SkillsInfo[2, 3].Name = "크루세이더 액티브 스킬 2 레벨 3";
                SkillsInfo[2, 3].Type = "Active";
                SkillsInfo[2, 3].ExplainText = "크루세이더 액티브 스킬 2의 레벨 3에 대한 설명입니다.";
                SkillsInfo[2, 3].Damage = 7.0;
                SkillsInfo[2, 3].Duration = 6;
                SkillsInfo[2, 3].Coefficient = 2.2;
                SkillsInfo[2, 3].Scope[5] = 1;

                SkillsInfo[2, 4].Skill_Img = Resources.Load<Sprite>("Characters/CruActiveSkill2");
                SkillsInfo[2, 4].Name = "크루세이더 액티브 스킬 2 레벨 4";
                SkillsInfo[2, 4].Type = "Active";
                SkillsInfo[2, 4].ExplainText = "크루세이더 액티브 스킬 2의 레벨 4에 대한 설명입니다.";
                SkillsInfo[2, 4].Damage = 8.0;
                SkillsInfo[2, 4].Duration = 6;
                SkillsInfo[2, 4].Coefficient = 2.2;
                SkillsInfo[2, 4].Scope[5] = 1;

                SkillsInfo[2, 5].Skill_Img = Resources.Load<Sprite>("Characters/CruActiveSkill2");
                SkillsInfo[2, 5].Name = "크루세이더 액티브 스킬 2 레벨 5";
                SkillsInfo[2, 5].Type = "Active";
                SkillsInfo[2, 5].ExplainText = "크루세이더 액티브 스킬 2의 레벨 5에 대한 설명입니다.";
                SkillsInfo[2, 5].Damage = 9.0;
                SkillsInfo[2, 5].Duration = 7;
                SkillsInfo[2, 5].Coefficient = 2.2;
                SkillsInfo[2, 5].Scope[5] = 1;

                SkillsInfo[7, 1].Skill_Img = Resources.Load<Sprite>("Characters/CruPassiveSkill1");
                SkillsInfo[7, 1].Name = "크루세이더 패시브 스킬1 레벨 1";
                SkillsInfo[7, 1].Type = "Passive";
                SkillsInfo[7, 1].ExplainText = "크루세이더 패시브 스킬 1의 레벨 1에 대한 설명입니다.";
                SkillsInfo[7, 1].Damage = 5.0;
                SkillsInfo[7, 1].Duration = 5;
                SkillsInfo[7, 1].Coefficient = 2.2;

                SkillsInfo[7, 2].Skill_Img = Resources.Load<Sprite>("Characters/CruPassiveSkill1");
                SkillsInfo[7, 2].Name = "크루세이더 패시브 스킬1 레벨 2";
                SkillsInfo[7, 2].Type = "Passive";
                SkillsInfo[7, 2].ExplainText = "크루세이더 패시브 스킬 1의 레벨 2에 대한 설명입니다.";
                SkillsInfo[7, 2].Damage = 5.0;
                SkillsInfo[7, 2].Duration = 5;
                SkillsInfo[7, 2].Coefficient = 2.2;

                SkillsInfo[7, 3].Skill_Img = Resources.Load<Sprite>("Characters/CruPassiveSkill1");
                SkillsInfo[7, 3].Name = "크루세이더 패시브 스킬1 레벨 3";
                SkillsInfo[7, 3].Type = "Passive";
                SkillsInfo[7, 3].ExplainText = "크루세이더 패시브 스킬 1의 레벨 3에 대한 설명입니다.";
                SkillsInfo[7, 3].Damage = 5.0;
                SkillsInfo[7, 3].Duration = 5;
                SkillsInfo[7, 3].Coefficient = 2.2;
                //end - 크루세이더 스킬 세팅

                //start - 크루세이더 장비 세팅
                EquipmentInfo[1].Equip_Img = Resources.Load<Sprite>("Characters/CruEquipment1");
                EquipmentInfo[1].ExplainText = "크루세이더 장비 레벨 1의 소개 텍스트";
                EquipmentInfo[1].Attack = 10;
                EquipmentInfo[1].Accuracy = 5;
                EquipmentInfo[1].Speed = 1;
                EquipmentInfo[1].Armor = 10;
                EquipmentInfo[1].Avoidance = 1;

                EquipmentInfo[2].Equip_Img = Resources.Load<Sprite>("Characters/CruEquipment2");
                EquipmentInfo[2].ExplainText = "크루세이더 장비 레벨 2의 소개 텍스트";
                EquipmentInfo[2].Attack = 20;
                EquipmentInfo[2].Accuracy = 5;
                EquipmentInfo[2].Speed = 1;
                EquipmentInfo[2].Armor = 10;
                EquipmentInfo[2].Avoidance = 1;

                EquipmentInfo[3].Equip_Img = Resources.Load<Sprite>("Characters/CruEquipment3");
                EquipmentInfo[3].ExplainText = "크루세이더 장비 레벨 3의 소개 텍스트";
                EquipmentInfo[3].Attack = 30;
                EquipmentInfo[3].Accuracy = 5;
                EquipmentInfo[3].Speed = 1;
                EquipmentInfo[3].Armor = 10;
                EquipmentInfo[3].Avoidance = 1;

                EquipmentInfo[4].Equip_Img = Resources.Load<Sprite>("Characters/CruEquipment4");
                EquipmentInfo[4].ExplainText = "크루세이더 최종 장비의 소개 텍스트";
                EquipmentInfo[4].Attack = 40;
                EquipmentInfo[4].Accuracy = 5;
                EquipmentInfo[4].Speed = 1;
                EquipmentInfo[4].Armor = 10;
                EquipmentInfo[4].Avoidance = 1;

                //end - 크루세이더 장비 세팅



                break;

            case "Pir":
                //start - 해적 기본 세팅

                BasicSets[1].Portrait = Resources.Load<Sprite>("Characters/PirPortrait");
                BasicSets[1].Portrait_Vertical = Resources.Load<Sprite>("Characters/PirPortrait_Vertical");
                BasicSets[1].Portrait_Horizontal = Resources.Load<Sprite>("Characters/PirPortrait_Horizontal");
                BasicSets[1].CharacterIntroText = "해적 소개에 대한 텍스트 입니다. 해적은~";
                BasicSets[1].Health = 50;
                BasicSets[1].Morale = 50;
                BasicSets[1].Will = 21.1;

                BasicSets[2].Portrait = Resources.Load<Sprite>("Characters/PirPortrait");
                BasicSets[2].Portrait_Vertical = Resources.Load<Sprite>("Characters/PirPortrait_Vertical");
                BasicSets[2].Portrait_Horizontal = Resources.Load<Sprite>("Characters/PirPortrait_Horizontal");
                BasicSets[2].CharacterIntroText = "해적 소개에 대한 텍스트 입니다. 해적은~";
                BasicSets[2].Health = 60;
                BasicSets[2].Morale = 60;
                BasicSets[2].Will = 21.1;

                BasicSets[3].Portrait = Resources.Load<Sprite>("Characters/PirPortrait");
                BasicSets[3].Portrait_Vertical = Resources.Load<Sprite>("Characters/PirPortrait_Vertical");
                BasicSets[3].Portrait_Horizontal = Resources.Load<Sprite>("Characters/PirPortrait_Horizontal");
                BasicSets[3].CharacterIntroText = "해적 소개에 대한 텍스트 입니다. 해적은~";
                BasicSets[3].Health = 70;
                BasicSets[3].Morale = 70;
                BasicSets[3].Will = 21.1;

                BasicSets[4].Portrait = Resources.Load<Sprite>("Characters/PirPortrait");
                BasicSets[4].Portrait_Vertical = Resources.Load<Sprite>("Characters/PirPortrait_Vertical");
                BasicSets[4].Portrait_Horizontal = Resources.Load<Sprite>("Characters/PirPortrait_Horizontal");
                BasicSets[4].CharacterIntroText = "해적 소개에 대한 텍스트 입니다. 해적은~";
                BasicSets[4].Health = 80;
                BasicSets[4].Morale = 80;
                BasicSets[4].Will = 21.1;

                BasicSets[5].Portrait = Resources.Load<Sprite>("Characters/PirPortrait");
                BasicSets[5].Portrait_Vertical = Resources.Load<Sprite>("Characters/PirPortrait_Vertical");
                BasicSets[5].Portrait_Horizontal = Resources.Load<Sprite>("Characters/PirPortrait_Horizontal");
                BasicSets[5].CharacterIntroText = "해적 소개에 대한 텍스트 입니다. 해적은~";
                BasicSets[5].Health = 90;
                BasicSets[5].Morale = 90;
                BasicSets[5].Will = 21.1;

                //end - 해적 기본 세팅

                //start - 해적 스킬 세팅

                SkillsInfo[1, 1].Skill_Img = Resources.Load<Sprite>("Characters/PirActiveSkill1");
                SkillsInfo[1, 1].Name = "해적 액티브 스킬1 레벨 1";
                SkillsInfo[1, 1].Type = "Active";
                SkillsInfo[1, 1].ExplainText = "해적 액티브 스킬 1의 레벨 1에 대한 설명입니다.";
                SkillsInfo[1, 1].Damage = 21.0;
                SkillsInfo[1, 1].Duration = 3;
                SkillsInfo[1, 1].Coefficient = 1.2;
                SkillsInfo[1, 1].Scope[1] = 1;
                SkillsInfo[1, 1].Scope[3] = 1;
                SkillsInfo[1, 1].Scope[5] = 1;
                SkillsInfo[1, 1].Scope[6] = 1;

                SkillsInfo[1, 2].Skill_Img = Resources.Load<Sprite>("Characters/PirActiveSkill1");
                SkillsInfo[1, 2].Name = "해적 액티브 스킬1 레벨 2";
                SkillsInfo[1, 2].Type = "Active";
                SkillsInfo[1, 2].ExplainText = "해적 액티브 스킬 1의 레벨 2에 대한 설명입니다.";
                SkillsInfo[1, 2].Damage = 22.0;
                SkillsInfo[1, 2].Duration = 3;
                SkillsInfo[1, 2].Coefficient = 1.2;
                SkillsInfo[1, 2].Scope[1] = 1;
                SkillsInfo[1, 2].Scope[3] = 1;
                SkillsInfo[1, 2].Scope[5] = 1;
                SkillsInfo[1, 2].Scope[6] = 1;

                SkillsInfo[1, 3].Skill_Img = Resources.Load<Sprite>("Characters/PirActiveSkill1");
                SkillsInfo[1, 3].Name = "해적 액티브 스킬1 레벨 3";
                SkillsInfo[1, 3].Type = "Active";
                SkillsInfo[1, 3].ExplainText = "해적 액티브 스킬 1의 레벨 3에 대한 설명입니다.";
                SkillsInfo[1, 3].Damage = 33.0;
                SkillsInfo[1, 3].Duration = 3;
                SkillsInfo[1, 3].Coefficient = 1.2;
                SkillsInfo[1, 3].Scope[1] = 1;
                SkillsInfo[1, 3].Scope[3] = 1;
                SkillsInfo[1, 3].Scope[5] = 1;
                SkillsInfo[1, 3].Scope[6] = 1;

                SkillsInfo[1, 4].Skill_Img = Resources.Load<Sprite>("Characters/PirActiveSkill1");
                SkillsInfo[1, 4].Name = "해적 액티브 스킬1 레벨 4";
                SkillsInfo[1, 4].Type = "Active";
                SkillsInfo[1, 4].ExplainText = "해적 액티브 스킬 1의 레벨 4에 대한 설명입니다.";
                SkillsInfo[1, 4].Damage = 44.0;
                SkillsInfo[1, 4].Duration = 3;
                SkillsInfo[1, 4].Coefficient = 1.2;
                SkillsInfo[1, 4].Scope[1] = 1;
                SkillsInfo[1, 4].Scope[3] = 1;
                SkillsInfo[1, 4].Scope[5] = 1;
                SkillsInfo[1, 4].Scope[6] = 1;

                SkillsInfo[1, 5].Skill_Img = Resources.Load<Sprite>("Characters/PirActiveSkill1");
                SkillsInfo[1, 5].Name = "해적 액티브 스킬1 레벨 5";
                SkillsInfo[1, 5].Type = "Active";
                SkillsInfo[1, 5].ExplainText = "해적 액티브 스킬 1의 레벨 5에 대한 설명입니다.";
                SkillsInfo[1, 5].Damage = 55.0;
                SkillsInfo[1, 5].Duration = 3;
                SkillsInfo[1, 5].Coefficient = 1.2;
                SkillsInfo[1, 5].Scope[1] = 1;
                SkillsInfo[1, 5].Scope[3] = 1;
                SkillsInfo[1, 5].Scope[5] = 1;
                SkillsInfo[1, 5].Scope[6] = 1;

                SkillsInfo[2, 1].Skill_Img = Resources.Load<Sprite>("Characters/PirActiveSkill2");
                SkillsInfo[2, 1].Name = "해적 액티브 스킬2 레벨 1";
                SkillsInfo[2, 1].Type = "Active";
                SkillsInfo[2, 1].ExplainText = "해적 액티브 스킬 2의 레벨 1에 대한 설명입니다.";
                SkillsInfo[2, 1].Damage = 5.0;
                SkillsInfo[2, 1].Duration = 5;
                SkillsInfo[2, 1].Coefficient = 2.2;
                SkillsInfo[2, 1].Scope[6] = 1;

                SkillsInfo[2, 2].Skill_Img = Resources.Load<Sprite>("Characters/PirActiveSkill2");
                SkillsInfo[2, 2].Name = "해적 액티브 스킬2 레벨 2";
                SkillsInfo[2, 2].Type = "Active";
                SkillsInfo[2, 2].ExplainText = "해적 액티브 스킬 2의 레벨 2에 대한 설명입니다.";
                SkillsInfo[2, 2].Damage = 6.0;
                SkillsInfo[2, 2].Duration = 5;
                SkillsInfo[2, 2].Coefficient = 2.2;
                SkillsInfo[2, 2].Scope[6] = 1;

                SkillsInfo[2, 3].Skill_Img = Resources.Load<Sprite>("Characters/PirActiveSkill2");
                SkillsInfo[2, 3].Name = "해적 액티브 스킬2 레벨 3";
                SkillsInfo[2, 3].Type = "Active";
                SkillsInfo[2, 3].ExplainText = "해적 액티브 스킬 2의 레벨 3에 대한 설명입니다.";
                SkillsInfo[2, 3].Damage = 7.0;
                SkillsInfo[2, 3].Duration = 6;
                SkillsInfo[2, 3].Coefficient = 2.2;
                SkillsInfo[2, 3].Scope[6] = 1;

                SkillsInfo[2, 4].Skill_Img = Resources.Load<Sprite>("Characters/PirActiveSkill2");
                SkillsInfo[2, 4].Name = "해적 액티브 스킬2 레벨 4";
                SkillsInfo[2, 4].Type = "Active";
                SkillsInfo[2, 4].ExplainText = "해적 액티브 스킬 2의 레벨 4에 대한 설명입니다.";
                SkillsInfo[2, 4].Damage = 8.0;
                SkillsInfo[2, 4].Duration = 6;
                SkillsInfo[2, 4].Coefficient = 2.2;
                SkillsInfo[2, 4].Scope[6] = 1;

                SkillsInfo[2, 5].Skill_Img = Resources.Load<Sprite>("Characters/PirActiveSkill2");
                SkillsInfo[2, 5].Name = "해적 액티브 스킬2 레벨 5";
                SkillsInfo[2, 5].Type = "Active";
                SkillsInfo[2, 5].ExplainText = "해적 액티브 스킬 2의 레벨 5에 대한 설명입니다.";
                SkillsInfo[2, 5].Damage = 9.0;
                SkillsInfo[2, 5].Duration = 7;
                SkillsInfo[2, 5].Coefficient = 2.2;
                SkillsInfo[2, 5].Scope[6] = 1;

                SkillsInfo[7, 1].Skill_Img = Resources.Load<Sprite>("Characters/PirPassiveSkill1");
                SkillsInfo[7, 1].Name = "해적 패시브 스킬1 레벨 1";
                SkillsInfo[7, 1].Type = "Passive";
                SkillsInfo[7, 1].ExplainText = "해적 패시브 스킬 1의 레벨 1에 대한 설명입니다.";
                SkillsInfo[7, 1].Damage = 5.0;
                SkillsInfo[7, 1].Duration = 5;
                SkillsInfo[7, 1].Coefficient = 2.2;

                SkillsInfo[7, 2].Skill_Img = Resources.Load<Sprite>("Characters/PirPassiveSkill1");
                SkillsInfo[7, 2].Name = "해적 패시브 스킬1 레벨 2";
                SkillsInfo[7, 2].Type = "Passive";
                SkillsInfo[7, 2].ExplainText = "해적 패시브 스킬 1의 레벨 2에 대한 설명입니다.";
                SkillsInfo[7, 2].Damage = 5.0;
                SkillsInfo[7, 2].Duration = 5;
                SkillsInfo[7, 2].Coefficient = 2.2;

                SkillsInfo[7, 3].Skill_Img = Resources.Load<Sprite>("Characters/PirPassiveSkill1");
                SkillsInfo[7, 3].Name = "해적 패시브 스킬1 레벨 3";
                SkillsInfo[7, 3].Type = "Passive";
                SkillsInfo[7, 3].ExplainText = "해적 패시브 스킬 1의 레벨 3에 대한 설명입니다.";
                SkillsInfo[7, 3].Damage = 5.0;
                SkillsInfo[7, 3].Duration = 5;
                SkillsInfo[7, 3].Coefficient = 2.2;


                //end - 해적 스킬 세팅
                //start - 해적 장비 세팅

                EquipmentInfo[1].Equip_Img = Resources.Load<Sprite>("Characters/PirEquipment1");
                EquipmentInfo[1].ExplainText = "해적 장비 레벨 1의 소개 텍스트";
                EquipmentInfo[1].Attack = 10;
                EquipmentInfo[1].Accuracy = 5;
                EquipmentInfo[1].Speed = 1;
                EquipmentInfo[1].Armor = 10;
                EquipmentInfo[1].Avoidance = 1;

                EquipmentInfo[2].Equip_Img = Resources.Load<Sprite>("Characters/PirEquipment2");
                EquipmentInfo[2].ExplainText = "해적 장비 레벨 2의 소개 텍스트";
                EquipmentInfo[2].Attack = 20;
                EquipmentInfo[2].Accuracy = 5;
                EquipmentInfo[2].Speed = 1;
                EquipmentInfo[2].Armor = 10;
                EquipmentInfo[2].Avoidance = 1;

                EquipmentInfo[3].Equip_Img = Resources.Load<Sprite>("Characters/PirEquipment3");
                EquipmentInfo[3].ExplainText = "해적 장비 레벨 3의 소개 텍스트";
                EquipmentInfo[3].Attack = 30;
                EquipmentInfo[3].Accuracy = 5;
                EquipmentInfo[3].Speed = 1;
                EquipmentInfo[3].Armor = 10;
                EquipmentInfo[3].Avoidance = 1;

                EquipmentInfo[4].Equip_Img = Resources.Load<Sprite>("Characters/PirEquipment4");
                EquipmentInfo[4].ExplainText = "해적 최종 장비의 소개 텍스트";
                EquipmentInfo[4].Attack = 40;
                EquipmentInfo[4].Accuracy = 5;
                EquipmentInfo[4].Speed = 1;
                EquipmentInfo[4].Armor = 10;
                EquipmentInfo[4].Avoidance = 1;


                //end - 해적 장비 세팅

                break;



            case "Mag":

                //start - 메이지 기본 세팅
                BasicSets[1].Portrait = Resources.Load<Sprite>("Characters/MagePortrait");
                BasicSets[1].Portrait_Vertical = Resources.Load<Sprite>("Characters/MagePortrait_Vertical");
                BasicSets[1].Portrait_Horizontal = Resources.Load<Sprite>("Characters/MagePortrait_Horizontal");
                BasicSets[1].CharacterIntroText = "메이지 소개에 대한 텍스트 입니다. 메이지는~";
                BasicSets[1].Health = 40;
                BasicSets[1].Morale = 40;
                BasicSets[1].Will = 30.1;

                BasicSets[2].Portrait = Resources.Load<Sprite>("Characters/MagePortrait");
                BasicSets[2].Portrait_Vertical = Resources.Load<Sprite>("Characters/MagePortrait_Vertical");
                BasicSets[2].Portrait_Horizontal = Resources.Load<Sprite>("Characters/MagePortrait_Horizontal");
                BasicSets[2].CharacterIntroText = "메이지 소개에 대한 텍스트 입니다. 메이지는~";
                BasicSets[2].Health = 50;
                BasicSets[2].Morale = 50;
                BasicSets[2].Will = 32.1;

                BasicSets[3].Portrait = Resources.Load<Sprite>("Characters/MagePortrait");
                BasicSets[3].Portrait_Vertical = Resources.Load<Sprite>("Characters/MagePortrait_Vertical");
                BasicSets[3].Portrait_Horizontal = Resources.Load<Sprite>("Characters/MagePortrait_Horizontal");
                BasicSets[3].CharacterIntroText = "메이지 소개에 대한 텍스트 입니다. 메이지는~";
                BasicSets[3].Health = 60;
                BasicSets[3].Morale = 60;
                BasicSets[3].Will = 34.1;

                BasicSets[4].Portrait = Resources.Load<Sprite>("Characters/MagePortrait");
                BasicSets[4].Portrait_Vertical = Resources.Load<Sprite>("Characters/MagePortrait_Vertical");
                BasicSets[4].Portrait_Horizontal = Resources.Load<Sprite>("Characters/MagePortrait_Horizontal");
                BasicSets[4].CharacterIntroText = "메이지 소개에 대한 텍스트 입니다. 메이지는~";
                BasicSets[4].Health = 70;
                BasicSets[4].Morale = 70;
                BasicSets[4].Will = 36.1;

                BasicSets[5].Portrait = Resources.Load<Sprite>("Characters/MagePortrait");
                BasicSets[5].Portrait_Vertical = Resources.Load<Sprite>("Characters/MagePortrait_Vertical");
                BasicSets[5].Portrait_Horizontal = Resources.Load<Sprite>("Characters/MagePortrait_Horizontal");
                BasicSets[5].CharacterIntroText = "메이지 소개에 대한 텍스트 입니다. 메이지는~";
                BasicSets[5].Health = 90;
                BasicSets[5].Morale = 90;
                BasicSets[5].Will = 38.1;
                //end - 메이지 기본 세팅

                //start - 메이지 스킬 세팅
                //end - 메이지 스킬 세팅
                //start - 메이지 장비 세팅
                //end - 메이지 장비 세팅

                break;



            case "Sli":

                //start - 슬링어 기본 세팅
                BasicSets[1].Portrait = Resources.Load<Sprite>("Characters/SlingerPortrait");
                BasicSets[1].Portrait_Vertical = Resources.Load<Sprite>("Characters/SlingerPortrait_Vertical");
                BasicSets[1].Portrait_Horizontal = Resources.Load<Sprite>("Characters/SlingerPortrait_Horizontal");
                BasicSets[1].CharacterIntroText = "슬링어 소개에 대한 텍스트 입니다. 슬링어는~";
                BasicSets[1].Health = 50;
                BasicSets[1].Morale = 50;
                BasicSets[1].Will = 25.1;

                BasicSets[2].Portrait = Resources.Load<Sprite>("Characters/SlingerPortrait");
                BasicSets[2].Portrait_Vertical = Resources.Load<Sprite>("Characters/SlingerPortrait_Vertical");
                BasicSets[2].Portrait_Horizontal = Resources.Load<Sprite>("Characters/SlingerPortrait_Horizontal");
                BasicSets[2].CharacterIntroText = "슬링어 소개에 대한 텍스트 입니다. 슬링어는~";
                BasicSets[2].Health = 50;
                BasicSets[2].Morale = 50;
                BasicSets[2].Will = 25.1;

                BasicSets[3].Portrait = Resources.Load<Sprite>("Characters/SlingerPortrait");
                BasicSets[3].Portrait_Vertical = Resources.Load<Sprite>("Characters/SlingerPortrait_Vertical");
                BasicSets[3].Portrait_Horizontal = Resources.Load<Sprite>("Characters/SlingerPortrait_Horizontal");
                BasicSets[3].CharacterIntroText = "슬링어 소개에 대한 텍스트 입니다. 슬링어는~";
                BasicSets[3].Health = 50;
                BasicSets[3].Morale = 50;
                BasicSets[3].Will = 25.1;

                BasicSets[4].Portrait = Resources.Load<Sprite>("Characters/SlingerPortrait");
                BasicSets[4].Portrait_Vertical = Resources.Load<Sprite>("Characters/SlingerPortrait_Vertical");
                BasicSets[4].Portrait_Horizontal = Resources.Load<Sprite>("Characters/SlingerPortrait_Horizontal");
                BasicSets[4].CharacterIntroText = "슬링어 소개에 대한 텍스트 입니다. 슬링어는~";
                BasicSets[4].Health = 50;
                BasicSets[4].Morale = 50;
                BasicSets[4].Will = 25.1;

                BasicSets[5].Portrait = Resources.Load<Sprite>("Characters/SlingerPortrait");
                BasicSets[5].Portrait_Vertical = Resources.Load<Sprite>("Characters/SlingerPortrait_Vertical");
                BasicSets[5].Portrait_Horizontal = Resources.Load<Sprite>("Characters/SlingerPortrait_Horizontal");
                BasicSets[5].CharacterIntroText = "슬링어 소개에 대한 텍스트 입니다. 슬링어는~";
                BasicSets[5].Health = 50;
                BasicSets[5].Morale = 50;
                BasicSets[5].Will = 25.1;

                //end - 슬링어 기본 세팅

                //start - 슬링어 스킬 세팅
                //end - 슬링어 스킬 세팅
                //start - 슬링어 장비 세팅
                //end - 슬링어 장비 세팅

                break;

        }

    }


    public class Character_BasicSet
    {

        //초상화, 캐릭터 소개 정보
        public Sprite Portrait;
        public Sprite Portrait_Vertical;
        public Sprite Portrait_Horizontal;
        public string CharacterIntroText;

        //최대 체력, 최대 사기, 의지 : 캐릭터 레벨에 따른 증가 스텟
        public int Health;
        public int Morale;
        public double Will;
    }

    public class Character_SkillSet
    {
        public Sprite Skill_Img; //스킬 이미지
        public string Name;
        public string Type; //스킬의 타입
        public string ExplainText;

        public double Damage;
        public double Duration; // 지속 시간
        public double Coefficient; //계수: 임시 변수
        public int[] Scope = new int[Variable.SkillScopeMax]; //범위
    }

    public class Character_EquipSet
    {
        public Sprite Equip_Img; //장비 이미지
        public string ExplainText;

        public double Attack;
        public double Accuracy;
        public double Speed;
        public double Armor;
        public double Avoidance;

        public string SpecialEffect; //최종 레벨의 장비일때, 특수 효과 : 임시 변수
    }


}//Character 클래스



public class Enemy
{
    //배열의 각 인자는 적 몬스터의 레벨
    public Enemy_BasicSet[] EnemyInfo = new Enemy_BasicSet[Variable.EnemyLevelMax];

    public Enemy(int num)
    {
        for (int i = 1; i < Variable.EnemyLevelMax; i++)
        {
            EnemyInfo[i] = new Enemy_BasicSet();
            for (int j = 1; j < Variable.EnemySkillMany; j++)
                EnemyInfo[i].SkillSet[j] = new Enemy_BasicSet.Enemy_SkillSet();
        }


        switch (num) //고유번호를 뜻함
        {
            case 1:

                //st - elf 1
                //st - lv.1
                EnemyInfo[1].Number = 1;
                EnemyInfo[1].Portrait = Resources.Load<Sprite>("Enemy/Elf/Elf1_Portrait");
                EnemyInfo[1].Name = "스코이아 텔 순찰자";
                EnemyInfo[1].Trible = "Elf";
                EnemyInfo[1].IntroText = "몬스터 설명 텍스트 1";
                EnemyInfo[1].Type = "Nomal";
                EnemyInfo[1].level = 1;
                EnemyInfo[1].Health = 10;
                EnemyInfo[1].Morale = 10;
                EnemyInfo[1].Will = 1;
                EnemyInfo[1].Attack = 5;
                EnemyInfo[1].Accuracy = 7;
                EnemyInfo[1].Speed = 4;
                EnemyInfo[1].Armor = 2;
                EnemyInfo[1].Avoidance = 5;
                EnemyInfo[1].SpecialEffect = "None";

                //EnemyInfo[1].SkillSet[1].Skill_Img = Resources.Load<Sprite>("Enemy/Elf/Elf1_Skill1");
                EnemyInfo[1].SkillSet[1].Type = "Near";
                EnemyInfo[1].SkillSet[1].ExplainText = "elf1의 액티브 스킬 1 레벨 1";
                EnemyInfo[1].SkillSet[1].Damage = 10;
                EnemyInfo[1].SkillSet[1].Duration = 1;
                EnemyInfo[1].SkillSet[1].Coefficient = 1;
                EnemyInfo[1].SkillSet[1].Scope[3] = 1;
                EnemyInfo[1].SkillSet[1].Scope[4] = 1;
                EnemyInfo[1].SkillSet[1].Scope[5] = 1;
                EnemyInfo[1].SkillSet[1].Scope[6] = 1;

                EnemyInfo[1].SkillSet[2].Type = "Near";
                EnemyInfo[1].SkillSet[2].ExplainText = "elf1의 액티브 스킬 2 레벨 1";
                EnemyInfo[1].SkillSet[2].Damage = 10;
                EnemyInfo[1].SkillSet[2].Duration = 1;
                EnemyInfo[1].SkillSet[2].Coefficient = 1;
                EnemyInfo[1].SkillSet[2].Scope[3] = 1;
                EnemyInfo[1].SkillSet[2].Scope[4] = 1;
                EnemyInfo[1].SkillSet[2].Scope[5] = 1;
                EnemyInfo[1].SkillSet[2].Scope[6] = 1;

                EnemyInfo[1].SkillSet[3].Type = "Near";
                EnemyInfo[1].SkillSet[3].ExplainText = "elf1의 액티브 스킬 3 레벨 1";
                EnemyInfo[1].SkillSet[3].Damage = 10;
                EnemyInfo[1].SkillSet[3].Duration = 1;
                EnemyInfo[1].SkillSet[3].Coefficient = 1;
                EnemyInfo[1].SkillSet[3].Scope[3] = 1;
                EnemyInfo[1].SkillSet[3].Scope[4] = 1;
                EnemyInfo[1].SkillSet[3].Scope[5] = 1;
                EnemyInfo[1].SkillSet[3].Scope[6] = 1;

                EnemyInfo[1].SkillSet[4].Type = "Near";
                EnemyInfo[1].SkillSet[4].ExplainText = "elf1의 패시브 스킬 1 레벨 1";
                EnemyInfo[1].SkillSet[4].Damage = 10;
                EnemyInfo[1].SkillSet[4].Duration = 1;
                EnemyInfo[1].SkillSet[4].Coefficient = 1;
                EnemyInfo[1].SkillSet[4].Scope[3] = 1;
                EnemyInfo[1].SkillSet[4].Scope[4] = 1;
                EnemyInfo[1].SkillSet[4].Scope[5] = 1;
                EnemyInfo[1].SkillSet[4].Scope[6] = 1;
                //ed - lv.1
                //st - lv.2
                EnemyInfo[2].Number = 1;
                EnemyInfo[2].Portrait = Resources.Load<Sprite>("Enemy/Elf/Elf1_Portrait");
                EnemyInfo[2].Name = "스코이아 텔 순찰자";
                EnemyInfo[2].Trible = "Elf";
                EnemyInfo[2].IntroText = "스코이아 텔에서 흔하게 보이는 엘프. 비교적 조잡한 무장으로봐서 신병으로 추측된다.";
                EnemyInfo[2].Type = "Nomal";
                EnemyInfo[2].level = 2;
                EnemyInfo[2].Health = 10;
                EnemyInfo[2].Morale = 10;
                EnemyInfo[2].Will = 1;
                EnemyInfo[2].Attack = 5;
                EnemyInfo[2].Accuracy = 7;
                EnemyInfo[2].Speed = 4;
                EnemyInfo[2].Armor = 2;
                EnemyInfo[2].Avoidance = 5;
                EnemyInfo[2].SpecialEffect = "None";
                //..이하 생략
                //ed - lv.2


                //ed - elf 1





                break;

            case 2:
                //st - elf 2
                //st - lv.1
                EnemyInfo[1].Number = 2;
                //ed - lv.1


                //ed - elf 2
                break;

            case 3:

                break;

            case 4:

                break;

            case 5:

                break;

        }


    }
    public Enemy()
    {
        for (int i = 1; i < Variable.EnemyLevelMax; i++)
        {
            EnemyInfo[i] = new Enemy_BasicSet();
            for (int j = 1; j < Variable.EnemySkillMany; j++)
                EnemyInfo[i].SkillSet[j] = new Enemy_BasicSet.Enemy_SkillSet();
        }
    }
    public class Enemy_BasicSet
    {
        public int Number; //몬스터 고유 번호 (==인덱스 번호, 이름과 같은 역활)

        public Sprite Portrait;
        public string Name;
        public string Trible; //종족
        public string IntroText;
        public string Type; //몬스터의 타입, 분류(노말, 보스, 중간보스 등) : 임시 변수

        public int level;
        public int Health;
        public int Morale; //몬스터의 사기 수치 : 임시변수
        public double Will;

        public double Attack;
        public double Accuracy;
        public double Speed;
        public double Armor;
        public double Avoidance;

        public string SpecialEffect; //몬스터의 특수 능력? : 임시 변수

        //1~3 인덱스는 액티브 스킬, 4 인덱스는 패시브 스킬 
        public Enemy_SkillSet[] SkillSet = new Enemy_SkillSet[Variable.EnemySkillMany];

        public class Enemy_SkillSet
        {
            //public Sprite Skill_Img; //스킬 이미지 - 쓰지 않음
            public string Type; //스킬의 타입
            public string ExplainText;

            public double Damage;
            public double Duration; // 지속 시간
            public double Coefficient; //계수: 임시 변수
            public int[] Scope = new int[Variable.SkillScopeMax]; //범위
        }

    }


}


public class StageInfo
{
    public StageInfo_BasicSet[] Stage = new StageInfo_BasicSet[Variable.StageSize];
    public string Name;
    public int Number; //스테이지 고유 번호
    public StageInfo(string name)
    {
        for (int i = 1; i < Variable.StageSize; i++)
            Stage[i] = new StageInfo_BasicSet();

        switch (name)
        {
            case "Elf":

                Name = "Stinky Forest";
                Number = 1;

                Stage[1].Type = "Nomal";
                Stage[1].State = "Lock";
                Stage[1].EnemyIndex[1] = 1;
                Stage[1].EnemyIndex[2] = 1;
                Stage[1].EnemyIndex[3] = 1;
                Stage[1].EnemyIndex[4] = 1;
                Stage[1].DropItem_Index = 0; //모름
                Stage[1].Event_Index = 0; // 모름

                Stage[2].Type = "Nomal";
                Stage[2].State = "Lock";
                Stage[2].EnemyIndex[1] = 1;
                Stage[2].EnemyIndex[2] = 1;
                Stage[2].EnemyIndex[3] = 1;
                Stage[2].EnemyIndex[4] = 1;
                Stage[2].DropItem_Index = 0; //모름
                Stage[2].Event_Index = 0; // 모름

                Stage[3].Type = "Nomal";
                Stage[3].State = "Lock";
                Stage[3].EnemyIndex[1] = 1;
                Stage[3].EnemyIndex[2] = 1;
                Stage[3].EnemyIndex[3] = 1;
                Stage[3].EnemyIndex[4] = 1;
                Stage[3].DropItem_Index = 0; //모름
                Stage[3].Event_Index = 0; // 모름

                Stage[4].Type = "Foothold";
                Stage[4].State = "Lock";
                Stage[4].DropItem_Index = 0; //모름
                Stage[4].Event_Index = 0; // 모름

                Stage[5].Type = "Nomal";
                Stage[5].State = "Lock";
                Stage[5].EnemyIndex[1] = 1;
                Stage[5].EnemyIndex[2] = 1;
                Stage[5].EnemyIndex[3] = 1;
                Stage[5].EnemyIndex[4] = 1;
                Stage[5].DropItem_Index = 0; //모름
                Stage[5].Event_Index = 0; // 모름

                Stage[6].Type = "Nomal";
                Stage[6].State = "Lock";
                Stage[6].EnemyIndex[1] = 1;
                Stage[6].EnemyIndex[2] = 1;
                Stage[6].EnemyIndex[3] = 1;
                Stage[6].EnemyIndex[4] = 1;
                Stage[6].DropItem_Index = 0; //모름
                Stage[6].Event_Index = 0; // 모름

                Stage[7].Type = "Nomal";
                Stage[7].State = "Lock";
                Stage[7].EnemyIndex[1] = 1;
                Stage[7].EnemyIndex[2] = 1;
                Stage[7].EnemyIndex[3] = 1;
                Stage[7].EnemyIndex[4] = 1;
                Stage[7].DropItem_Index = 0; //모름
                Stage[7].Event_Index = 0; // 모름

                Stage[8].Type = "Foothold";
                Stage[8].State = "Lock";
                Stage[8].DropItem_Index = 0; //모름
                Stage[8].Event_Index = 0; // 모름

                Stage[9].Type = "Nomal";
                Stage[9].State = "Lock";
                Stage[9].EnemyIndex[1] = 1;
                Stage[9].EnemyIndex[2] = 1;
                Stage[9].EnemyIndex[3] = 1;
                Stage[9].EnemyIndex[4] = 1;
                Stage[9].DropItem_Index = 0; //모름
                Stage[9].Event_Index = 0; // 모름

                Stage[10].Type = "Nomal";
                Stage[10].State = "Lock";
                Stage[10].EnemyIndex[1] = 1;
                Stage[10].EnemyIndex[2] = 1;
                Stage[10].EnemyIndex[3] = 1;
                Stage[10].EnemyIndex[4] = 1;
                Stage[10].DropItem_Index = 0; //모름
                Stage[10].Event_Index = 0; // 모름

                Stage[11].Type = "Nomal";
                Stage[11].State = "Lock";
                Stage[11].EnemyIndex[1] = 1;
                Stage[11].EnemyIndex[2] = 1;
                Stage[11].EnemyIndex[3] = 1;
                Stage[11].EnemyIndex[4] = 1;
                Stage[11].DropItem_Index = 0; //모름
                Stage[11].Event_Index = 0; // 모름

                Stage[12].Type = "Foothold";
                Stage[12].State = "Lock";
                Stage[12].DropItem_Index = 0; //모름
                Stage[12].Event_Index = 0; // 모름

                Stage[13].Type = "ElfBoss";
                Stage[13].State = "Lock";
                Stage[13].EnemyIndex[1] = 1;
                Stage[13].EnemyIndex[2] = 1;
                Stage[13].EnemyIndex[3] = 1;
                Stage[13].EnemyIndex[4] = 1;
                Stage[13].DropItem_Index = 0; //모름
                Stage[13].Event_Index = 0; // 모름

                break;

            case "Orc":

                Name = "Stage 2. 붉은 황무지\n- 오크 지역 -";
                Number = 2;

                break;

            case "Lizard":

                Name = "Stage 3. 푸른 해안\n- 리자드맨 지역 -";
                Number = 3;

                break;

            case "Undead":

                Name = "Stage 4. 검은 묘지\n- 언데드 지역 -";
                Number = 4;

                break;

            case "Final":

                Name = "Stage 5. 최종\n- 최종 -";
                Number = 5;

                break;
        }
    }
    public StageInfo()
    {
        for (int i = 1; i < Variable.StageSize; i++)
            Stage[i] = new StageInfo_BasicSet();
    }
    public class StageInfo_BasicSet
    {
        public string Type; //중간 지점, 일반, 보스, 중간보스
        public string State; //Lock(잠김 - 기본값), Clear(클리어), Half(클리어 했지만 중간지점 도달 실패) : 임시 변수
        public int[] EnemyIndex = new int[Variable.Each_StageEnemyMax];

        public int DropItem_Index; //드롭 아이템 인덱스
        public int Event_Index; //스테이지 관련 발생 이벤트의 인덱스
    }

}

public class SaveData_Character //세이브 데이터에 저장되는 캐릭터의 정보(로스터 정보)
{
    public string Name; //플레이어가 커스터마이징한 이름
    public string State; //부상(경상, 중상, 쇼크 등), 출전, 대기, 미고용

    public string Job;
    public string Title;

    public int Level;
    public int Number; //캐릭터 고유 번호: 임시 변수

    public int Health; //현재 체력
    public int Morale; //현재 사기

    public int[] Skill_Levels = new int[Variable.SkillMany];

    public int EquipLevel;
    public int AccessoryIndex;
}


public class SaveData
{
    public int Day;

    //스테이지 진행도 저장 배열
    //1번 인덱스는 스테이지 번호, 2번 인덱스는 해당 스테이지에서 어디까지, 3번 인덱스는 확장성 임시
    public int[] StageProg = new int[4];

    //고용된 모든 영웅의 정보를 가지고 있는 배열
    public SaveData_Character[] Roster = new SaveData_Character[5];
    //원정대로 선택된 영웅의 로스터 인덱스 정보를 가지고 있는 배열
    public int[] Expeditions = new int[Variable.ExpeditionSize];

    public int Now_ExpeditionMany;

    public int Money;//재화
    public int[] Labor = new int[2];//노동력 현재 [0] = 현재 노동력  [1] = 최대 노동력
    public int GuildLV;//길드레벨
    public int ForgeLV;//대장간 레벨
    public int AccommdationLV;//숙소레벨
    public int TrainingGroundLV;//훈련소 레벨
    public int JobSeeker_reset;//길드 고용가능 영웅 유지용
    public int[] AccRoom = new int[7];//숙소 수 보다 많도록 내용은 로스터번호 인덱스 번호는 숙소 자리 번호
    public SaveData_Character[] JobSeeker_savedata = new SaveData_Character[16];//일단 최대치만큼

}

public static class JsonIO
{
    private static string JsonStr;
    public static SaveData save = new SaveData();

    public static void JsonTake()
    {
        JsonStr = File.ReadAllText(DataPathStringClass.DataPathString() + "/Json/SaveData.txt");

        save = JsonMapper.ToObject<SaveData>(JsonStr);
    }

    public static void JsonExport()
    {
        JsonStr = JsonMapper.ToJson(save);

        File.WriteAllText(DataPathStringClass.DataPathString() + "/Json/SaveData.txt", JsonStr);
    }

    public static void ResetSave()
    {
        SaveData save = new SaveData();

        JsonStr = JsonMapper.ToJson(save);

        File.WriteAllText(DataPathStringClass.DataPathString() + "/Json/SaveData.txt", JsonStr);
    }
}


public static class DataPathStringClass
{

    public static string DataPathString()
    {
#if UNITY_EDITOR
        string path = Application.dataPath;
        return path;
#elif UNITY_ANDROID
        
        string path = Application.persistentDataPath;

        if (!Directory.Exists(path + "/Save"))
        {
            

        }


        return path;
#endif
    }




}