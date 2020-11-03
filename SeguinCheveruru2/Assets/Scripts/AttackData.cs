using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AttackData : ScriptableObject
{
    public FloatValue HealthDamage;
    public FloatValue ShieldDamage;

    public FloatValue UnshieldedKnockBack;
    public FloatValue ShieldedKnockBack;
}
