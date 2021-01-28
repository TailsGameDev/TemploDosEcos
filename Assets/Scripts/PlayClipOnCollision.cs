using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayClipOnCollision : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource = null;

    [SerializeField]
    private AudioClip audioClip = null;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        AudioSource spawned = Instantiate(audioSource, collision.contacts[0].point, Quaternion.identity);
        spawned.PlayOneShot(audioClip);
    }
}
