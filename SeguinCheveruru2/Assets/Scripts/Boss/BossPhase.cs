using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BossPhase 
{
    public List<BossAttack> attacks;
    public Vector2 timeBetweenAttacks;
}
