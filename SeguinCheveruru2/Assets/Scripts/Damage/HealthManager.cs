using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public FloatValue CurrentHealth;
    public FloatGameEvent OnDamageTaken;
    public FloatGameEvent OnHealthGained;
    public GameEvent OnDeath;

    private void Awake()
    {
        OnDamageTaken.onEventRaised += DamageTakenhandler;
        OnHealthGained.onEventRaised += HealthGainedHandler;
    }
    private void OnDestroy()
    {
        OnDamageTaken.onEventRaised -= DamageTakenhandler; 
        OnHealthGained.onEventRaised += HealthGainedHandler;
    }

    private void HealthGainedHandler(float obj)
    {
        CurrentHealth.Value += obj;
        CurrentHealth.Value = Mathf.Min(CurrentHealth.Value, CurrentHealth.InitialValue);
    }

    private void DamageTakenhandler(float obj)
    {
        CurrentHealth.Value -= obj;
        CurrentHealth.Value = Mathf.Max(CurrentHealth.Value, 0);

        if(CurrentHealth.Value == 0)
        {
            OnDeath.Raise();
        }
    }
}
