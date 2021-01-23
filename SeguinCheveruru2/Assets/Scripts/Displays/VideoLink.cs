using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoLink : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<VideoPlayer>().url = System.IO.Path.Combine(Application.streamingAssetsPath, "movie.mp4");
        GetComponent<VideoPlayer>().Play();
    }
}
