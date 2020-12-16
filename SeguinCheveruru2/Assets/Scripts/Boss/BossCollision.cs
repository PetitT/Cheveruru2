using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCollision : MonoBehaviour
{
    public FloatGameEvent HealthDamageRequest;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out DamageDealer damageDealer))
        {
            if(damageDealer.currentOrigin == DamageOrigin.ennemy) { return; }
            HealthDamageRequest.Raise(damageDealer.attackData.HealthDamage);
            damageDealer.gameObject.SetActive(false);
        }
    }
}
