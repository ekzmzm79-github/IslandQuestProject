using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using LitJson;


public class GuildUIController : MonoBehaviour
{
    public GameObject guildUI;
   
    public void ON()
    {
        guildUI.SetActive(true);
        
    }
    public void PressX()
    {
        guildUI.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
