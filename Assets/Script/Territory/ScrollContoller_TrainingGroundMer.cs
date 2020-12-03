using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using LitJson;


public class ScrollContoller_TrainingGroundMer : MonoBehaviour
{
    [SerializeField]
    ScrollRect scrollRect; // 스크롤 
    [SerializeField]
    GameObject[] MerObj = new GameObject[31];
    // Start is called before the first frame update
    void Start()
    {
        Resize_TrainingGrounMerScroll();

    }
    public void Resize_TrainingGrounMerScroll()
    {
        GameObject.Find("territory").SendMessage("MercenaryDataSort");
        float width = 0.0f;
        float height = 550 + (275 * (GlobalVariable.MercenaryCount - 2));

        scrollRect = GetComponent<ScrollRect>();
        scrollRect.content.sizeDelta = new Vector2(width, height);

        for (int i = 0; i < GlobalVariable.MercenaryCount; i++)
        {
            MerObj[i].SetActive(true);
            MerObj[i].SendMessage("Resize_TrainingGround");

        }
        for (int i = GlobalVariable.MercenaryCount; i < 6; i++)
        {
            MerObj[i].SetActive(false);

        }
    }
}
