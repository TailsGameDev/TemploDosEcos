using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class volumeOnStep : MonoBehaviour
{

    private AudioSource audioObject;
    private float radarScale = 1;

    void Values(float radarRange)
    {
       radarScale = radarRange;
    }

    void Start()
    {
        audioObject = GetComponent<AudioSource>();
        audioObject.volume = radarScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
