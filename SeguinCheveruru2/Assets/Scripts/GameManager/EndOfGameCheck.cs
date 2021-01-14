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

    public Collider2D bossCollider;
    public Collider2D playerCollider;

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
        BossDirection.Instance.ToggleRotation(false);
        BossAnimation.Instance.Animate(BossAnimation.BossAnim.Die);
        bossCollider.enabled = false;
        playerCollider.enabled = false;

        StartCoroutine("RestartGame");
    }

    private void OnPlayerDeath_onEventRaised()
    {
        player.SetActive(false);
        StartCoroutine("RestartGame");
    }

    private IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
