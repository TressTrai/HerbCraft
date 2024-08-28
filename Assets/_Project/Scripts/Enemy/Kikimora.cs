using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kikimora : Enemy
{
    public GameObject attackAreaNear;
    public GameObject attackAreaOut;
    public Animator animator;
    private bool canAttack = true;
    protected override void Update()
    {
        if (!fieldScript.GetDetection() || Vector2.Distance(transform.position, player.position) > angerDistance)
        {
            chill = true;
        }

        if (Vector2.Distance(transform.position, player.position) <= angerDistance && fieldScript.GetDetection())
        {
            angry = true;
            chill = false;
        }

        if (!canAttack)
        {
            chill = false;
        }


        if (chill)
        {
            Chill();
        }
        else if (angry)
        {
            Angry();
        }

    }
    protected override void Angry() // Когда Кикимора агрессивна, то она приближается к игроку
    {
        if (Vector2.Distance(transform.position, player.position) > 3f)
        {
            if (transform.position.x > player.position.x)
            {
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            }

            if (transform.position.y < player.position.y)
            {
                spriteRend.sortingOrder = 12;
            }
            else
            {
                spriteRend.sortingOrder = 10;
            }

            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }

        if (canAttack && Vector2.Distance(transform.position, player.position) <= 3f)
        {
            Invoke("AttackNear", 1f);
            animator.SetTrigger("Attack");
            canAttack = false;
            speed = 0;
            speedChill = 0;
        }
    }

    private void AttackNear()
    {
        attackAreaNear.SetActive(true);
        Invoke("AttackOut", attackAreaNear.GetComponent<AttackAreaEnemy>().liveTime + 0.5f);
    }

    private void AttackOut()
    {
        attackAreaOut.SetActive(true);
        Invoke("AttackReload", attackAreaOut.GetComponent<AttackAreaEnemy>().liveTime);
    }

    private void AttackReload()
    {
        canAttack = true;
        speed = 5;
        speedChill = 4;
    }
}
