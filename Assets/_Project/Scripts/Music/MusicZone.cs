using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicZone : MonoBehaviour
{
    private MusicPlayer musicPlayer;
    private Movement movement;
    public int changedMusic;
    public int changeTo;
    public int walkSound;

    void Start()
    {
        musicPlayer = GameObject.FindGameObjectWithTag("MusicPlayer").GetComponent<MusicPlayer>();
        movement = GameObject.FindGameObjectWithTag("PlayerBody").GetComponent<Movement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBody"))
        {
            musicPlayer.PauseMusic(changedMusic);
            musicPlayer.PauseMusic(movement.walkSound);
            if (musicPlayer.Time(changeTo) != 0)
            {
                musicPlayer.UnPauseMusic(changeTo);
                movement.walkSound = walkSound;
            }
            else
            {
                musicPlayer.PlayMusic(changeTo);
                musicPlayer.PlayMusic(walkSound);
                movement.walkSound = walkSound;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBody") && musicPlayer != null)
        {
            musicPlayer.PauseMusic(changeTo);
            musicPlayer.UnPauseMusic(changedMusic);
            musicPlayer.PauseMusic(walkSound);
            movement.walkSound = 3;
        }
    }
}
