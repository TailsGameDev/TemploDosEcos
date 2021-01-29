using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleOnAwake : MonoBehaviour
{
    [SerializeField]
    private Vector3 newScale = Vector3.one;

    private void Awake()
    {
        transform.parent = null;
        transform.localScale = newScale;
    }
}
