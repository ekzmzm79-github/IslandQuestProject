using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using LitJson;

public class AccommodationContoller : MonoBehaviour
{
    ScrollRect scrollRect;//스크롤쪽에 필요한부분
    [SerializeField]
    GameObject MersenaryPrefab;// 고용된 영웅 프리펩
    [SerializeField]
    GameObject content; // 콘텐트 


    [SerializeField]
    int StartSign;// 스타트함수 제어용

    [SerializeField]
    GameObject accommodationUI;
    [SerializeField]


    public void ON()
    {
        accommodationUI.SetActive(true);
        GameObject.Find("MercenaryScroll_Acc").SendMessage("Resize_AccMerScroll");
    }
    public void PressX()
    {
        accommodationUI.SetActive(false);
    }
    // Start is called before the first frame update
    /*
    void Start()
    {
        if (StartSign == 100)
        {
            int MersenaryCount = Variable.RosterSize;

            int MerCount = 0;
            for (int i = 1; i < MersenaryCount; i++)//고용된 머시너리 수 확인
            {
                if (JsonIO.save.Roster[i].Number != 0)
                {
                    MerCount++;//머시너리 수
                }
            }
            float temp1 = MerCount;//고용가능한 영웅 수 나중에 변수로 바꿔줘야함  길드에 필요한부분

            float width = 0.0f;
            float height = 550 + (275 * (temp1 - 2));

            scrollRect = GetComponent<ScrollRect>();
            scrollRect.content.sizeDelta = new Vector2(width, height);

            for (int i = 1, j = 0; i < MersenaryCount; i++, j++)
            {
                if (JsonIO.save.Roster[i].Number == 0)
                {
                    j--;
                    continue;
                }

                GlobalVariable.Acc_Mersenary[i] = (GameObject)Instantiate(
                   MersenaryPrefab,
                   transform.position + new Vector3(20, 202 - (j * 275), 0),
                   Quaternion.identity
                   );

                GlobalVariable.Acc_Mersenary[i].transform.parent = content.transform;

                JsonIO.JsonExport();

            }
            GameObject.Find("MercenaryScroll_Acc").SendMessage("Acc_Resize_mersenary");
        }
        JsonIO.JsonExport();
    }
    */
    public void Acc_Resize_mersenary()
    {
        JsonIO.JsonTake();
        int MersenaryCount2 = 0;


        for (int k = 0; k < Variable.RosterSize; k++)
        {
            if (JsonIO.save.Roster[k].Number == 0)
            {
                continue;
            }
            MersenaryCount2++;
        }

        float temp1 = MersenaryCount2;
        int i = 0, j = 0;
        float width = 0.0f;
        float height = 550 + (275 * (temp1 - 2));
        scrollRect = GetComponent<ScrollRect>();
        scrollRect.content.sizeDelta = new Vector2(width, height);
        scrollRect.content.position = new Vector3(150, -5, 0);


        for (i = 1, j = 0; i < Variable.RosterSize; i++, j++)
        {
            if (JsonIO.save.Roster[i].Number == 0)
            {
                Debug.Log("스킾되는것" + i);
                j--;
                continue;
            }

            GlobalVariable.Acc_Mersenary[i].transform.position = new Vector3(546, -158- (j * 275), 0);
        }

    }
    // Update is called once per frame
    void Update()
    {

    }
    
}
