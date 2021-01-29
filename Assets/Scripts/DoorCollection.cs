using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCollection : MonoBehaviour
{
    [SerializeField]
    private Door[] doors = null;

    public Door[] Doors { get => doors; }
}
