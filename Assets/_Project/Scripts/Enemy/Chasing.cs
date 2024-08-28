using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chasing : MonoBehaviour
{
    public float speed;
    protected Transform player;
    protected bool attacking=false;

    protected void Start()
    {
        player = GameObject.FindGameObjectWithTag("PlayerBody").transform;
    }

    private void OnEnable()
    {
        attacking = true;
    }

    private void OnDisable()
    {
        attacking = false;
    }

    private void Update()
    {
        if (attacking)
        {
            Chase();
        }
    }

    private void Chase()
    {
        // находим направление поворота
        Vector3 direction = (player.position - transform.position).normalized;

        // Устанавливаем угол поворота 
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle-90));

        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }
}
