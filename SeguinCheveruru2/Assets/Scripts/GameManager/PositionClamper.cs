using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionClamper : MonoBehaviour
{
    public Transform leftWall;
    public Transform rightWall;
    public Transform groundPosition;

    public Transform[] charactersToClamp;

    public void Update()
    {
        for (int i = 0; i < charactersToClamp.Length; i++)
        {
            float X = Mathf.Clamp(charactersToClamp[i].position.x, leftWall.position.x, rightWall.position.x);
            float Y = Mathf.Clamp(charactersToClamp[i].position.y, groundPosition.position.y, 1000);
            charactersToClamp[i].transform.position = new Vector2(X, Y);
        }
    }
}
