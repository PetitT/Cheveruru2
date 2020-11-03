using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpoderController : MonoBehaviour
{
    public Transform[] legs;
    public LayerMask ground;

    private List<GameObject> legItems = new List<GameObject>();

    private void Start()
    {
        foreach (var leg in legs)
        {
            GameObject newItem = new GameObject();
            legItems.Add(newItem);
        }
    }

    private void Update()
    {
        for (int i = 0; i < legs.Length; i++)
        {
            MoveLegs(i);
        }
    }

    private void MoveLegs(int legIndex)
    {
        legItems[legIndex].transform.position = new Vector2(legs[legIndex].position.x, 20);
        RaycastHit2D hit = Physics2D.Raycast(legItems[legIndex].transform.position, -legItems[legIndex].transform.up, 30, ground);
        
        if (hit.collider != null)
        {
            legs[legIndex].transform.position = hit.point;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        foreach (var leg in legs)
        {
            Gizmos.DrawWireSphere(new Vector2(leg.position.x, 10), 0.5f);
            Gizmos.DrawRay(leg.position, Vector2.down);
        }
    }


}
