using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 10;
    private float x;
    private float y;
    private Rigidbody2D rb;

    public bool freeze = false;
    public MusicPlayer audioSource;

    public Animator animator;

    private MusicPlayer musicPlayer;
    public int walkSound=3;

    public Joystick joystick;
    private bool onMobile = false; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        musicPlayer = GameObject.FindGameObjectWithTag("MusicPlayer").GetComponent<MusicPlayer>();

#if !UNITY_EDITOR
        if (Yandex.DeviceInfo() == "mobile")
        {
            onMobile = true;
        }
        else
        {
            GameObject.FindGameObjectWithTag("MobileMovement").SetActive(false);
        }
#endif
        GameObject.FindGameObjectWithTag("MobileMovement").SetActive(false); // При билде Удалить эту строку
    }

    void FixedUpdate()
    {
        if (!onMobile)
        {
            x = Input.GetAxisRaw("Horizontal");
            y = Input.GetAxisRaw("Vertical");
        }
        else
        {
            x = joystick.Horizontal;
            y = joystick.Vertical;
        }

        if(x < 0 && !freeze)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (x > 0 && !freeze)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        if (x == 0 && y == 0 || freeze)
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
