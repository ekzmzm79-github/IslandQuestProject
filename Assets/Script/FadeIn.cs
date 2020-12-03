using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{

    [SerializeField]
    Image Panel;
    [SerializeField]
    Canvas Blinder;
    [SerializeField]
    float speed = 0.08f;

    int Lowest_Rayer = -1;

    float fades = 1.0f;
    float time = 0;

    bool Trigger;

    // Use this for initialization
    void Start()
    {
        Blinder.sortingOrder = 10;
        Panel.color = new Color(0, 0, 0, 255);
    }

    // Update is called once per frame
    void Update()
    {
        if (Trigger)
        {
            //Debug.Log(fades);

            if (fades <= 0)
            {

                time = 0;
                Blinder.sortingOrder = Lowest_Rayer;
                Trigger = false;
            }



            time += Time.deltaTime;

            if (fades > 0 && time >= 0.1f)
            {
                fades -= speed;
                Panel.color = new Color(0, 0, 0, fades);
                time = 0;
            }




        }
    }

    public void TriggerOn()
    {
        Blinder.sortingOrder = 10;
        Trigger = true;
        
    }


}
