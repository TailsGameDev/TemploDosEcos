using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerRadar : MonoBehaviour
{
  public GameObject radarObject;
  public float radarRange;
  public float radarSound;

  void Start()
  {

  }

  void Update()
  {
    // default radar key
    if (Input.GetKeyDown(KeyCode.Space))
    {
      GameObject radar = Instantiate(radarObject, transform.position, Quaternion.identity) as GameObject;
      radar.SendMessage("Values", radarRange);
    }


    // ! sizes tests (remove later)
    if (Input.GetKeyDown(KeyCode.Alpha1))
    {
      // transform.localScale = new Vector3(1, 1, 1);
      GameObject radar = Instantiate(radarObject, transform.position, Quaternion.identity) as GameObject;
      radar.SendMessage("Values", 1);
    }
    if (Input.GetKeyDown(KeyCode.Alpha2))
    {
      // transform.localScale = new Vector3(1, 1, 1);
      GameObject radar = Instantiate(radarObject, transform.position, Quaternion.identity) as GameObject;
      radar.SendMessage("Values", 3);
    }
    if (Input.GetKeyDown(KeyCode.Alpha3))
    {
      // transform.localScale = new Vector3(1, 1, 1);
      GameObject radar = Instantiate(radarObject, transform.position, Quaternion.identity) as GameObject;
      radar.SendMessage("Values", 5);
    }
    // ! ------ // ------

  }

  void FixedUpdate()
  {
  }
}
