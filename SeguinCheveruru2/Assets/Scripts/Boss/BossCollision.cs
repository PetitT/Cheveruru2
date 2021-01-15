using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCollision : MonoBehaviour
{
    public FloatGameEvent HealthDamageRequest;
    public FloatGameEvent FreezeFrameRequest;
    public GameEvent FlashRequest;
    public FloatValue FreezeFrameIntensity;
    public GameEvent onBossHitByWeapon;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out DamageDealer damageDealer))
        {
            if (damageDealer.currentOrigin == DamageOrigin.ennemy) { return; }
            HealthDamageRequest.Raise(damageDealer.attackData.HealthDamage);
            FlashRequest.Raise();
            DisplayBlood(collision, damageDealer);
            if (collision.TryGetComponent(out IRevertableProjectile revertableProjectile))
            {
                collision.gameObject.SetActive(false);
            }
            else
            {
                onBossHitByWeapon.Raise();
                FreezeFrameRequest.Raise(FreezeFrameIntensity.Value);
            }
        }
    }

    private void DisplayBlood(Collider2D col, DamageDealer damageDealer)
    {
        Vector2 direction = damageDealer.transform.rotation == Quaternion.Euler(0, 0, 0) ? Vector2.right : Vector2.left;
        Vector2 position = damageDealer.GetComponent<Collider2D>().ClosestPoint(transform.position);
        BloodDisplay.Instance.DisplayBlood(position, direction);
    }
}
