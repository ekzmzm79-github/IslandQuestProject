using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class StageDetail_ProgButton : MonoBehaviour
{
    [SerializeField]
    StageDetail stageDetail;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickProg_Button()
    {
        transform.parent.GetComponent<AudioSource>().Play();

        Button btn = GetComponent<Button>();
        int num = Convert.ToInt32(Regex.Replace(btn.name, @"\D", ""));

        //스테이지 내에서 누른 버튼의 번호를 전송함
        stageDetail.SendMessage("SetEnemy", num);

    }
}
