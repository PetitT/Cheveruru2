using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AttackDamageData : ScriptableObject
{
    public float HealthDamage;
    public float ShieldDamage;

    public float UnshieldedKnockBack;
    public float ShieldedKnockBack;
}
