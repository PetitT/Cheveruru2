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
    public float timeToDisplay;

    private float remainingTimeToDisplay;
    private float smoothedTarget = 1;

    private void Awake()
    {
        health.onValueChanged += DisplayHealth;
    }

    private void Update()
    {
        remainingTimeToDisplay -= Time.deltaTime;

        if (smoothedBar.value > smoothedTarget && remainingTimeToDisplay < 0)
        {
            smoothedBar.value -= smoothing * Time.deltaTime;
        }
    }

    private void DisplayHealth(float currentHealth)
    {
        float normalizedHealth = currentHealth / health.InitialValue;
        normalizedHealth = Mathf.Clamp(normalizedHealth, 0, 1);

        remainingTimeToDisplay = timeToDisplay;

        instantBar.value = normalizedHealth;
        smoothedTarget = normalizedHealth;
    }
}
