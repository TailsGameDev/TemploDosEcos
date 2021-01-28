using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class visibleOnRadarMask : MonoBehaviour
{

    SpriteRenderer rend;
    private Sprite defaultSprite;
    
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        defaultSprite = rend.sprite;
        rend.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;                                                 
    }
}
