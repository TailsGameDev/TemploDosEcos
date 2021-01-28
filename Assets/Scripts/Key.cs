using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField]
    private string doorName = null;

    [SerializeField]
    private AudioClip unlockSound = null;

    private Transform myTransform;

    public Transform Transform { get => myTransform; }
    public string DoorName { get => doorName; }
    public AudioClip UnlockSound { get => unlockSound; }

    void Awake()
    {
        myTransform = transform;
    }
}
