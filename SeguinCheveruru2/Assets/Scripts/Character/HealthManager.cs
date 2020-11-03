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
        CurrentHealth.RuntimeValue += obj;
        CurrentHealth.RuntimeValue = Mathf.Min(CurrentHealth.RuntimeValue, CurrentHealth.InitialValue);
    }

    private void DamageTakenhandler(float obj)
    {
        CurrentHealth.RuntimeValue -= obj;
        CurrentHealth.RuntimeValue = Mathf.Max(CurrentHealth.RuntimeValue, 0);

        if(CurrentHealth.RuntimeValue == 0)
        {
            OnDeath.Raise();
        }
    }
}
