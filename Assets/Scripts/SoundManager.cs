using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public AudioSource BGMSource, playerSource, gunSource, portalSource;
    public AudioClip BGM, hurt, powerUp, shoot, portal;

    private void Awake()
    {
        if (Instance == null) Instance = this;

        if (Instance == this)
        {
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }

        BGMSource.clip = BGM;
        gunSource.clip = shoot;
        portalSource.clip = portal;
    }

    private void Start()
    {
        PlayBGMLoop();
    }

    private void PlayBGMLoop()
    {
        BGMSource.Play();
    }

    public void PlayHurt()
    {
        playerSource.PlayOneShot(hurt, 1f);
    }

    public void PlayPowerUp()
    {
        playerSource.PlayOneShot(powerUp, 0.8f);
    }

    public void PlayShoot()
    {
        gunSource.Play();
    }

    public void PlayPortal()
    {
        portalSource.Play();
    }
}
