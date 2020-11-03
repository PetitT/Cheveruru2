using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharAttackAnimCallbacks : MonoBehaviour
{
    public BoolValue AttackRequestAvailability;
    public BoolValue IsAttacking;

    public void ToggleAvailabilityTrue()
    {
        AttackRequestAvailability.RuntimeValue = true;
    }

    public void ToggleAvailabilityFalse()
    {
        AttackRequestAvailability.RuntimeValue = false;
    }

    public void ToggleAttackFalse()
    {
        IsAttacking.RuntimeValue = false;
        AttackRequestAvailability.RuntimeValue = true;
    }
}
