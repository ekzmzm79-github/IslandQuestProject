using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using LitJson;

public class ForgeContoller : MonoBehaviour
{
    [SerializeField]
    GameObject ForgeUI;
    

    public void ON()
    {
        GameObject.Find("territory").SendMessage("MercenaryDataSort");
        ForgeUI.SetActive(true);
        GameObject.Find("MercenaryScroll_Forge").SendMessage("Resize_ForgeMerScroll");
    }
    public void PressX()
    {
        ForgeUI.SetActive(false);
    }
 
}
