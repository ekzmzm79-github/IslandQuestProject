using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    int Highest_Rayer = 10;

    [SerializeField]
    Image Panel;
    [SerializeField]
    Canvas Blinder;
    [SerializeField]
    float speed = 0.08f;


    float fades = 0;
    float time = 0;

    bool Trigger;
    

    // Use this for initialization
    void Start()
    {
        Trigger = false;
        Blinder.sortingOrder = -1;
        Panel.color = new Color(0, 0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Trigger)
        {
            //Debug.Log(fades);

            if (fades >= 1.0f)
            {
                time = 0;
            }


            Blinder.sortingOrder = Highest_Rayer;
            time += Time.deltaTime;

            if (fades < 1.0f && time >= 0.1f)
            {
                fades += speed;
                Panel.color = new Color(0, 0, 0, fades);
                time = 0;
            }




        }


    }


    public void TriggerOn()
    {
        Trigger = true;
    }
}
