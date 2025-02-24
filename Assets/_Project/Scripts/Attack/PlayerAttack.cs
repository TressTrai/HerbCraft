using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float timeToAttack = 1f;
    private float timer = 0f;

    private GameObject attackArea = default;
    public Animator animator;

    private bool attacking = false;

    private MusicPlayer musicPlayer;

    private Movement movement;

    void Start()
    {
        attackArea = transform.GetChild(0).gameObject;
        musicPlayer = GameObject.FindGameObjectWithTag("MusicPlayer").GetComponent<MusicPlayer>();
        movement = GameObject.FindGameObjectWithTag("PlayerBody").GetComponent<Movement>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !attacking && !movement.GetFreeze()) {
            Attack();
        }

        if (attacking)
        {
            timer += Time.deltaTime;

            if(timer >= timeToAttack)
            {
                timer = 0;
                attacking = false;
                attackArea.SetActive(attacking);
            }
        }
    }

    private void Attack()
    {
        attacking = true;
        attackArea.SetActive(attacking);
        animator.SetTrigger("Attack");
        musicPlayer.PlayMusic(6);
    }
}
