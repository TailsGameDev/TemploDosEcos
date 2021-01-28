using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class radarAnimator : MonoBehaviour
{

  [SerializeField] private Sprite[] frameArray;
  private int currentFrame = 0;
  private float timer;


  void Values(float radarRange)
  {
    transform.localScale = new Vector3(radarRange, radarRange, 1);
  }



  private void OnCollisionEnter2D(Collision2D collision)
  {
  }
  void Update()
  {
    timer += Time.deltaTime;

    if (timer >= 0.01f)
    {
      timer -= 0.01f;
      currentFrame++;
      gameObject.GetComponent<SpriteRenderer>().sprite = frameArray[currentFrame];
    }
    if (currentFrame == frameArray.Length - 1)
    {
      Destroy(gameObject);
    }
  }
}
