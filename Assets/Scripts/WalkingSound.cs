using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class WalkingSound : MonoBehaviour
{
    public List<AudioClip> WalkSounds;
    public AudioSource audioSource;

    private int currentSoundIndex = 0;

    

    public void PlaySound()
    {
        if (WalkSounds.Count == 0 || audioSource == null)
        {
            Debug.LogWarning("No walking sounds");
            return;
        }

        audioSource.PlayOneShot(WalkSounds[currentSoundIndex]);

        // Switch to the next sound
        currentSoundIndex = (currentSoundIndex + 1) % WalkSounds.Count;
    }
}

