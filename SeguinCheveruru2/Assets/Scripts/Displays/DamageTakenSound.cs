using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTakenSound : MonoBehaviour
{
    public AudioSource audioSrc;
    public FloatGameEvent damageTakenEvent;
    public List<AudioClip> damageSounds;
    public AudioClip surprise;

    private float initialChance = 101;
    private float currentChance;

    private void Awake()
    {
        damageTakenEvent.onEventRaised += DoSound;
        currentChance = initialChance;
    }

    private void OnDestroy()
    {
        damageTakenEvent.onEventRaised -= DoSound;
    }

    private void DoSound(float obj)
    {
        AudioClip clip = damageSounds.GetRandom();
        int random = UnityEngine.Random.Range(0, 100);
        if (random > currentChance)
        {
            clip = surprise;
            currentChance = 1000;
        }
        currentChance -= 0.5f;
        audioSrc.PlayOneShot(clip);
    }
}
