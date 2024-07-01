using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioSource audioSource;
    private bool stopped;


    public void PlayMusic()
    {
        audioSource.Play();
        stopped = false;
    }

    public void PauseMusic()
    {
        audioSource.Pause();
    }

    public void StopMusic()
    {
        audioSource.Stop();
        stopped = true;
    }

    public bool GetStopped()
    {
        return stopped;
    }


}
