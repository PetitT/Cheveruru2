using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossAttack : MonoBehaviour
{
    [ContextMenu("DoAttack")]
    public void DoAttack()
    {
        StartCoroutine(Attack(() => { }));
    }

    public abstract IEnumerator Attack(Action onFinish);
}
