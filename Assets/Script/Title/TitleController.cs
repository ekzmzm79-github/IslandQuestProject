using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using LitJson;
public class TitleController : MonoBehaviour
{
    [SerializeField]
    FadeOut fadeout;
    [SerializeField]
    FadeIn fadein;

    // Start is called before the first frame update
    void Start()
    {
        fadein.TriggerOn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Click_Start()
    {
        transform.GetComponent<AudioSource>().Play();
        fadeout.TriggerOn();
        StartCoroutine("Delayer");
        
    }


    IEnumerator Delayer()
    {
        yield return new WaitForSeconds(4.0f);


        Debug.Log("시작!");
        SceneManager.LoadScene("TerritoryMain");
    }
}
