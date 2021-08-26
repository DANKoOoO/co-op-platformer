using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class IntroAnimation : MonoBehaviour
{
    private VideoPlayer video;

    // Start is called before the first frame update
    void Start()
    {
        video = GetComponent<VideoPlayer>();
        video.Play();
        StartCoroutine("WaitForMovieEnd");
    }

    void Update() 
    {
        if (Input.anyKey) 
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public IEnumerator WaitForMovieEnd()
    {
        while (!video.isPrepared || video.isPlaying)
        {
            yield return new WaitForEndOfFrame();

        }
        OnMovieEnded();
    }

    void OnMovieEnded()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
