using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Transform myTransform;
    
    [SerializeField]
    private float speed = 0.0f;

    [SerializeField]
    private float stepDistance = 0.0f;
    private Vector3 lastStapPosition = Vector3.zero;

    [SerializeField]
    private Transform leftFoot = null;
    [SerializeField]
    private Transform rightFoot = null;

    [SerializeField]
    private GameObject stepPrototype = null;

    [SerializeField]
    private SpriteRenderer spriteRenderer = null;

    [SerializeField]
    private Transform hand = null;

    private Key currentKey;

    [SerializeField]
    private AudioSource audioSource = null;

    void Awake()
    {
        myTransform = transform;
        spriteRenderer.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        Vector3 movementVector = GetMovementVector();
        Vector3 increment = movementVector.normalized * speed * Time.deltaTime;
        myTransform.position += increment;
        myTransform.up = movementVector;

        if (Vector3.Distance(myTransform.position, lastStapPosition) > stepDistance)
        {
            lastStapPosition = myTransform.position;
            Instantiate(stepPrototype, myTransform.position, myTransform.rotation);
        }
    }

    private Vector3 GetMovementVector()
    {
        Vector3 movementVector = Vector3.zero;
        if (Input.GetKey("w"))
        {
            movementVector += Vector3.up;
        }
        if (Input.GetKey("s"))
        {
            movementVector -= Vector3.up;
        }
        if (Input.GetKey("d"))
        {
            movementVector += Vector3.right;
        }
        if (Input.GetKey("a"))
        {
            movementVector -= Vector3.right;
        }
        return movementVector;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        {
            Key key = collision.collider.GetComponent<Key>();
            if (key != null)
            {
                currentKey = key;
                key.Transform.position = hand.position;
                key.Transform.parent = hand;
                return;
            }
        }

        Door door = collision.collider.GetComponent<Door>();
        if (door != null && currentKey!=null && currentKey.DoorName == door.DoorName)
        {
            AudioSource spawned = Instantiate(audioSource, collision.contacts[0].point, Quaternion.identity);
            spawned.PlayOneShot(currentKey.UnlockSound);
            Destroy(door.gameObject);
            Destroy(currentKey.gameObject);
            currentKey = null;
            return;
        }
    }
}
