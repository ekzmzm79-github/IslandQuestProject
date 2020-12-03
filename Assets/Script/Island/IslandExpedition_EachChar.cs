using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;
using LitJson;

public class IslandExpedition_EachChar : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    CharacterInfo characterInfo;

    // Start is called before the first frame update
    void Start()
    {
        characterInfo = transform.parent.GetComponent<Expedition>().characterInfo;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickPortrait()
    {
        JsonIO.JsonTake();

        int num = Convert.ToInt32(Regex.Replace(gameObject.name, @"\D", ""));

        characterInfo.SendMessage("SetCharacterInfo", JsonIO.save.Expeditions[num]);

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

        if (inout_Trigger && Des_Expedition_Number != 0)
        {
            Debug.Log("스왑 실행!");
            transform.parent.GetComponent<Expedition>().Swap_Expeditions(Expedition_Number, Des_Expedition_Number);

        }

        Expedition_Number = 0;
        Des_Expedition_Number = 0;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Expedition_EachChar")
        {
            inout_Trigger = true;

            Des_Expedition_Number = Convert.ToInt32(Regex.Replace(collision.gameObject.name, @"\D", ""));

        }


    }

    void OnTriggerExit2D(Collider2D collision)
    {

        inout_Trigger = false;

    }
}
