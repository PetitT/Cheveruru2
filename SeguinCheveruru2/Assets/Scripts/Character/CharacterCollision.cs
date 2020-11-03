using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CharacterCollision : MonoBehaviour
{
    public FloatValue DamageTakenFreezeFrame;
    public FloatValue ShieldedFreezeFrame;
    public FloatValue PerfectShieldedFreezeFrame;

    public BoolValue shieldValue;
    public BoolValue perfectShieldValue;

    public FloatGameEvent FreezeFrameRequest;
    public KnockBackGameEvent KnockBackRequest;
    public FloatGameEvent HealthDamageRequest;
    public FloatGameEvent ShieldDamageRequest;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out DamageDealer damageDealer))
        {
            if (shieldValue.RuntimeValue)
            {
                Debug.Log("Shielded");
                FreezeFrameRequest.Raise(ShieldedFreezeFrame.RuntimeValue);

                KnockBackData knockback = new KnockBackData()
                {
                    force = damageDealer.attackData.ShieldedKnockBack.RuntimeValue,
                    direction = transform.position - col.transform.position.normalized
                };
                KnockBackRequest.Raise(knockback);
                ShieldDamageRequest.Raise(damageDealer.attackData.ShieldDamage.RuntimeValue);
            }
            else if (perfectShieldValue.RuntimeValue)
            {
                Debug.Log("Perfect Shielded");
                FreezeFrameRequest.Raise(PerfectShieldedFreezeFrame.RuntimeValue);
            }
            else
            {
                Debug.Log("Hit");
                FreezeFrameRequest.Raise(DamageTakenFreezeFrame.RuntimeValue);
                KnockBackData knockback = new KnockBackData()
                {
                    force = damageDealer.attackData.UnshieldedKnockBack.RuntimeValue,
                    direction = transform.position - col.transform.position.normalized
                };
                KnockBackRequest.Raise(knockback);
                HealthDamageRequest.Raise(damageDealer.attackData.HealthDamage.RuntimeValue);
            }
        }
    }
}
