using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuseOn : MonoBehaviour
{
    public MusicPlayer audioSource;

    void Start()
    {
        audioSource.PlayMusic();
    }
}
