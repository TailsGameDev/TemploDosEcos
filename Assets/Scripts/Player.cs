using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Transform myTransform;
    
    // Movement
    [SerializeField]
    private float speed = 0.0f;

    // Step
    [SerializeField]
    private float stepDistance = 0.0f;
    private Vector3 lastStapPosition = Vector3.zero;
    [SerializeField]
    private Transform foot = null;
    private int currentStep = 0;
    [SerializeField]
    private float longStepInterval = 4;
    [SerializeField]
    private float minStepRange = 2;
    [SerializeField]
    private float maxStepRange = 3;
    private bool stepEven = false;

    [SerializeField]
    private GameObject stepPrototype = null;

    [SerializeField]
    private Animator animator = null;

    void Awake()
    {
        myTransform = transform;
        lastStapPosition = myTransform.position;
    }

    private void FixedUpdate()
    {
        Vector3 movementVector = GetMovementVector();

        // Set Position
        Vector3 increment = movementVector.normalized * speed * Time.deltaTime;
        myTransform.position += increment;

        bool hasInput = Vector3.SqrMagnitude(increment) > 0.001f;

        // Set Rotation
        if (hasInput)
        {
            transform.up = increment;
        }

        // Manage Animation
        if (hasInput)
        {
            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }

        // Step spawner
        if (Vector3.Distance(myTransform.position, lastStapPosition) > stepDistance)
        {
            currentStep++;
            lastStapPosition = myTransform.position;
            GameObject step = Instantiate(stepPrototype, foot.position, myTransform.rotation);
            
            // even step
            stepEven = currentStep % 2 == 0;
            step.SendMessage("Even", stepEven);

            // step facing direction
            step.SendMessage("Direction", movementVector.normalized);

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
}
