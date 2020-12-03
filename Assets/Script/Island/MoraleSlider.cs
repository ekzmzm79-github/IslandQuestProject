using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using LitJson;

public class MoraleSlider : MonoBehaviour
{
    [SerializeField]
    Slider moraleslider;

    // Start is called before the first frame update
    void Start()
    {
        int morales_sum = 0, morales_max = 0;

        JsonIO.JsonTake();

        for (int i = 1; i <= JsonIO.save.Now_ExpeditionMany; i++)
        {
            if (JsonIO.save.Expeditions[i] == 0)
                continue;

            morales_sum += JsonIO.save.Roster[JsonIO.save.Expeditions[i]].Morale;

            Character character = new Character(JsonIO.save.Roster[JsonIO.save.Expeditions[i]].Job);

            morales_max += character.BasicSets[JsonIO.save.Roster[JsonIO.save.Expeditions[i]].Level].Morale;
        }

        moraleslider.maxValue = morales_max;
        moraleslider.value = morales_sum;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
