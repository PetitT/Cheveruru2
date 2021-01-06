using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodDisplay : Singleton<BloodDisplay>
{
    public GameObject bloodParticles;

    public void DisplayBlood(Vector2 position, Vector2 direction)
    {
        Quaternion dir = direction == Vector2.right ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);
        GameObject newParticles = Pool.Instance.GetItemFromPool(bloodParticles, position, dir);
    }
}
