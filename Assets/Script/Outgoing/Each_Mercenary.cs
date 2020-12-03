using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;
using LitJson;

public class Each_Mercenary : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{

    Expedition_Setting_Controller expedition_Setting_Controller;

    // Start is called before the first frame update
    void Start()
    {
        expedition_Setting_Controller = transform.parent.parent.parent.parent.GetComponent<RosterScroll>().expedition_Setting_Controller;

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Click_SelButton()
    {
        transform.parent.parent.parent.parent.GetComponent<AudioSource>().Play();

        //해당 프리팹 블라인더 활성화
        transform.Find("BlinderImage").gameObject.SetActive(true);

        //세이브 데이터의 원정대 정보 업데이트

        JsonIO.JsonTake();

        if (JsonIO.save.Now_ExpeditionMany >= 4)
        {
            //원정대 숫자는 최대 4명
            Debug.Log("숫자 초과!");
        }
        else
        {
            for (int i = Variable.ExpeditionSize -1 ; i > 0  ; i--)
            {
                if (JsonIO.save.Expeditions[i] == 0)
                {
                    //빈 자리 찾음
                    JsonIO.save.Expeditions[i] = int.Parse(transform.GetChild(4).GetComponent<Text>().text);
                    JsonIO.save.Now_ExpeditionMany++;


                    JsonIO.JsonExport();
                    //원정대 창 갱신 요청
                    expedition_Setting_Controller.SendMessage("Expedition_Setting");


                    break;
                }
            }
        }

        transform.gameObject.GetComponent<Each_Mercenary>().enabled = false;
        transform.gameObject.GetComponent<BoxCollider2D>().enabled = false;

    }


    static Vector2 defaultposition, PositionOffset;
    int Roster_Number = 0, Des_Roster_Number = 0;
    bool inout_Trigger;

    public void OnBeginDrag(PointerEventData eventData)
    {
        return;
        Roster_Number = Convert.ToInt32(Regex.Replace(gameObject.name, @"\D", ""));

        defaultposition = transform.position;
        PositionOffset = transform.position - Input.mousePosition;
        
        transform.SetAsLastSibling();

    }

    public void OnDrag(PointerEventData eventData)
    {
        return;
        Vector2 currentPos = new Vector2(defaultposition.x, Input.mousePosition.y + PositionOffset.y);
        transform.position = currentPos;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        return;
        transform.position = defaultposition;



        if (inout_Trigger && Des_Roster_Number != 0)
        {
            transform.parent.parent.parent.parent.GetComponent<RosterScroll>().Swap_List(Roster_Number, Des_Roster_Number);

        }
        Roster_Number = 0;
        Des_Roster_Number = 0;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Roster_EachChar")
        {
            inout_Trigger = true;
            Des_Roster_Number = Convert.ToInt32(Regex.Replace(collision.gameObject.name, @"\D", ""));
        }

    }

    void OnTriggerExit2D(Collider2D collision)
    {
        inout_Trigger = false;

    }

}
