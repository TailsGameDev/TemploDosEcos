using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField]
    private string doorName = null;

    [SerializeField]
    private Collider2D[] allColliders = null;

    private Transform myTransform;
    private BoxCollider2D myCollider;

    public Transform Transform { get => myTransform; }
    public string DoorName { get => doorName; }
    public BoxCollider2D Collider { get => myCollider; }

    private void Awake()
    {
        myTransform = transform;
        myCollider = GetComponent<BoxCollider2D>();
    }

    public void DestroyItselfIfNeeded()
    {
        Destroy(gameObject);
    }

    public void SetAllCollidersEnable(bool shouldEnable)
    {
        for (int c = 0; c < allColliders.Length; c++)
        {
            allColliders[c].enabled = shouldEnable;
        }
    }
}
