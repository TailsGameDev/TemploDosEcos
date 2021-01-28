using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyItselfInTime : MonoBehaviour
{
    [SerializeField]
    private float timeToDestruction = 0.0f;

    private WaitForSeconds waitForDestruction;

    private void Awake()
    {
        waitForDestruction = new WaitForSeconds(timeToDestruction);
    }

    private IEnumerator Start()
    {
        yield return waitForDestruction;
        Destroy(gameObject);
    }
}
