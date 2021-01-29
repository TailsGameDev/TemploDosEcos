using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Transform myTransform;
    
    // movement
    [SerializeField]
    private float speed = 0.0f;

    // step
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

    [SerializeField]
    private GameObject stepPrototype = null;

    void Awake()
    {
        myTransform = transform;
    }

    private void FixedUpdate()
    {
        Vector3 movementVector = GetMovementVector();

        // Set Position
        Vector3 increment = movementVector.normalized * speed * Time.deltaTime;
        myTransform.position += increment;

        // Step spawner
        if (Vector3.Distance(myTransform.position, lastStapPosition) > stepDistance)
        {
            currentStep++;
            lastStapPosition = myTransform.position;
            GameObject step = Instantiate(stepPrototype, foot.position, myTransform.rotation);

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
