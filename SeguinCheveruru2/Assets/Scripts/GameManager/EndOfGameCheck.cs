using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfGameCheck : MonoBehaviour
{
    public GameEvent onPlayerDeath;
    public GameEvent onBossDeath;

    public GameObject player;
    public GameObject boss;

    public GameObject bigBloodParticles;
    public GameObject deadBoss;
    public CinemachineVirtualCamera cam;
    public LowTeeGames.LTInputManager inputManager;
    public HorizontalMovement horizontalMove;
    public CharacterMovementManager movement;
    public Animator anim;
    public TextMeshProUGUI endText;
    public List<GameObject> itemsToDeactivate;

    public Animator fadeCanvas;

    public Collider2D bossCollider;
    public Collider2D playerCollider;

    private void Awake()
    {
        onPlayerDeath.onEventRaised += OnPlayerDeath_onEventRaised;
        onBossDeath.onEventRaised += OnBossDeath_onEventRaised;
        fadeCanvas.SetTrigger("FadeIn");
    }

    private void OnDestroy()
    {
        onPlayerDeath.onEventRaised -= OnPlayerDeath_onEventRaised;
        onBossDeath.onEventRaised -= OnBossDeath_onEventRaised;
    }

    private void OnBossDeath_onEventRaised()
    {
        playerCollider.enabled = false;
        boss.SetActive(false);
        Pool.Instance.GetItemFromPool(bigBloodParticles, boss.transform.position, boss.transform.rotation);
        Pool.Instance.GetItemFromPool(deadBoss, boss.transform.position, boss.transform.rotation);
        itemsToDeactivate.ForEach(t => t.SetActive(false));
        StartCoroutine("DeathOfBoss");
    }

    private void OnPlayerDeath_onEventRaised()
    {
        Pool.Instance.GetItemFromPool(bigBloodParticles, player.transform.position, player.transform.rotation);
        player.SetActive(false);
        StartCoroutine("RestartGame");
    }

    private IEnumerator DeathOfBoss()
    {
        yield return new WaitForSeconds(3);
        cam.Priority = 100;
        inputManager.enabled = false;
        Destroy(movement);
        anim.SetBool("IsMoving", false);
        yield return new WaitForSeconds(1);
        LetteredTextWriter writer = new LetteredTextWriter();
        yield return writer.WriteText(endText, 0.1f, "Sit down, dog", () => { });
        yield return new WaitForSeconds(0.5f);
        fadeCanvas.SetTrigger("FadeOut");
    }

    private IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}