using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooBusyToPlaceCloudsKek : MonoBehaviour
{
    public GameObject cloud;
    public List<Sprite> sprites;

    public Transform bottomRight;
    public Transform topLeft;
    public int numberOfClouds;
    public float securityDistance;

    private List<GameObject> currentClouds = new List<GameObject>();

    [ContextMenu("PlaceClouds")]
    public void PlaceClouds()
    {
        for (int i = 0; i < numberOfClouds; i++)
        {
            GameObject newCloud = Instantiate(cloud);
            newCloud.transform.position = GetCloudPosition();
            newCloud.GetComponent<SpriteRenderer>().flipX = Helper.GetRandomBool();
            newCloud.GetComponent<SpriteRenderer>().sprite = sprites.GetRandom();

            currentClouds.Add(newCloud);
        }
    }

    private Vector2 GetCloudPosition()
    {
        float X = new Vector2(topLeft.position.x, bottomRight.position.x).RandomRange();
        float Y = new Vector2(bottomRight.position.y, topLeft.position.y).RandomRange();
        Vector2 randomPoint = new Vector2(X, Y);
        if (IsTooClose(randomPoint))
        {
            return GetCloudPosition();
        }
        return randomPoint;
    }

    private bool IsTooClose(Vector2 randomPoint)
    {
        if(currentClouds.Count == 0) { return false; }

        for (int i = 0; i < currentClouds.Count; i++)
        {
            float distance = Vector2.Distance(randomPoint, currentClouds[i].transform.position);
            if(distance < securityDistance)
            {
                return true;
            }
        }
        return false;
    }
}
