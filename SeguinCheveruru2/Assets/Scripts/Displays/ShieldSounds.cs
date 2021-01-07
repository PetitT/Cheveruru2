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

    public AudioClip shieldStartSound;

    public GameEvent parryEvent;
    public GameEvent perfectParryEvent;
    public GameEvent shieldBreakEvent;
    public GameEvent shieldRegainedEvent;
    public BoolValue isShielding;

    private bool shieldBroken;

    private void Awake()
    {
        parryEvent.onEventRaised += ParrySound;
        perfectParryEvent.onEventRaised += PerfectParrySound;
        shieldBreakEvent.onEventRaised += ShieldBreakSound;
        shieldRegainedEvent.onEventRaised += ShieldRegainedSound;
        isShielding.onValueChanged += ShieldStartSound;
    }

    private void ShieldStartSound(bool obj)
    {
        if (obj)
        {
            audioSrc.PlayOneShot(shieldStartSound);
        }
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
        shieldBroken = true;
        audioSrc.PlayOneShot(shieldBreakSoundOne);
        audioSrc.PlayOneShot(shieldBreakSoundTwo);
    }

    private void ShieldRegainedSound()
    {
        if (shieldBroken)
        {
            shieldBroken = false;
            audioSrc.PlayOneShot(shieldRegainedSoundOne);
            audioSrc.PlayOneShot(shieldRegainedSoundTwo);
        }
    }
}
