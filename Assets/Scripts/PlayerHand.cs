using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    [SerializeField]
    private Transform handPosition = null;

    [SerializeField]
    private Transform dropPosition = null;

    [SerializeField]
    private AudioSource audioSource = null;
    [SerializeField]
    private AudioClip pickUpItemSFX = null;
    [SerializeField]
    private AudioClip dropItemSFX = null;

    private Key keyBeingHeld;
    private List<Key> keysInRangeToPickUp;
    private List<Door> doorsInRange;

    private void Awake()
    {
        keysInRangeToPickUp = new List<Key>();
        doorsInRange = new List<Door>();
    }

    public void PickUpKey(Key keyBeingHeld)
    {
        if (keyBeingHeld != null)
        {
            this.keyBeingHeld = keyBeingHeld;
            keyBeingHeld.Transform.position = handPosition.position;
            keyBeingHeld.Transform.rotation = transform.rotation;
            keyBeingHeld.Transform.parent = handPosition;
            keyBeingHeld.SetAllCollidersEnable(false);

            keysInRangeToPickUp.Remove(keyBeingHeld);

            audioSource.PlayOneShot(pickUpItemSFX);
        }
        else
        {
            Debug.LogError("keyBeingHeld param is null");
        }
    }

    private void Update()
    {
        // Trying to fix bug quick
        if (doorsInRange.Count > 0 && (doorsInRange[0]==null||Vector3.SqrMagnitude(doorsInRange[0].transform.position - transform.position) > 15.0f))
        {
            doorsInRange.Clear();
        }

        if (Input.GetKeyDown(KeyCode.Space)){
            if (keyBeingHeld == null && keysInRangeToPickUp.Count > 0)
            {
                PickUpKey(keysInRangeToPickUp[0]);
            }
            else if (keyBeingHeld!=null && doorsInRange.Count==0)
            {
                // Drop Key

                keyBeingHeld.Transform.position = dropPosition.position;
                keyBeingHeld.Transform.rotation = Quaternion.identity;
                keyBeingHeld.Transform.parent = null;
                keyBeingHeld.SetAllCollidersEnable(true);
                keyBeingHeld = null;

                audioSource.PlayOneShot(dropItemSFX);
            }
            else if (keyBeingHeld != null && doorsInRange.Count>0)
            {
                // Try to use key

                bool succeed = false;
                for (int d = 0; d < doorsInRange.Count; d++)
                {
                    succeed |= doorsInRange[d].TryKey(keyBeingHeld);
                }

                if (succeed)
                {
                    keyBeingHeld.transform.parent = null;
                    keyBeingHeld.DestroyItselfIfNeeded();
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
                    doorsInRange.Add(doorCollection.Doors[d]);
                }
                return;
            }
        }

        // Collision with Door
        Door door = col.GetComponent<Door>();
        if (door != null)
        {
            doorsInRange.Insert(0, door);
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
                // The better would be to remove just the ones that belong to "col", but it was bugged that way.
                doorsInRange.Clear();
                /*
                for (int d = 0; d < doorCollection.Doors.Length; d++)
                {
                    doorsInRange.Remove(doorCollection.Doors[d]);
                }
                */
                return;
            }
        }

        // Exit collision with door
        {
            Door door = col.GetComponent<Door>();
            if (door != null)
            {
                doorsInRange.Remove(door);
                return;
            }
        }
    }

    public void QuitAllDoors()
    {
        doorsInRange.Clear();
    }
}
