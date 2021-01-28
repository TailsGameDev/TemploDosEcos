using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private string doorName = null;

    public string DoorName { get => doorName; }
}
