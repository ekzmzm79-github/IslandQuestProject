using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class StageDetail_EnemyButton : MonoBehaviour
{
    [SerializeField]
    Text ExplainText;
    [SerializeField]
    Text enemy_name, enemy_level, enemy_tirbe;
    [SerializeField]
    Text enemy_health, enemy_will, enemy_attact, enemy_accuracy, enemy_speed, enemy_armor, enemy_avoidance;
    

    private Enemy enemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetEnemyInfo(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void ClickEnemyButton()
    {
        Button btn = GetComponent<Button>();
        int num = Convert.ToInt32(Regex.Replace(btn.name, @"\D", ""));


        ExplainText.text = enemy.EnemyInfo[1].IntroText;
        enemy_name.text = "이름: " + enemy.EnemyInfo[1].Name;
        enemy_level.text = "레벨: " + enemy.EnemyInfo[1].level.ToString();
        enemy_tirbe.text = "종족" + enemy.EnemyInfo[1].Trible;
        enemy_health.text = "체력" + enemy.EnemyInfo[1].Health.ToString();
        enemy_will.text = "의지" + enemy.EnemyInfo[1].Will.ToString();
        enemy_attact.text = "공격력" + enemy.EnemyInfo[1].Attack.ToString();
        enemy_accuracy.text = "정확도" + enemy.EnemyInfo[1].Accuracy.ToString();
        enemy_speed.text = "속도" + enemy.EnemyInfo[1].Speed.ToString();
        enemy_armor.text = "방어력" + enemy.EnemyInfo[1].Armor.ToString();
        enemy_avoidance.text = "회피" + enemy.EnemyInfo[1].Avoidance.ToString();

        //현재 엘프 스테이지 레벨 1로 고정이기 때문에 상수 1 사용됨.

    }
}
