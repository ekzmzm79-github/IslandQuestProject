using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using LitJson;

public class TrainingGroundContoller : MonoBehaviour
{

    [SerializeField]
    GameObject TrainingGroundUI;


    public void ON()
    {
        GameObject.Find("territory").SendMessage("MercenaryDataSort");
        TrainingGroundUI.SetActive(true);
        GameObject.Find("MercenaryScroll_TrainingGround").SendMessage("Resize_TrainingGrounMerScroll");
    }
    public void PressX()
    {
        TrainingGroundUI.SetActive(false);
    }
}
