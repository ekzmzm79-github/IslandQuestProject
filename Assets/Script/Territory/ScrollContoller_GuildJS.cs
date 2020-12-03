using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using LitJson;


public class ScrollContoller_GuildJS : MonoBehaviour
{

    [SerializeField]
    ScrollRect scrollRect; // 스크롤 

    public void Resize_JobSeeker()
    {
        
        float width = 0.0f;
        float height = 550 + (275 * (GlobalVariable.MercenaryCount - 2));

        scrollRect = GetComponent<ScrollRect>();
        scrollRect.content.sizeDelta = new Vector2(width, height);

        for (int i = 1; i < GlobalVariable.MercenaryCount; i++)
        {
            GameObject.Find("JobSeeker (" + i + ")").SetActive(true);
            GameObject.Find("JobSeeker (" + i + ")").SendMessage("Resize_JobSeeker");

        }
        for (int i = GlobalVariable.MercenaryCount+1; i < GlobalVariable.MaxjobSeekerIndex; i++)
        {
            GameObject.Find("JobSeeker (" + i + ")").SetActive(false);
        }

    }
    void Start()
    {
        GameObject.Find("territory").SendMessage("MercenaryDataSort");
        
        float width = 0.0f;
        float height = 550 + (275 * (GlobalVariable.MercenaryCount - 2));

        scrollRect = GetComponent<ScrollRect>();
        scrollRect.content.sizeDelta = new Vector2(width, height);

        for (int i = 1; i < GlobalVariable.MercenaryCount; i++)
        {
            GameObject.Find("Mercenary_Guild (" + i + ")").SetActive(true);
            GameObject.Find("Mercenary_Guild (" + i + ")").SendMessage("Resize_MerGuild");

        }
        for (int i = GlobalVariable.MercenaryCount + 1; i < 7; i++)
        {
            GameObject.Find("Mercenary_Guild (" + i + ")").SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
