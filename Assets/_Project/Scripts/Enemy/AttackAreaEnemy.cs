using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAreaEnemy : MonoBehaviour
{
    public int damage;
    public float liveTime;
    private bool attacking = false;
    private float attackTimer = 0f;

    private HelthBar helthBar;
    public GameObject attackArea;

    private void Start()
    {
        helthBar = GameObject.FindGameObjectWithTag("HelthBar").GetComponent<HelthBar>();
    }

    private void Update()
    {
        if (attacking)
        {
            attackTimer += Time.deltaTime;
        }

        if(attacking && attackTimer > liveTime)
        {
            attackArea.SetActive(false);
        }
    }

    private void OnEnable()
    {
        attackTimer = 0f;
        attacking = true;
    }

    private void OnDisable()
    {
        attacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("PlayerBody"))
        {
            helthBar.SubHp(damage);
            attackArea.SetActive(false);
        }
    }
}
