using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager soundManagerInstance;
    public static AudioClip jump, coin, backgroundMusic;
    static AudioSource audioSrc;


    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);

        jump = Resources.Load<AudioClip>("jump");
        coin = Resources.Load<AudioClip>("feather");
        backgroundMusic = Resources.Load<AudioClip>("backgroundMusic");

        audioSrc = GetComponent<AudioSource>();
        audioSrc.volume = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound(string action)
    {

        switch (action)
        {
            case "jump":
                audioSrc.PlayOneShot(jump);
                break;
            case "feather":
                audioSrc.PlayOneShot(coin);
                break;
        }
    }
}
