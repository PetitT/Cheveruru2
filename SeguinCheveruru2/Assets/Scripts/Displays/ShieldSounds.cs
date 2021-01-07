using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSounds : MonoBehaviour
{
    public AudioSource audioSrc;
    public List<AudioClip> parryClips;
    public AudioClip perfectParryClip;
    public AudioClip shieldBreakSoundOne;
    public AudioClip shieldBreakSoundTwo;

    public AudioClip shieldRegainedSoundOne;
    public AudioClip shieldRegainedSoundTwo;

    public GameEvent parryEvent;
    public GameEvent perfectParryEvent;
    public GameEvent shieldBreakEvent;
    public GameEvent shieldRegainedEvent;

    private void Awake()
    {
        parryEvent.onEventRaised += ParrySound;
        perfectParryEvent.onEventRaised += PerfectParrySound;
        shieldBreakEvent.onEventRaised += ShieldBreakSound;
        shieldRegainedEvent.onEventRaised += ShieldRegainedSound;
    }


    private void OnDestroy()
    {
        parryEvent.onEventRaised -= ParrySound;
        perfectParryEvent.onEventRaised -= PerfectParrySound;
        shieldBreakEvent.onEventRaised -= ShieldBreakSound;
        shieldRegainedEvent.onEventRaised += ShieldRegainedSound;
    }

    private void PerfectParrySound()
    {
        audioSrc.PlayOneShot(perfectParryClip);
    }

    private void ParrySound()
    {
        audioSrc.PlayOneShot(parryClips.GetRandom());
    }

    private void ShieldBreakSound()
    {
        audioSrc.PlayOneShot(shieldBreakSoundOne);
        audioSrc.PlayOneShot(shieldBreakSoundTwo);
    }

    private void ShieldRegainedSound()
    {
        audioSrc.PlayOneShot(shieldRegainedSoundOne);
        audioSrc.PlayOneShot(shieldRegainedSoundTwo);
    }
}
