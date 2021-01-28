using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField]
    private Vector3 offset;

    [SerializeField] 
    private Transform toFollow = null;

    private Transform myTransform = null;

    private void Awake()
    {
        myTransform = transform;
    }

    void FixedUpdate()
    {
        myTransform.position = new Vector3(toFollow.position.x, toFollow.position.y, myTransform.position.z) + offset;
    }
}
