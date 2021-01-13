using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MobileButton : MonoBehaviour
{
    private bool isPointedLeft;
    private bool isPointedRight;

    private HorizontalMovement movement;

    private void Start()
    {
        movement = FindObjectOfType<HorizontalMovement>();
    }

    private void Update()
    {
        if (!isPointedLeft && !isPointedRight)
        {
            movement.Move(0);
        }
        else if (isPointedLeft)
        {
            movement.Move(-1f);
        }
        else if (isPointedRight)
        {
            movement.Move(1f);
        }
    }

    public void OnPointerDownLeft()
    {
        isPointedLeft = true;
    }

    public void OnPointerUpLeft()
    {
        isPointedLeft = false;
    }

    public void OnPointerDownRight()
    {
        isPointedRight = true;
    }

    public void OnPointerUpRight()
    {
        isPointedRight = false;
    }
}
