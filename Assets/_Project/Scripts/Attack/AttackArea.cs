using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    public int damage;
    public int counter = 6;

    private StickIndicator stickIndicator;

    //Сопротивление урону
    private bool damageResist;


    private void Start()
    {
        stickIndicator = GameObject.FindGameObjectWithTag("StickIndicator").GetComponent<StickIndicator>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<Enemy>() != null)
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            enemy.SubHp(damage);
            CounterManager();
        }
    }

    private void CounterManager()
    {
        if (!damageResist)
        {
            damageResist = true;
            counter -= 1;
            if (counter == 0)
            {
                counter = 6;
                stickIndicator.Sub();
            }
            Invoke("DropResist", .2f);
        }
    }

    private void DropResist()
    {
        damageResist = false;
    }
}
