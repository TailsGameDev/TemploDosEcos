using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private string doorName = null;

    [SerializeField]
    private GameObject objToSpawnWhenUnlocked = null;

    public string DoorName { get => doorName; }

    public bool TryKey(Key key)
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
            gameObject.SetActive(false);
        }

        return succeed;
    }
}
