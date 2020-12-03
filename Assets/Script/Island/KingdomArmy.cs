using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingdomArmy : MonoBehaviour
{
    [SerializeField]
    Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        canvas.sortingOrder = -1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenKingdomInfo()
    {
        canvas.sortingOrder = 1;
    }

    public void ExitKingdomInfo()
    {
        canvas.sortingOrder = -1;
    }

}
