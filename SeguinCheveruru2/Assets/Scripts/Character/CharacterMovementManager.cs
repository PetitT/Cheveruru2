using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterMovementManager : MonoBehaviour
{
    private List<Move> effects = new List<Move>();

    private void Awake()
    {
        effects = GetComponents<Move>().ToList();
    }

    private void Update()
    {
        ProcessEffects();
    }

    private void ProcessEffects()
    {
        Vector2 movement = Vector2.zero;
        for (int i = 0; i < effects.Count; i++)
        {
            movement += effects[i].Movement;
        }
        gameObject.transform.Translate(movement);
    }
}
