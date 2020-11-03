using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CharacterShield : MonoBehaviour
{
    public FloatValue ShieldValue;
    public FloatValue ShieldGainPerSec;

    public FloatValue ShieldStunTime;
    public FloatValue PerfectShieldTime;
    public FloatValue ShieldCooldownTime;

    public FloatValue ShieldActivationBufferTimer;

    public BoolValue IsShielding;
    public BoolValue IsPerfectShielding;
    public BoolValue IsAttacking;

    public GameEvent ShieldBreak;
    public FloatGameEvent ShieldDamageTaken;

    private bool wantsToActivateShield = false;
    private bool wantsToReleaseShield = false;

    private enum ShieldState { fullShield, regenerating, shielding, shieldStunned, releasing, recovering }
    private ShieldState currentState = ShieldState.fullShield;

    private void Awake()
    {
        ShieldDamageTaken.onEventRaised += ShieldDamagehandler;
    }

    private void OnDestroy()
    {
        ShieldDamageTaken.onEventRaised -= ShieldDamagehandler;
    }

    private void ShieldDamagehandler(float obj)
    {
        ShieldValue.RuntimeValue -= obj;
        ShieldValue.RuntimeValue = Mathf.Max(ShieldValue.RuntimeValue, 0);
        if (ShieldValue.RuntimeValue == 0)
        {
            BreakShield();
        }
    }

    private void Update()
    {
        ProcesState();
    }

    private void ProcesState()
    {
        switch (currentState)
        {
            case ShieldState.fullShield:
                CheckShieldActivation();
                break;

            case ShieldState.regenerating:
                CheckShieldActivation();
                RegenerateShield();
                break;

            case ShieldState.shielding:
                LoseShield();
                CheckShieldRelease();
                break;

            case ShieldState.shieldStunned:
                LoseShield();
                break;

            case ShieldState.releasing:
                break;

            case ShieldState.recovering:
                RegenerateShield();
                break;
            default:
                break;
        }
    }

    private void RegenerateShield()
    {
        ShieldValue.RuntimeValue += Time.deltaTime * ShieldGainPerSec.RuntimeValue;
        if (ShieldValue.RuntimeValue >= ShieldValue.InitialValue)
        {
            ShieldValue.RuntimeValue = ShieldValue.InitialValue;
            currentState = ShieldState.fullShield;
        }
    }

    private void LoseShield()
    {
        ShieldValue.RuntimeValue -= Time.deltaTime;
        if (ShieldValue.RuntimeValue <= 0)
        {
            BreakShield();
        }
    }

    private void BreakShield()
    {
        ShieldBreak.Raise();
        IsShielding.RuntimeValue = false;
        currentState = ShieldState.recovering;
    }

    public void ActivateShieldRequest()
    {
        wantsToReleaseShield = false;
        if (!wantsToActivateShield && !IsAttacking.RuntimeValue)
        {
            StartCoroutine(nameof(ShieldActivationBuffer));
        }
    }

    private void CheckShieldActivation()
    {
        if (wantsToActivateShield)
        {
            IsShielding.RuntimeValue = true;
            wantsToActivateShield = false;
            StopCoroutine(nameof(ShieldActivationBuffer));
            StartCoroutine(nameof(ShieldStun));
        }
    }

    public void ReleaseShieldRequest()
    {
        wantsToReleaseShield = true;
    }

    private void CheckShieldRelease()
    {
        if (wantsToReleaseShield)
        {
            wantsToReleaseShield = false;
            IsShielding.RuntimeValue = false;
            StartCoroutine(nameof(ReleaseShield));
        }
    }
    private IEnumerator ShieldStun()
    {
        currentState = ShieldState.shieldStunned;
        yield return new WaitForSeconds(ShieldStunTime.RuntimeValue);
        currentState = ShieldState.shielding;
    }

    private IEnumerator ReleaseShield()
    {
        currentState = ShieldState.releasing;
        IsPerfectShielding.RuntimeValue = true;
        yield return new WaitForSeconds(PerfectShieldTime.RuntimeValue);
        IsPerfectShielding.RuntimeValue = false;
        yield return new WaitForSeconds(ShieldCooldownTime.RuntimeValue);
        currentState = ShieldState.regenerating;
    }

    private IEnumerator ShieldActivationBuffer()
    {
        wantsToActivateShield = true;
        yield return new WaitForSeconds(ShieldActivationBufferTimer.RuntimeValue);
        wantsToActivateShield = false;
    }
}
