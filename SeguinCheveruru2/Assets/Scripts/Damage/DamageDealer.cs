using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public DamageOrigin defaultOrigin;

    public AttackDamageData attackData;

    [HideInInspector] public DamageOrigin currentOrigin;

    private void OnEnable()
    {
        currentOrigin = defaultOrigin;
    }
}
