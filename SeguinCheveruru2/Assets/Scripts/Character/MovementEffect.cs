using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Move : MonoBehaviour
{
    protected Vector2 pMovement;
    public Vector2 Movement => pMovement;
}
