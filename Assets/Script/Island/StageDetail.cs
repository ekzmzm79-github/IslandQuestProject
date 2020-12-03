using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class StageDetail : MonoBehaviour
{
    [SerializeField]
    AudioClip[] clips = new AudioClip[5];

    [SerializeField]
    AudioSource sound_left, sound_right;


    [SerializeField]
    Image back_top;
    [SerializeField]
    Sprite[] back_tops = new Sprite[5];

    [SerializeField]
    Image back_bottom;
    [SerializeField]
    Sprite[] back_bottoms = new Sprite[5];

    [SerializeField]
    Image back_right;
    [SerializeField]
    Sprite[] back_rights = new Sprite[5];

    [SerializeField]
    Image back_left;
    [SerializeField]
    Sprite[] back_lefts = new Sprite[5];


    [SerializeField]
    Text title;

    [SerializeField]
    Sprite[] backs = new Sprite[5];
    [SerializeField]
    Image background;

    [SerializeField]
    FadeOut fadeOut;
    [SerializeField]
    AudioSource backsound;

    [SerializeField]
    Sprite[] DetailButtonBack = new Sprite[4];
    [SerializeField]
    Button[] DetailButtons = new Button[14];
    [SerializeField]
    Button[] DetailEnemyButtons = new Button[Variable.Each_StageEnemyMax];

    [SerializeField]
    StageDetail_EnemyButton[] stageDetail_EnemyButton = new StageDetail_EnemyButton[Variable.Each_StageEnemyMax];

    [SerializeField]
    float move_speed = 1.0f;

    private bool move_trigger;
    private float fade = 0;

    private Canvas canvas;
    private int[] StageProg = new int[Variable.StageSize]; //임시
    private StageInfo temp_stageInfo = new StageInfo();

    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.sortingOrder = -1;

        for (int i = 1; i < Variable.Each_StageEnemyMax; i++)
            DetailEnemyButtons[i].image.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(move_trigger)
        {
            if (back_top.transform.localPosition.y <= 389 && back_bottom.transform.localPosition.y >= -457 && back_right.transform.localPosition.x <= 751 && back_left.transform.localPosition.x >= -704)
            {
                move_trigger = false;
                transform.GetChild(1).GetChild(0).GetComponent<AudioSource>().Play();
                transform.GetChild(1).GetChild(1).GetComponent<AudioSource>().Play();
                title.color = new Color(9 / 255f, 94 / 255f, 0, 255f);
            }
                

            
            if(back_top.transform.localPosition.y <= 389)
                back_top.transform.localPosition = new Vector2(back_top.transform.localPosition.x, 389.91f);
            else
                back_top.transform.localPosition = new Vector2(back_top.transform.localPosition.x, back_top.transform.localPosition.y - Time.deltaTime * move_speed * 100);

            if(back_bottom.transform.localPosition.y >= -457)
                back_bottom.transform.localPosition = new Vector2(back_bottom.transform.localPosition.x, -456.6625f);
            else
                back_bottom.transform.localPosition = new Vector2(back_bottom.transform.localPosition.x, back_bottom.transform.localPosition.y + Time.deltaTime * move_speed * 100);

            if(back_right.transform.localPosition.x <= 751)
                back_right.transform.localPosition = new Vector2(751f, back_right.transform.localPosition.y);
            else
                back_right.transform.localPosition = new Vector2(back_right.transform.localPosition.x - Time.deltaTime * move_speed * 100, back_right.transform.localPosition.y);

            if(back_left.transform.localPosition.x >= -704)
                back_left.transform.localPosition = new Vector2(-704f, back_left.transform.localPosition.y);
            else
                back_left.transform.localPosition = new Vector2(back_left.transform.localPosition.x + Time.deltaTime * move_speed * 100, back_left.transform.localPosition.y);

            
            fade += Time.deltaTime * move_speed * 0.1f;
            title.color = new Color(9 / 255f, 94 / 255f, 0, fade);
            
        }
    }
    
    void StageBackSetting(StageInfo stageInfo)
    {
        sound_left.clip = clips[stageInfo.Number];
        sound_right.clip = clips[stageInfo.Number];

        //스테이지 배경 및 타이틀 세팅
        title.text = stageInfo.Name;
        title.color = new Color(9/255f, 94/255f, 0f, 0f);

        background.sprite = backs[stageInfo.Number];

        back_top.sprite = back_tops[stageInfo.Number];
        back_bottom.sprite = back_bottoms[stageInfo.Number];
        back_right.sprite = back_rights[stageInfo.Number];
        back_left.sprite = back_lefts[stageInfo.Number];


        //각 스테이지별 세팅 하드 코딩
        switch (stageInfo.Number)
        {
            case 1:
                back_top.rectTransform.sizeDelta = new Vector2(back_top.rectTransform.sizeDelta.x, 300f);
                back_top.transform.localPosition = new Vector2(back_top.transform.localPosition.x, 690f);

                back_bottom.rectTransform.sizeDelta = new Vector2(back_bottom.rectTransform.sizeDelta.x, 165f);
                back_bottom.transform.localPosition = new Vector2(back_bottom.transform.localPosition.x, -622f);

                back_right.rectTransform.sizeDelta = new Vector2(415f, back_right.rectTransform.sizeDelta.y);
                back_right.transform.localPosition = new Vector2(1167f, back_right.transform.localPosition.y);

                back_left.rectTransform.sizeDelta = new Vector2(512f, back_left.rectTransform.sizeDelta.y);
                back_left.transform.localPosition = new Vector2(-1217f, back_left.transform.localPosition.y);


                break;
            case 2:

                break;
            case 3:

                break;
            case 4:

                break;
            case 5:

                break;
        }

        move_trigger = true;


    }


    void StageProgMapping(StageInfo stageInfo)
    {
        JsonIO.JsonTake();


        StageBackSetting(stageInfo);



        //클릭한 스테이지에 맞는 정보를 stageprog에 갱신시킴.
        int Save_StageNum = JsonIO.save.StageProg[1], Save_StageProgNum = JsonIO.save.StageProg[2];

        temp_stageInfo = stageInfo; //현재 이 스크립트에서 전역변수인 temp_stageInfo의 갱신

        for (int i =1; i<Variable.StageSize -1; i++)
        {
            if(stageInfo.Number < Save_StageNum) //모두 클리어 했음
            {
                if (i % 4 == 0)//클리어한 스테이지가 4의 배수 == 중간 거점
                {
                    DetailButtons[i].image.sprite = DetailButtonBack[2];

                }   
                else
                {
                    DetailButtons[i].image.sprite = DetailButtonBack[1];
                }
                    
            }
            else if(stageInfo.Number == Save_StageNum) //현재 진행 중인 스테이지
            {
                if(i<=Save_StageProgNum) //여기까진 클리어함
                {
                    if (i % 4 == 0)//클리어한 스테이지가 4의 배수 == 중간 거점
                    {
                        DetailButtons[i].image.sprite = DetailButtonBack[2];
                    }
                    else
                    {
                        DetailButtons[i].image.sprite = DetailButtonBack[1];
                    }
                }
                else
                {
                    DetailButtons[i].image.sprite = DetailButtonBack[3];
                }
            }
            else if(stageInfo.Number > Save_StageNum) //아직 개방되지 않은 스테이지
            {
                DetailButtons[i].image.sprite = DetailButtonBack[3];
            }
        }


    }

    public void SetEnemy(int num)
    {
        //StageDetail_ProgButton에서 num을 받음.(num은 스테이지에서 몇번째 칸을 눌렀는가를 알려주는 번호)

        JsonIO.JsonTake();
        //클릭한 스테이지에 맞는 정보를 stageprog에 갱신시킴.
        int Save_StageNum = JsonIO.save.StageProg[1], Save_StageProgNum = JsonIO.save.StageProg[2];


        if (num % 4 == 0) //선택한 칸이 중간 거점이다.
        {
            for (int i = 1; i < Variable.Each_StageEnemyMax; i++)
            {
                DetailEnemyButtons[i].enabled = false;
                DetailEnemyButtons[i].image.enabled = false;
            }
                

            //Debug.Log("중간 거점 선택함");

            return;
        }

        //현재 진행하지 못한 곳의 칸을 클릭했다.
        if (temp_stageInfo.Number > Save_StageNum)
        {
            //Debug.Log("진행 못한 스테이지 선택함");
            return;
        } 
        else if (temp_stageInfo.Number == Save_StageNum && num > Save_StageProgNum)
        {
            //Debug.Log("진행 못한 칸을 선택함");
            return;
        }
            

        for (int i = 1; i < Variable.Each_StageEnemyMax; i++)
        {
            DetailEnemyButtons[i].enabled = true;
            DetailEnemyButtons[i].image.enabled = true;

            Enemy enemy = new Enemy(temp_stageInfo.Stage[num].EnemyIndex[i]);
            DetailEnemyButtons[i].image.sprite = enemy.EnemyInfo[1].Portrait;
            //현재 엘프 스테이지가 1스테이지로 고정되어 있기때문에 가능한 코딩임
            //enemy.EnemyInfo[1].Portrait : 1은 레벨을 뜻하는데, 몇번째 칸에서 등장하느냐에 따라 레벨이 달라짐.

            stageDetail_EnemyButton[i].SendMessage("SetEnemyInfo", enemy);
        }

    }


    public void ClickBattle()
    {
        fadeOut.TriggerOn();
        transform.GetComponent<AudioSource>().Play();
        backsound.Pause();
        StartCoroutine("Delayer");

        //씬 이동 추가할것
    }
    public void ClickExit()
    {
        canvas.sortingOrder = -1;

        for (int i = 1; i < Variable.Each_StageEnemyMax; i++)
        {
            DetailEnemyButtons[i].enabled = false;
            DetailEnemyButtons[i].image.enabled = false;
        }
            
    }

    IEnumerator Delayer()
    {
        yield return new WaitForSeconds(3.5f);


        Debug.Log("시작!");
    }



}
