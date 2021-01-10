using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShaker : MonoBehaviour
{
    public List<GameEvent> screenShakeEvents;
    public List<FloatGameEvent> floatShakeEvents;
    public float amplitude;
    public float shakeTime;
    public CinemachineVirtualCamera cam;
    private CinemachineBasicMultiChannelPerlin perlin;

    private void Awake()
    {
        perlin = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        for (int i = 0; i < screenShakeEvents.Count; i++)
        {
            screenShakeEvents[i].onEventRaised += ScreenShake;
        }

        for (int i = 0; i < floatShakeEvents.Count; i++)
        {
            floatShakeEvents[i].onEventRaised += ScreenShake;
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < screenShakeEvents.Count; i++)
        {
            screenShakeEvents[i].onEventRaised -= ScreenShake;
        }

        for (int i = 0; i < floatShakeEvents.Count; i++)
        {
            floatShakeEvents[i].onEventRaised -= ScreenShake;
        }
    }

    private void ScreenShake(float obj)
    {
        DoShake();
    }

    private void ScreenShake()
    {
        DoShake();
    }

    private void DoShake()
    {
        StopCoroutine("Shake");
        StartCoroutine("Shake");
    }

    private IEnumerator Shake()
    {
        perlin.m_AmplitudeGain = amplitude;
        yield return new WaitForSeconds(shakeTime);
        perlin.m_AmplitudeGain = 0;
    }
}
