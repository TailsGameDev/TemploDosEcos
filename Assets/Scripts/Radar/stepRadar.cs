using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class stepRadar : MonoBehaviour
{

    public GameObject radarObject;
    public Sprite leftFootSprite = null;
    public Sprite rightFootSprite = null;
    private SpriteRenderer rend = null;
    private float radarScale = 1;    
    private Vector3 stepDirection = Vector3.zero;
    private bool stepEven = false;

    [SerializeField]
    private Light2D light2D = null;

    void Values(float radarRange)
    {
       radarScale = radarRange;
        light2D.pointLightOuterRadius = radarRange;
    }

    void Even(bool even)
    {
       stepEven = even;
    }

    void Direction(Vector3 movementVector)
    {
       stepDirection = movementVector;
    }

    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        GameObject radar = Instantiate(radarObject, transform.position, Quaternion.identity) as GameObject;
        radar.SendMessage("Values", radarScale);
        transform.up = stepDirection;
        if(stepEven){
            rend.sprite = leftFootSprite;
        }
        else{
            rend.sprite = rightFootSprite;
        }
    }
}
