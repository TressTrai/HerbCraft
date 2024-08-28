using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poludennitsa : Enemy
{
    public GameObject attackArea;
    public Animator animator;
    private bool canAttack = true;
    protected override void Angry() // Когда Полуденница агрессивна, то меняет своё положение в сторону игрока
    {
        if (Vector2.Distance(transform.position, player.position) > 1.5f)
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
        
        if(canAttack && Vector2.Distance(transform.position, player.position) <= 3.5f)
        {
            Invoke("Attack", 0.5f);
            animator.SetTrigger("Attack");
            canAttack = false;
        }
    }

    private void Attack()
    {
        attackArea.SetActive(true);
        Invoke("AttackReload", attackArea.GetComponent<AttackAreaEnemy>().liveTime);
    }

    private void AttackReload()
    {
        canAttack = true;
    }

}
