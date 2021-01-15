using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFader : MonoBehaviour
{
    private AudioSource audioSrc;
    private float fadeSpeed = 0.25f;

    private void Awake()
    {
        audioSrc = GetComponent<AudioSource>();
    }

    public void FadeOut()
    {
        StartCoroutine("Fade");
    }

    private IEnumerator Fade()
    {
        while(audioSrc.volume > 0)
        {
            audioSrc.volume -= fadeSpeed * Time.deltaTime;
            yield return null;
        }
        audioSrc.volume = 0;
    }
}
