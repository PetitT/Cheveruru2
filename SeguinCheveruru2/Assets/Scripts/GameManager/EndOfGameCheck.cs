using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfGameCheck : MonoBehaviour
{
    public GameEvent onPlayerDeath;
    public GameEvent onBossDeath;

    public GameObject player;
    public GameObject boss;

    private void Awake()
    {
        onPlayerDeath.onEventRaised += OnPlayerDeath_onEventRaised;
        onBossDeath.onEventRaised += OnBossDeath_onEventRaised;
    }

    private void OnDestroy()
    {
        onPlayerDeath.onEventRaised -= OnPlayerDeath_onEventRaised;
        onBossDeath.onEventRaised -= OnBossDeath_onEventRaised;
    }

    private void OnBossDeath_onEventRaised()
    {
        boss.SetActive(false);
        StartCoroutine("RestartGame");
    }

    private void OnPlayerDeath_onEventRaised()
    {
        player.SetActive(false);
        StartCoroutine("RestartGame");
    }

    private IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
