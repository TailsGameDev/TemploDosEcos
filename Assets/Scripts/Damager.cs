using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField]
    private float damageAmount = 0.0f;

    public float DamageAmount { get => damageAmount; }
}
