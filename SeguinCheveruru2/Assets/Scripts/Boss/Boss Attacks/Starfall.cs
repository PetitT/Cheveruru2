using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starfall : BossAttack
{
    public int numberOfStars;
    public GameObject star;
    public float preWaitTime;
    public float postWaitTime;
    public Vector2 timeBetweenStarThrows;
    public Vector2 starsSpeed;
    public Transform topRight, bottomLeft;
    public Transform attackPosition;
    public Transform endOfAttackPosition;
    public float movementSpeed;
    public AudioClip jumpClip;
    public GameObject starBounds;
    private float securityDistance = 0.5f;

    private List<StarProjectile> stars = new List<StarProjectile>();

    private void Start()
    {
        for (int i = 0; i < numberOfStars; i++)
        {
            GameObject newStar = Pool.Instance.GetItemFromPool(star, GetStarPosition(), Quaternion.identity);
            stars.Add(newStar.GetComponent<StarProjectile>());
        }
    }

    private Vector3 GetStarPosition()
    {
        Collider2D col = starBounds.GetComponents<BoxCollider2D>().GetRandom();
        float XSize = col.bounds.size.x / 2;
        float YSize = col.bounds.size.y / 2;
        float X = new Vector2(col.bounds.center.x - XSize, col.bounds.center.x + XSize).RandomRange();
        float Y = new Vector2(col.bounds.center.y - YSize, col.bounds.center.y + YSize).RandomRange();
        
        return new Vector3(X, Y, -1);
    }

    public override IEnumerator Attack(Action onFinish)
    {
        CameraTarget.Instance.ToggleRecenter(true);
        yield return new WaitForSeconds(preWaitTime);
        BossDirection.Instance.ToggleRotation(false);
        audioSrc.PlayOneShot(jumpClip);
        BossAnimation.Instance.Animate(BossAnimation.BossAnim.AirIdle);
        yield return GoToTarget(attackPosition.position);
        yield return ThrowStars();
        yield return GoToTarget(endOfAttackPosition.position);
        BossAnimation.Instance.Animate(BossAnimation.BossAnim.StandingIdle);
        BossDirection.Instance.ToggleRotation(true);
        yield return new WaitForSeconds(postWaitTime);
        CameraTarget.Instance.ToggleRecenter(false);
        onFinish?.Invoke();
        StartCoroutine(RespawnStars());
    }

    public override void CancelAttack()
    {
        StopCoroutine(Attack(() => { }));
        StartCoroutine(GoToTarget(endOfAttackPosition.position));
    }

    private IEnumerator GoToTarget(Vector2 targetPos)
    {
        while (Vector2.Distance(transform.position, targetPos) > securityDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPos, movementSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;
    }

    private IEnumerator ThrowStars()
    {
        for (int i = 0; i < stars.Count; i++)
        {
            stars[i].Activate(starsSpeed.RandomRange());
            yield return new WaitForSeconds(timeBetweenStarThrows.RandomRange());
        }
    }

    private IEnumerator RespawnStars()
    {
        yield return new WaitForSeconds(3);
        for (int i = 0; i < stars.Count; i++)
        {
            stars[i].gameObject.SetActive(false);
            stars[i].transform.position = GetStarPosition();
            stars[i].Deactivate();
            stars[i].GetComponent<TrailRenderer>().Clear();
            stars[i].gameObject.SetActive(true);
        }
    }
}
