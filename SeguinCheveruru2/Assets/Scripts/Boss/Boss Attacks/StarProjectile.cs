using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarProjectile : MonoBehaviour, IRevertableProjectile
{
    public float timeToStart;
    public float speedMultiplication = 3;
    public float displaySpeed = 1;
    private bool isActivated = false;
    private Vector2 direction;
    private ParticleSystem particle;
    private SpriteRenderer sprite;
    private AudioSource audioSrc;
    private float speed;

    private void Awake()
    {
        particle = GetComponentInChildren<ParticleSystem>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        audioSrc = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        isActivated = false;
        StartCoroutine("DisplayStar");
    }

    public void Activate(float speed)
    {
        this.speed = speed;
        StartCoroutine(BeginCharge());
    }

    private IEnumerator DisplayStar()
    {
        float currentDisplay = 0;
        while (currentDisplay < 1)
        {
            currentDisplay += Time.deltaTime * displaySpeed;
            sprite.color = sprite.color.ModifyAlpha(currentDisplay);
            yield return null;
        }
    }

    private IEnumerator BeginCharge()
    {
        particle.Play();
        yield return new WaitForSeconds(timeToStart);
        audioSrc.Play();
        direction = ((Vector2)CharacterPosition.Instance.transform.position - (Vector2)transform.position).normalized;
        isActivated = true;
    }

    private void Update()
    {
        if (!isActivated) { return; }
        Move();
    }

    private void Move()
    {
        transform.Translate(direction * speed * TimeManager.Instance.DeltaTime.Value);
    }

    public void Deactivate()
    {
        isActivated = false;
    }

    public void OnRevert()
    {
        Vector2 newDirection = (BossDirection.Instance.transform.position - transform.position).normalized;
        GetComponent<DamageDealer>().currentOrigin = DamageOrigin.allied;
        direction = newDirection;
        speed *= speedMultiplication;
    }
}
