using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossAttack : MonoBehaviour
{
    protected AudioSource audioSrc;
    protected virtual void Awake()
    {
        audioSrc = GetComponent<AudioSource>();
    }

    [ContextMenu("DoAttack")]
    public void DoAttack()
    {
        StartCoroutine(Attack(() => { }));
    }

    public abstract IEnumerator Attack(Action onFinish);
    public abstract void CancelAttack();
}
