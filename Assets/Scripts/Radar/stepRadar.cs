using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stepRadar : MonoBehaviour
{

    public GameObject radarObject;
    private float radarScale = 1;

    void Values(float radarRange)
    {
       radarScale = radarRange;
    }

    void Start()
    {
        GameObject radar = Instantiate(radarObject, transform.position, Quaternion.identity) as GameObject;
        radar.SendMessage("Values", radarScale);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
