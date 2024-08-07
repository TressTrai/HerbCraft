using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 10;
    private Rigidbody2D rb;

    public bool freeze = false;
    public MusicPlayer audioSource;

    public Animator animator;

    private MusicPlayer musicPlayer;
    public int walkSound=3;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        musicPlayer = GameObject.FindGameObjectWithTag("MusicPlayer").GetComponent<MusicPlayer>();
    }

    void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if(x== -1)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (x == 1)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        if (x == 0 && y == 0)
        {
            animator.SetTrigger("Stay");
            musicPlayer.PauseMusic(walkSound);
        }
        else
        {
            animator.SetTrigger("Walk");
            musicPlayer.UnPauseMusic(walkSound);
        }

        rb.velocity = new Vector2(x,y).normalized * speed;

    }

    public bool GetFreeze()
    {
        return freeze;
    }

    public void Freeze()
    {
        speed = 0;
        freeze = true;
    }

    public void UnFreeze()
    {
        speed = 10;
        freeze = false;
    }
}
