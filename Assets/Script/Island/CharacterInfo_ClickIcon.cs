using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.UI;


public class CharacterInfo_ClickIcon : MonoBehaviour
{
    [SerializeField]
    CharacterInfo characterInfo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickIcon()
    {
        

        //클릭한 스킬 아이콘 찾기
        Button btn = GetComponent<Button>();

        if(btn.name== "PortraitIcon")
        {
            Debug.Log("초상화 클릭");

            characterInfo.SendMessage("ChangExplainText", -3);
        }
        else if(btn.name == "EquipIcon")
        {
            Debug.Log("장비 클릭");

            characterInfo.SendMessage("ChangExplainText", -2);
        }
        else if(btn.name == "AccessoryIcon")
        {
            Debug.Log("악세사리 클릭");

            characterInfo.SendMessage("ChangExplainText", -1);
        }
        else
        {
            int num = Convert.ToInt32(Regex.Replace(btn.name, @"\D", ""));
            Debug.Log(num);

            characterInfo.SendMessage("ChangExplainText", num);
        }
        


    }
}
