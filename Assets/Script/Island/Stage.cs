using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class Stage : MonoBehaviour
{
    [SerializeField]
    StageDetail detailScript;
    [SerializeField]
    Canvas detailCanvas;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectStage()
    {
        transform.parent.GetComponent<AudioSource>().Play();
        //클릭한 스테이지 번호 찾기
        Button btn = GetComponent<Button>();
        int num = Convert.ToInt32(Regex.Replace(btn.name, @"\D", ""));

        string name = "Error";

        switch (num)
        {
            case 1:
                name = "Elf";
                break;

            case 2:
                name = "Orc";
                break;

            case 3:
                name = "Lizard";
                break;

            case 4:
                name = "Undead";
                break;
        }

        StageInfo stageInfo = new StageInfo(name);

        //레이어 순위 상승 시켜서 출력
        detailCanvas.sortingOrder = 1;


        detailScript.SendMessage("StageProgMapping", stageInfo);
    }

}
