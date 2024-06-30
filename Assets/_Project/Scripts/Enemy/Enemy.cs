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

    private float ranX = 1f;
    private float ranY = 1f;
    private Vector2 ranVec ;

    private Transform player;

    private HelthBar helthBar;

    private float timer = 0f;

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
            timer += Time.deltaTime;
            if(timer >= 1)
            {
                helthBar.SubHp(damage);
                timer = 0;
            }
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
        timer += Time.deltaTime;
        if (timer >= 0.5)
        {
            transform.position = Vector2.MoveTowards(transform.position, ranVec, speed * Time.deltaTime);
        }
        else
        {
            PlusHp();
        }

        if (new Vector2(transform.position.x, transform.position.y) == ranVec)
        {
            timer = 0;
            RandomeStep();
        }
    }

    void Angry()
    {
        if (transform.position.x >  player.position.x)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        transform.position = Vector2.MoveTowards(transform.position, player.position,speed * Time.deltaTime);
    }

    void GoBack()
    {
        if (transform.position != fieldPoint.position)
        {
            transform.position = Vector2.MoveTowards(transform.position, fieldPoint.position, speed * Time.deltaTime);
            
            if (transform.position.x > fieldPoint.position.x)
            {
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
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
        if(ranX < 0)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (ranX > 0)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        ranVec = new Vector2(transform.position.x + ranX, transform.position.y + ranY);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBody"))
        {
            doDamage = true;
            helthBar.SubHp(damage);
            timer = 0f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBody"))
        {
            doDamage = false;
            timer = 0f;
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
        gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
