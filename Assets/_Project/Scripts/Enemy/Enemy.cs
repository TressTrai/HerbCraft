using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float step;
    public int angerDistance;
    public float maxhp;
    public int damage;

    private float hp;

    public GameObject field;
    private Transform fieldPoint;
    private Field fieldScript;

    public GameObject enemy;

    private float ranX = 1f;
    private float ranY = 1f;
    private Vector2 ranVec;

    private Transform player;

    private HelthBar helthBar;

    private bool chill = true;
    private bool angry = true;
    private bool goBack = true;
    private bool doDamage = false;

    private void Start()
    {
        fieldPoint = field.transform;
        fieldScript = field.GetComponent<Field>();
        player = GameObject.FindGameObjectWithTag("PlayerBody").transform;
        helthBar = GameObject.FindGameObjectWithTag("HelthBar").GetComponent<HelthBar>();
        hp = maxhp;
        RandomeStep();
    }

    private void Update()
    {

        if(fieldScript.GetDetection() && !angry && !goBack)
        {
            chill = true;
        }

        if (Vector2.Distance(transform.position, player.position) < angerDistance && fieldScript.GetDetection())
        {
            angry = true;
            chill = false;
            goBack = false;
        }

        if (!fieldScript.GetDetection())
        {
            goBack = true;
            angry = false;
            chill = false;
        }

        if (doDamage)
        {
            //Надо что-то сделать с тем, что он толкает игрока
        }
        else if (chill)
        {
            Chill();
        }
        else if (angry)
        {
            Angry();
        }
        else if (goBack)
        {
            GoBack();
        }


    }

    void Chill()
    {
        PlusHp();
        transform.position = Vector2.MoveTowards(transform.position, ranVec , speed * Time.deltaTime);
        if (new Vector2(transform.position.x, transform.position.y) == ranVec)
        {
            //Invoke("RandomeStep", 0.5f);  Прикольно трясётся вот и всё
            RandomeStep();
        }
    }

    void Angry()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position,speed * Time.deltaTime);
    }

    void GoBack()
    {
        if (transform.position != fieldPoint.position)
        {
            transform.position = Vector2.MoveTowards(transform.position, fieldPoint.position, speed * Time.deltaTime);
        }
        else
        {
            chill = true;
            angry = false;
            goBack = false;
            RandomeStep();
        }
    }

    public void RandomeStep()
    {
        ranX = Random.Range(-step, step);
        ranY = Random.Range(-step, step);
        ranVec = new Vector2(transform.position.x + ranX, transform.position.y + ranY);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBody"))
        {
            doDamage = true;
            helthBar.SubHp(true, damage, 1f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBody"))
        {
            doDamage = false;
            helthBar.SubHp(false, damage, 1f);
        }
    }



    public void SubHp(float dmg)
    {
        hp -= dmg;
        if(hp<= 0)
        {
            Death();
        }
    }

    private void PlusHp()
    {
        if (hp < maxhp)
        {
            hp++;
        }
    }

    private void Death()
    {
        gameObject.SetActive(false);

        Invoke("Respawn",5f);
    }

    private void Respawn()
    {
        hp = maxhp;
        gameObject.SetActive(true);
        gameObject.transform.position = fieldPoint.position;
    }
}
