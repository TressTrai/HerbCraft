using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit : Button
{
    public MusicPlayer audioSource;

    void Start()
    {
        //audioSource.PlayMusic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
