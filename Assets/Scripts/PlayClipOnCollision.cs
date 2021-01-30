using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayClipOnCollision : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource = null;

    [SerializeField]
    private AudioClip audioClip = null;

    [SerializeField]
    [Range(0.0f,1.0f)]
    private float volume = 1.0f;

    [SerializeField]
    private bool isTrigger = false;

    private void PlayClip(){
        AudioSource spawned = Instantiate(audioSource, transform.position, Quaternion.identity);
        spawned.PlayOneShot(audioClip, volume);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!isTrigger){
           PlayClip();
        }
    }
    private void OnTriggerEnter2D(Collider2D  collision)
    {
        if(isTrigger){
           PlayClip();
        }       
    }
}
