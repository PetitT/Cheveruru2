using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossAttack : MonoBehaviour
{
    public abstract IEnumerator Attack(Action onFinish);
}
