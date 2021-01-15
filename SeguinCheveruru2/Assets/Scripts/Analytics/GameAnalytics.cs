using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class GameAnalytics : MonoBehaviour
{
    public StringValue currentDifficulty;
    public GameEvent onParry;
    public GameEvent onPerfectParry;
    public FloatGameEvent onDamageTaken;
    public GameEvent onPlayerDeath;
    public GameEvent onBossDeath;
    public FloatValue bossHealth;
    public FloatValue charHealth;
    public GameEvent onProjectileReflected;
    public GameEvent onBossHit;

    private int projectileReflected;
    private int bossHitByWeapon;

    private int numberOfParries;
    private int numberOfPerfectParries;
    private int timesHit;
    private float time;
    private Outcome outcome;

    private enum Outcome { Won, Lost, Quit}

    private void Awake()
    {
        onParry.onEventRaised += OnParry_onEventRaised;
        onPerfectParry.onEventRaised += OnPerfectParry_onEventRaised;
        onDamageTaken.onEventRaised += OndamageTaken_onEventRaised;
        onPlayerDeath.onEventRaised += OnPlayerDeath_onEventRaised;
        onBossDeath.onEventRaised += OnBossDeath_onEventRaised;
        onProjectileReflected.onEventRaised += OnProjectileReflected_onEventRaised;
        onBossHit.onEventRaised += OnBossHit_onEventRaised;
    }

    private void OnDestroy()
    {
        onParry.onEventRaised -= OnParry_onEventRaised;
        onPerfectParry.onEventRaised -= OnPerfectParry_onEventRaised;
        onDamageTaken.onEventRaised -= OndamageTaken_onEventRaised;
        onPlayerDeath.onEventRaised -= OnPlayerDeath_onEventRaised;
        onBossDeath.onEventRaised -= OnBossDeath_onEventRaised;
        onProjectileReflected.onEventRaised -= OnProjectileReflected_onEventRaised;
        onBossHit.onEventRaised -= OnBossHit_onEventRaised;
    }

    private void OnBossHit_onEventRaised()
    {
        bossHitByWeapon++;
    }

    private void OnProjectileReflected_onEventRaised()
    {
        projectileReflected++;
    }

    private void OnBossDeath_onEventRaised()
    {
        outcome = Outcome.Won;
        SendData();
    }

    private void OnPlayerDeath_onEventRaised()
    {
        outcome = Outcome.Lost;
        SendData();
    }

    private void OndamageTaken_onEventRaised(float value)
    {
        timesHit++;
    }

    private void OnPerfectParry_onEventRaised()
    {
        numberOfPerfectParries++;
    }

    private void OnParry_onEventRaised()
    {
        numberOfParries++;
    }

    private void SendData()
    {
        Dictionary<string, object> eventData = new Dictionary<string, object>()
        {
            { "Result", outcome },
            { "Difficulty", currentDifficulty.Value },
            { "Remaining Boss Health", bossHealth.Value.ToString() + " / " + bossHealth.InitialValue },
            { "Remaining Player Health", charHealth.Value.ToString() + " / " + charHealth.InitialValue },
            { "Time", Time.timeSinceLevelLoad },
            { "Times Hit", timesHit },
            { "Boss Hit", bossHitByWeapon},
            { "Parries",numberOfParries },
            { "Perfect Parries", numberOfPerfectParries},
            { "Projectiles Reflected", projectileReflected }
        };

        Analytics.CustomEvent("Game Data", eventData);
    }

    private void OnApplicationQuit()
    {
        outcome = Outcome.Quit;
        SendData();
    }
}
