using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public float thresholdDistance = 10.0f;
    public AudioSource audioSource;
    public Transform playerTransform;

    private bool isPlaying = false;

    void Update()
    {
        float distance = Vector3.Distance(playerTransform.position, audioSource.transform.position);
        if (distance > thresholdDistance && isPlaying)
        {
            audioSource.Stop();
            isPlaying = false;
        }
        else if (distance <= thresholdDistance && !isPlaying)
        {
            audioSource.Play();
            isPlaying = true;
        }
    }
}
