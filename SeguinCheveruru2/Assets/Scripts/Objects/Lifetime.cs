using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifetime : MonoBehaviour
{
    public float lifeTime = 10f;

    private float remainingLifeTime;

    private void OnEnable()
    {
        remainingLifeTime = lifeTime;
    }

    void Update()
    {
        remainingLifeTime -= Time.deltaTime;
        if(remainingLifeTime < 0)
        {
            gameObject.SetActive(false);
        }
    }
}
