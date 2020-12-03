using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeObject : MonoBehaviour
{
    private Vector3 originPosition;
    private Quaternion originRotation;
    float shake_decay;
    float shake_intensity;

    private bool shake_trigger=false;

    void Start()
    {

    }
    
    void Update()
    {
        if (shake_trigger)
        {
            transform.position = originPosition + Random.insideUnitSphere * shake_intensity;
            transform.rotation = new Quaternion(
            originRotation.x + Random.Range(-shake_intensity, shake_intensity) * 0.1f,
            originRotation.y + Random.Range(-shake_intensity, shake_intensity) * 0.1f,
            originRotation.z + Random.Range(-shake_intensity, shake_intensity) * 0.1f,
            originRotation.w + Random.Range(-shake_intensity, shake_intensity) * 0.1f);
            shake_intensity -= shake_decay;

            if (shake_intensity <= 0)
                shake_trigger = false;

        }
    }

    public void Shake()
    {
        originPosition = transform.position;
        originRotation = transform.rotation;
        shake_intensity = 0.2f;
        shake_decay = 0.002f;

        shake_trigger = true;
        Shake_Sound();
    }

    public void Shake_Sound()
    {
        transform.GetComponent<AudioSource>().Play();
    }
}
