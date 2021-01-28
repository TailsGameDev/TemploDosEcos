using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Transform myTransform;
    
    // movement
    [SerializeField]
    private float speed = 0.0f;
    private Vector2 faceDirection;

    // step
    [SerializeField]
    private float stepDistance = 0.0f;
    private Vector3 lastStapPosition = Vector3.zero;
    [SerializeField]
    private Transform leftFoot = null;
    [SerializeField]
    private Transform rightFoot = null;
    private int currentStep = 0;
    [SerializeField]
    private float longStepInterval = 4;
    [SerializeField]
    private float minStepRange = 2;
    [SerializeField]
    private float maxStepRange = 3;

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

        //face direction
        if(movementVector.x != 0 || movementVector.y != 0){
            faceDirection = movementVector;
        }
        myTransform.up = faceDirection;

        // step generator
        if (Vector3.Distance(myTransform.position, lastStapPosition) > stepDistance)
        {
            currentStep++;
            lastStapPosition = myTransform.position;
            GameObject step = Instantiate(stepPrototype, myTransform.position, myTransform.rotation);

            // envia valor do tamanho para o radar a cada N(longStepInterval) passos
            if(currentStep == longStepInterval)
            {
                step.SendMessage("Values", maxStepRange);
                currentStep = 0;
            }
            else{
                step.SendMessage("Values", minStepRange);
            }
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
