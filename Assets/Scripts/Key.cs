using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField]
    private string doorName = null;

    private Transform myTransform;
    private BoxCollider2D myCollider;

    public Transform Transform { get => myTransform; }
    public string DoorName { get => doorName; }
    public BoxCollider2D Collider { get => myCollider; }

    void Awake()
    {
        myTransform = transform;
        myCollider = GetComponent<BoxCollider2D>();
    }
}
