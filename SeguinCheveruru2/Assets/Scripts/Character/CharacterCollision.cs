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

    public GameEvent HitFlashEvent;
    public GameEvent NormalParryEvent;
    public GameEvent PerfectParryEvent;
    public FloatGameEvent FreezeFrameRequest;
    public KnockBackGameEvent KnockBackRequest;
    public FloatGameEvent HealthDamageRequest;
    public FloatGameEvent ShieldDamageRequest;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out DamageDealer damageDealer))
        {
            if (damageDealer.currentOrigin == DamageOrigin.allied) { return; }
            if (shieldValue.Value)
            {
                NormalParryEvent.Raise();
                FreezeFrameRequest.Raise(ShieldedFreezeFrame.Value);
                RequestKnockback(damageDealer.attackData.ShieldedKnockBack, col);
                ShieldDamageRequest.Raise(damageDealer.attackData.ShieldDamage);
                damageDealer.gameObject.SetActive(false);
            }
            else if (perfectShieldValue.Value)
            {
                FreezeFrameRequest.Raise(PerfectShieldedFreezeFrame.Value);
                PerfectParryEvent.Raise();
                if (col.TryGetComponent(out IRevertableProjectile revert))
                {
                    revert.OnRevert();
                }
            }
            else
            {
                DisplayBlood(col, damageDealer);
                HitFlashEvent.Raise();
                FreezeFrameRequest.Raise(DamageTakenFreezeFrame.Value);
                RequestKnockback(damageDealer.attackData.UnshieldedKnockBack, col);
                HealthDamageRequest.Raise(damageDealer.attackData.HealthDamage);
                damageDealer.gameObject.SetActive(false);
            }

        }
    }

    private void DisplayBlood(Collider2D col, DamageDealer damageDealer)
    {
        Vector2 direction = damageDealer.transform.rotation == Quaternion.Euler(0, 0, 0) ? Vector2.left : Vector2.right;
        Vector2 position = damageDealer.GetComponent<Collider2D>().ClosestPoint(transform.position);
        BloodDisplay.Instance.DisplayBlood(position, direction);
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
