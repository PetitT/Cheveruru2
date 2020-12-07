using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharAttackAnimCallbacks : MonoBehaviour
{
    public BoolValue AttackRequestAvailability;
    public BoolValue IsAttacking;

    public void ToggleAvailabilityTrue()
    {
        AttackRequestAvailability.Value = true;
    }

    public void ToggleAvailabilityFalse()
    {
        AttackRequestAvailability.Value = false;
    }

    public void ToggleAttackFalse()
    {
        IsAttacking.Value = false;
        AttackRequestAvailability.Value = true;
    }
}
