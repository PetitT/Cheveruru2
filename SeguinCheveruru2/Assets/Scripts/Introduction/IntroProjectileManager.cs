using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroProjectileManager : MonoBehaviour
{
    public GameObject projectile;

    private void Update()
    {
        if (!projectile.activeSelf)
        {
            projectile.SetActive(true);
        }
    }
}
