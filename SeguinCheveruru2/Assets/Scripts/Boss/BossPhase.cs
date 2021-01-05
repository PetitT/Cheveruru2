using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BossPhase
{
    [Range(0, 1)] public float threshold;
    public List<BossAttack> attacks;
    public Vector2 timeBetweenAttacks;
}
