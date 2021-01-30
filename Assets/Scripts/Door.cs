using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private string doorName = null;

    [SerializeField]
    private GameObject objToSpawnWhenUnlocked = null;

    protected Key keyRef;

    public string DoorName { get => doorName; }

    public virtual bool TryKey(Key key)
    {
        bool succeed = false;

        if (key.DoorName == doorName)
        {
            succeed = true;

            if (objToSpawnWhenUnlocked == null){
                objToSpawnWhenUnlocked = new GameObject();
            }
            GameObject obj = Instantiate(objToSpawnWhenUnlocked, transform.position, Quaternion.identity);
            obj.transform.parent = transform.parent;
            obj.transform.localScale = transform.localScale;
            obj.transform.rotation = transform.rotation;
            gameObject.SetActive(false);

            keyRef = key;
        }

        return succeed;
    }
}
