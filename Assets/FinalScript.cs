using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalScript : MonoBehaviour
{
   private IEnumerator Start()
    {
        yield return new WaitForSeconds(2.0f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("final");
    }
}
