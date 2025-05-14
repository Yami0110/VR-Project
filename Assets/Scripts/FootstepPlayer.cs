using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepPlayer : MonoBehaviour
{
    public AudioClip[] footstepSounds;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayFootstep()
    {
        if (footstepSounds.Length > 0)
        {
            int index = Random.Range(0, footstepSounds.Length);
            audioSource.PlayOneShot(footstepSounds[index]);
        }
    }
}
