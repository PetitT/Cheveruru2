using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroProjectile : MonoBehaviour, IRevertableProjectile
{
    public Transform up;
    public Transform down;

    public float speed;

    public GameObject difficultyChoose;

    public void OnRevert()
    {
        transform.position = up.position;
        difficultyChoose.SetActive(true);
    }

    private void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
        if (transform.position.y < down.position.y)
        {
            transform.position = up.position;
        }
    }


}
