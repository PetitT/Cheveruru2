using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarDisplay : MonoBehaviour
{
    public FloatValue health;

    public Slider instantBar;
    public Slider smoothedBar;

    public float smoothing;

    private float smoothedTarget = 1;

    private void Awake()
    {
        health.onValueChanged += DisplayHealth;
    }

    private void Update()
    {
        if(smoothedBar.value > smoothedTarget)
        {
            smoothedBar.value -= smoothing * Time.deltaTime;
        }
    }

    private void DisplayHealth(float currentHealth)
    {
        float normalizedHealth = currentHealth / health.InitialValue;
        normalizedHealth = Mathf.Clamp(normalizedHealth, 0, 1);

        instantBar.value = normalizedHealth;
        smoothedTarget = normalizedHealth;
    }
}
