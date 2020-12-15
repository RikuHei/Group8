using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    public Transform player;

    public static bool isFlipped = false;

    public AudioSource audioSource;
    public AudioClip deathAudio;
    public AudioClip deadAudio;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }

    public void PlayDeathSound()
    {
        audioSource.clip = deathAudio;
        audioSource.Play();
    }

    public void DeadAudio()
    {
        audioSource.clip = deadAudio;
        audioSource.Play();
    }
}
