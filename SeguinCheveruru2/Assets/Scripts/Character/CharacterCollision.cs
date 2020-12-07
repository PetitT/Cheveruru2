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
            if (shieldValue.Value)
            {
                Debug.Log("Shielded");
                FreezeFrameRequest.Raise(ShieldedFreezeFrame.Value);

                KnockBackData knockback = new KnockBackData()
                {
                    force = damageDealer.attackData.ShieldedKnockBack.Value,
                    direction = transform.position - col.transform.position.normalized
                };
                KnockBackRequest.Raise(knockback);
                ShieldDamageRequest.Raise(damageDealer.attackData.ShieldDamage.Value);
            }
            else if (perfectShieldValue.Value)
            {
                Debug.Log("Perfect Shielded");
                FreezeFrameRequest.Raise(PerfectShieldedFreezeFrame.Value);
            }
            else
            {
                Debug.Log("Hit");
                FreezeFrameRequest.Raise(DamageTakenFreezeFrame.Value);
                KnockBackData knockback = new KnockBackData()
                {
                    force = damageDealer.attackData.UnshieldedKnockBack.Value,
                    direction = transform.position - col.transform.position.normalized
                };
                KnockBackRequest.Raise(knockback);
                HealthDamageRequest.Raise(damageDealer.attackData.HealthDamage.Value);
            }
        }
    }
}
