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

    public GameEvent PerfectParryEvent;
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
                RequestKnockback(damageDealer.attackData.ShieldedKnockBack, col);
                ShieldDamageRequest.Raise(damageDealer.attackData.ShieldDamage);
            }
            else if (perfectShieldValue.Value)
            {
                Debug.Log("Perfect Shielded");
                FreezeFrameRequest.Raise(PerfectShieldedFreezeFrame.Value);
                PerfectParryEvent.Raise();
            }
            else
            {
                Debug.Log("Hit");
                FreezeFrameRequest.Raise(DamageTakenFreezeFrame.Value);
                RequestKnockback(damageDealer.attackData.UnshieldedKnockBack, col);
                HealthDamageRequest.Raise(damageDealer.attackData.HealthDamage);
            }

            damageDealer.gameObject.SetActive(false);
        }
    }

    private void RequestKnockback(float force, Collider2D col)
    {
        KnockBackData knockback = new KnockBackData()
        {
            force = force,
            direction = (transform.position.Grounded() - col.transform.position.Grounded())
        };
        KnockBackRequest.Raise(knockback);
    }
}
