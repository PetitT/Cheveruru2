using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryParticlesDisplay : MonoBehaviour
{
    public GameEvent onNormalParry;
    public GameEvent onPerfectParry;

    public ParticleSystem perfectParryParticles;
    public ParticleSystem normalParryParticles;

    private void Awake()
    {
        onPerfectParry.onEventRaised += DisplayPerfectParticles;
        onNormalParry.onEventRaised += DisplayNormalParticles;
    }

    private void OnDestroy()
    {
        onPerfectParry.onEventRaised -= DisplayPerfectParticles;
        onNormalParry.onEventRaised -= DisplayNormalParticles;
    }

    private void DisplayNormalParticles()
    {
        normalParryParticles.Play();
    }

    private void DisplayPerfectParticles()
    {
        perfectParryParticles.Play();
    }
}
