using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dublagem : MonoBehaviour
{
    [SerializeField]
    private AudioClip dublagemClip = null;
    [SerializeField]
    private AudioClip soundExample = null;

    [SerializeField]
    private float delayBetweenDublagemAndExample = 0.0f;

    [SerializeField]
    private AudioSource audioSource = null;

    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float volume = 1.0f;
    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float volume2 = 1.0f;

    [SerializeField]
    private bool IsTheTorchDublagemKKK = false;
    private static bool alreadyPlayedTorchDublagemXD;

    [SerializeField]
    private bool IsTheBucketDublagem = false;
    private static bool alreadyPlayedBucketDublagem;

    private static List<Dublagem> dublagemOrder;

    private void Awake()
    {
        if (dublagemOrder == null)
        {
            dublagemOrder = new List<Dublagem>();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnTriggerEnter2D(collision.collider);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!IsTheTorchDublagemKKK || !alreadyPlayedTorchDublagemXD)
        {
            if (IsTheTorchDublagemKKK)
            {
                alreadyPlayedTorchDublagemXD = true;
            }

            if (!IsTheBucketDublagem || !alreadyPlayedBucketDublagem)
            {
                if (IsTheBucketDublagem)
                {
                    alreadyPlayedBucketDublagem = true;
                }

                StartCoroutine(DublagemCoroutine());
            }
        }
        GetComponent<Collider2D>().enabled = false;
    }

    IEnumerator DublagemCoroutine()
    {
        if (!dublagemOrder.Contains(this))
        {
            dublagemOrder.Add(this);
        }
        else
        {
            GetComponent<Collider2D>().enabled = false;
        }

        while (dublagemOrder.Count > 0 && this != dublagemOrder[0])
        {
            if (dublagemOrder.Count > 0 && dublagemOrder[0] == null)
            {
                dublagemOrder.RemoveAt(0);
            }
            yield return null;
        }
        // Player.changeSpeed(0.5f);

        AudioSource spawned = Instantiate(audioSource, transform.position, Quaternion.identity);
        spawned.PlayOneShot(dublagemClip, volume);

        // Wait for first sound to finish
        while(spawned!=null && !spawned.isPlaying){
            yield return null;
        }
        while (spawned != null && spawned.isPlaying)
        {
            yield return null;
        }

        yield return new WaitForSeconds(delayBetweenDublagemAndExample);

        spawned = Instantiate(audioSource, transform.position, Quaternion.identity);
        spawned.PlayOneShot(soundExample, volume2);

        if (volume2 > 0.01f)
        {
            // Wait for first sound to finish
            while (spawned != null && !spawned.isPlaying)
            {
                yield return null;
            }
            while (spawned != null && spawned.isPlaying)
            {
                yield return null;
            }
        }

        // Player.changeSpeed(6.0f);
        dublagemOrder.Remove(this);
        Destroy(gameObject);
    }
}
