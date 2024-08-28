using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leshii : Enemy
{
    public GameObject attackArea;
    public Animator animator;
    private bool canAttack = true;

    public GameObject charge;

    protected override void Angry() // Когда Леший агрессивен, то убегает от игрока
    {
        if (Vector2.Distance(transform.position, player.position) < 3f)
        {
            if (transform.position.x < player.position.x)
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

            transform.position = Vector2.MoveTowards(transform.position, transform.position + (transform.position - player.position).normalized, speed * Time.deltaTime);
        }

        if (!attackArea.activeSelf && canAttack)
        {
            Invoke("Attack",1f);
            animator.SetTrigger("Attack");
            canAttack = false;
            charge.SetActive(false);
        }
    }

    private void Attack()
    {
        charge.transform.position = gameObject.transform.position;
        charge.SetActive(true);
        attackArea.transform.position = transform.position;
        attackArea.SetActive(true);
        canAttack = true;
    }
}
