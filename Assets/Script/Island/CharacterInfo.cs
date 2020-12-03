using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using LitJson;

public class CharacterInfo : MonoBehaviour
{
    [SerializeField]
    Canvas CharacterInfoCanvas;

    [SerializeField]
    Sprite[] icon_candidates = new Sprite[Variable.JobsMany];

    [SerializeField]
    Button cha_portrait;
    [SerializeField]
    Slider cha_health, cha_morale;
    [SerializeField]
    Text cha_name, cha_title, cha_job, cha_level;
    [SerializeField]
    Text cha_will, cha_attact, cha_accuracy, cha_speed, cha_armor, cha_avoidance;

    [SerializeField]
    Image[] Skill_Icons = new Image[Variable.SkillMany];
    [SerializeField]
    Image EquipIcons;
    [SerializeField]
    Image AccessoryIcons;

    [SerializeField]
    Text ExplainText;

    private int SelectedExpeditionNum;

    // Start is called before the first frame update
    void Start()
    {
        CharacterInfoCanvas.sortingOrder = -1;
        SelectedExpeditionNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCharacterInfo(int num)
    {
        JsonIO.JsonTake();


        SelectedExpeditionNum = num;


        Character character = new Character(JsonIO.save.Roster[SelectedExpeditionNum].Job);
        int lev = JsonIO.save.Roster[SelectedExpeditionNum].Level;

        cha_portrait.image.sprite = character.BasicSets[lev].Portrait_Vertical;

        cha_health.maxValue = character.BasicSets[lev].Health;
        cha_health.value = JsonIO.save.Roster[SelectedExpeditionNum].Health;
        cha_morale.maxValue = character.BasicSets[lev].Morale;
        cha_morale.value = JsonIO.save.Roster[SelectedExpeditionNum].Morale;

        cha_name.text = "이름: " + JsonIO.save.Roster[SelectedExpeditionNum].Name;
        cha_title.text = "칭호: " + JsonIO.save.Roster[SelectedExpeditionNum].Title;
        cha_job.text = "직업: " + JsonIO.save.Roster[SelectedExpeditionNum].Job;
        cha_level.text = "레벨: " + JsonIO.save.Roster[SelectedExpeditionNum].Level.ToString();

        cha_will.text = "의지: " + character.BasicSets[lev].Will;
        cha_attact.text = "공격력: " + character.EquipmentInfo[JsonIO.save.Roster[SelectedExpeditionNum].EquipLevel].Attack;
        cha_accuracy.text = "정확도: " + character.EquipmentInfo[JsonIO.save.Roster[SelectedExpeditionNum].EquipLevel].Accuracy;
        cha_speed.text = "속도: " + character.EquipmentInfo[JsonIO.save.Roster[SelectedExpeditionNum].EquipLevel].Speed;
        cha_armor.text = "방어력: " + character.EquipmentInfo[JsonIO.save.Roster[SelectedExpeditionNum].EquipLevel].Armor;
        cha_avoidance.text = "회피: " + character.EquipmentInfo[JsonIO.save.Roster[SelectedExpeditionNum].EquipLevel].Avoidance;

        for (int i = 1; i < Variable.SkillMany; i++)
            Skill_Icons[i].sprite = character.SkillsInfo[i,JsonIO.save.Roster[SelectedExpeditionNum].Skill_Levels[SelectedExpeditionNum]].Skill_Img;

        EquipIcons.sprite = character.EquipmentInfo[JsonIO.save.Roster[SelectedExpeditionNum].EquipLevel].Equip_Img;
        //AccessoryIcons.sprite = character.;

        ExplainText.text = character.BasicSets[JsonIO.save.Roster[SelectedExpeditionNum].Number].CharacterIntroText;

        

        CharacterInfoCanvas.sortingOrder = 2;
    }

    public void ChangExplainText(int btn_num)
    {
        Debug.Log(SelectedExpeditionNum);

        Character character = new Character(JsonIO.save.Roster[SelectedExpeditionNum].Job);

        if (btn_num == -3)
        {
            ExplainText.text = character.BasicSets[SelectedExpeditionNum].CharacterIntroText;
        }
        else if (btn_num == -2)
        {
            int eq_level = JsonIO.save.Roster[SelectedExpeditionNum].EquipLevel;

            if (eq_level == 0)
                ExplainText.text = "장비가 없습니다.";
            else
                ExplainText.text = character.EquipmentInfo[eq_level].ExplainText;
        }
        else if (btn_num == -1)
        {
         /*
            int ac_index = JsonIO.save.Roster[JsonIO.save.Expeditions[SelectedExpeditionNum]].AccessoryIndex;

            if (ac_index == 0)
                ExplainText.text = "없음";
            else
                ExplainText.text = character.BasicSet.AccessoryText;
         */
        }
        else
        {
            int lev = JsonIO.save.Roster[SelectedExpeditionNum].Skill_Levels[btn_num];

            if (lev == 0)
                ExplainText.text = "아직 배우지 못 했습니다.";
            else
                ExplainText.text = character.SkillsInfo[btn_num, lev].ExplainText + "\n레벨은 " + lev + "입니다.";
        }
    }

    public void ClickExit()
    {
        SelectedExpeditionNum = 0;
        CharacterInfoCanvas.sortingOrder = -1;
    }
}
