using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 10;
    private Rigidbody2D rb;

    public bool freeze = false;
    public MusicPlayer audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource.PlayMusic();
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

        rb.velocity = new Vector2(speed * x,speed * y);
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
