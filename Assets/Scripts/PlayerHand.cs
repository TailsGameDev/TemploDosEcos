using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    [SerializeField]
    private Transform handPosition = null;

    [SerializeField]
    private Transform dropPosition = null;

    private Key keyBeingHeld;
    private List<Key> keysInRangeToPickUp;
    private List<Door> doorInRange;

    private void Awake()
    {
        keysInRangeToPickUp = new List<Key>();
        doorInRange = new List<Door>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)){
            if (keyBeingHeld == null && keysInRangeToPickUp.Count > 0)
            {
                // Pick up key

                keyBeingHeld = keysInRangeToPickUp[0];
                keyBeingHeld.Transform.position = handPosition.position;
                keyBeingHeld.Transform.parent = handPosition;
                keyBeingHeld.Collider.enabled = false;

                keysInRangeToPickUp.Remove(keyBeingHeld);
            }
            else if (keyBeingHeld!=null && doorInRange.Count==0)
            {
                // Drop Key

                keyBeingHeld.Transform.position = dropPosition.position;
                keyBeingHeld.Transform.parent = null;
                keyBeingHeld.Collider.enabled = true;
                keyBeingHeld = null;
            }
            else if (keyBeingHeld != null && doorInRange.Count>0)
            {
                // Try to use key

                bool succeed = false;
                for (int d = 0; d < doorInRange.Count; d++)
                {
                    succeed |= doorInRange[d].TryKey(keyBeingHeld);
                }

                if (succeed)
                {
                    Destroy(keyBeingHeld.gameObject);
                    keyBeingHeld = null;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnTriggerEnter2D(collision.collider);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        // Collision with Key
        {
            Key key = col.GetComponent<Key>();
            if (key != null)
            {
                keysInRangeToPickUp.Insert(0, key);
                return;
            }
        }

        // Collision with doorCollection
        {
            DoorCollection doorCollection = col.GetComponent<DoorCollection>();
            if (doorCollection != null)
            {
                for (int d = 0; d < doorCollection.Doors.Length; d++)
                {
                    doorInRange.Add(doorCollection.Doors[d]);
                }
                return;
            }
        }

        // Collision with Door
        Door door = col.GetComponent<Door>();
        if (door != null)
        {
            doorInRange.Insert(0, door);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        OnTriggerExit2D(collision.collider);
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        // Exit collision with key
        {
            Key key = col.GetComponent<Key>();
            if (key != null)
            {
                keysInRangeToPickUp.Remove(key);
                return;
            }
        }

        // Exit collision with doorCollection
        {
            DoorCollection doorCollection = col.GetComponent<DoorCollection>();
            if (doorCollection != null)
            {
                for (int d = 0; d < doorCollection.Doors.Length; d++)
                {
                    doorInRange.Remove(doorCollection.Doors[d]);
                }
                return;
            }
        }

        // Exit collision with door
        {
            Door door = col.GetComponent<Door>();
            if (door != null)
            {
                doorInRange.Remove(door);
                return;
            }
        }
    }
}
