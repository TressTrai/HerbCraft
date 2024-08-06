using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioSource[] audioSource;


    public void PlayMusic(int index)
    {
        audioSource[index].Play();
    }

    public void PauseMusic(int index)
    {
        audioSource[index].Pause();
    }

    public void UnPauseMusic(int index)
    {
        audioSource[index].UnPause();
    }

    public void StopMusic(int index)
    {
        audioSource[index].Stop();
    }

    public float Time(int index)
    {
        return audioSource[index].time;
    }
}
