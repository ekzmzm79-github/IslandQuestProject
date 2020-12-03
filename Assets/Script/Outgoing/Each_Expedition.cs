using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;
using LitJson;

public class Each_Expedition : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField]
    RosterScroll rosterScroll;



    // Start is called before the first frame update
    void Start()
    {
        //rosterScroll = GameObject.Find("RosterListPanel").GetComponent<RosterScroll>();


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    static Vector2 defaultposition, PositionOffset;
    int Expedition_Number = 0, Des_Expedition_Number = 0;
    bool inout_Trigger;

    public void OnBeginDrag(PointerEventData eventData)
    {

        Expedition_Number = Convert.ToInt32(Regex.Replace(gameObject.name, @"\D", ""));

        defaultposition = transform.position;
        PositionOffset = transform.position - Input.mousePosition;
        transform.SetAsLastSibling();
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 currentPos = new Vector2(Input.mousePosition.x + PositionOffset.x, defaultposition.y);
        transform.position = currentPos;
    }
    public void OnEndDrag(PointerEventData eventData)
    {

        transform.position = defaultposition;

        if(inout_Trigger && Des_Expedition_Number !=0)
        {
            Debug.Log("스왑 실행!");
            transform.parent.GetComponent<Expedition_Setting_Controller>().Swap_Expeditions(Expedition_Number, Des_Expedition_Number);

        }

        Expedition_Number = 0;
        Des_Expedition_Number = 0;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if ( collision.tag == "Expedition_EachChar")
        {
            inout_Trigger = true;

            Des_Expedition_Number = Convert.ToInt32(Regex.Replace(collision.gameObject.name, @"\D", ""));

        }


    }

    void OnTriggerExit2D(Collider2D collision)
    {
        
       inout_Trigger = false;
        
    }

    public void Click_Cancel()
    {
        transform.parent.GetComponent<AudioSource>().Play();

        int num = Convert.ToInt32(Regex.Replace(gameObject.name, @"\D", ""));

        JsonIO.JsonTake();

        JsonIO.save.Expeditions[num] = 0;
        JsonIO.save.Now_ExpeditionMany--;

        JsonIO.JsonExport();

        transform.parent.GetChild(0).GetChild(num-1).gameObject.SetActive(true);
        gameObject.SetActive(false);

        rosterScroll.SendMessage("RosterScroll_Setting");

        

    }
}
