using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveOnCollision : MonoBehaviour
{
    [SerializeField]
    private GameObject objToSetActive = null;
    
    [SerializeField]
    private bool shouldEndUpActive = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnTriggerEnter2D(collision.collider);   
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            objToSetActive.SetActive(shouldEndUpActive);
            Destroy(gameObject);
        }
    }
}
