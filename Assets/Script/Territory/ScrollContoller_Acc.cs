using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using LitJson;

public class ScrollContoller_Acc : MonoBehaviour
{
    [SerializeField]
    GameObject[] Room = new GameObject[6];
    [SerializeField]
    Text[] name = new Text[6];

    void Resize_AccRoom()
    {
        Debug.Log("뭐함");
        JsonIO.JsonTake();
        for (int i = 0; i < 6; i++)
        {

            if (JsonIO.save.AccRoom[i] == 0)
                Room[i].SetActive(false);
            else
            {
                Room[i].SetActive(true);
                switch (JsonIO.save.Roster[JsonIO.save.AccRoom[i]].Number)
                {
                    case 1:
                        Room[i].GetComponent<Image>().sprite =  Resources.Load<Sprite>("Characters/CruPortrait");
                        name[i].text = JsonIO.save.Roster[JsonIO.save.AccRoom[i]].Name;
                        break;
                   case 2:
                        Room[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Characters/PirPortrait");
                        name[i].text = JsonIO.save.Roster[JsonIO.save.AccRoom[i]].Name;
                        break;
                 }
            }
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        Resize_AccRoom();
    }

    
}
