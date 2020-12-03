using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using LitJson;

public class Outgoing_Controller : MonoBehaviour
{
    [SerializeField]
    Canvas Outgoing_Canvas;

    [SerializeField]
    RosterScroll rosterScroll;

    [SerializeField]
    Expedition_Setting_Controller expedition_Setting_Controller;

    [SerializeField]
    FadeOut fadeOut;

    // Start is called before the first frame update
    void Start()
    {
        Outgoing_Canvas.sortingOrder = -1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //출정하기 버튼을 눌렀을 때, 실행됨 (전체적인 세팅 담당)
    public void Set_Outgoing()
    {
        Outgoing_Canvas.sortingOrder = 1;

        //로스터 스크롤의 세팅
        expedition_Setting_Controller.SendMessage("Reset_ExpeditionList");
        rosterScroll.SendMessage("RosterScroll_Setting");
        
    }

    public void Click_Start()
    {
        JsonIO.JsonTake();

        //압축하기
        int[] temp = new int[5];
        int j = 4;

        for (int i = Variable.ExpeditionSize - 1; i > 0; i--)
        {

            if (JsonIO.save.Expeditions[i] == 0)
            {
                Debug.Log("컨티뉴!");
                continue;
            }

            Debug.Log("i: " + i + ", j:" + j  + JsonIO.save.Expeditions[i]);

            temp[j] = JsonIO.save.Expeditions[i];
            JsonIO.save.Expeditions[i] = 0;
            j--;
        }


        for (int i = 4; i > 0; i--)
        {

            if (i < j + 1)
                break;

            JsonIO.save.Expeditions[i] = temp[i];
        }

        JsonIO.JsonExport();


        transform.GetComponent<AudioSource>().Play();
        fadeOut.TriggerOn();
        StartCoroutine("Delayer");

        

        //SceneManager.LoadScene("IslandMain");
    }

    public void Click_Exit()
    {
        
        rosterScroll.SendMessage("Delete_List");

        Outgoing_Canvas.sortingOrder = -1;
    }

    IEnumerator Delayer()
    {
        yield return new WaitForSeconds(3.5f);


        Debug.Log("시작!");
        SceneManager.LoadScene("IslandMain");
    }

}
