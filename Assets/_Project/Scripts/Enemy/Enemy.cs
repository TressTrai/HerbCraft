using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    //�������� �������������� ����������
    public float speed;
    public float speedChill;
    public int angerDistance;
    public float maxhp;
    public int money;

    protected float hp;

    private Player playerMoney;
    private TextMeshProUGUI textMesh;

    //������������� �����
    protected bool damageResist;


    //���� ������������ ����������
    public GameObject field;
    protected Transform fieldPoint;
    protected Field fieldScript;

    //���������� ��� ���������� ��������� ����������
    protected Vector2 ranVec ;


    //�������������� � �������
    protected Transform player;

    protected float timer = 0f;


    //��������� ����������
    protected bool chill = true;
    protected bool angry = true;

    //��������
    protected SpriteRenderer spriteRend;
    protected Color blink = new Color(240f / 255f, 189f / 255f, 201f / 255f);

    //�����
    public GameObject explotion;



    protected void Start()
    {
        fieldPoint = field.transform;
        fieldScript = field.GetComponent<Field>();
        player = GameObject.FindGameObjectWithTag("PlayerBody").transform;
        playerMoney = GameObject.FindGameObjectWithTag("PlayerBody").GetComponent<Player>();
        spriteRend = GetComponent<SpriteRenderer>();
        hp = maxhp;
        ranVec = new Vector2(fieldPoint.position.x, fieldPoint.position.y);
        textMesh = GameObject.FindGameObjectWithTag("Money").GetComponent<TextMeshProUGUI>();
    }

    protected virtual void Update()
    {
        if(!fieldScript.GetDetection() || Vector2.Distance(transform.position, player.position) > angerDistance)
        {
            chill = true;
        }

        if (Vector2.Distance(transform.position, player.position) <= angerDistance && fieldScript.GetDetection())
        {
            angry = true;
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

    protected void Chill() // ����� ���� �������, �� �� ����� ������ �� ��������� ������
    {
        timer += Time.deltaTime;
        if (timer >= 0.5)
        {
            if (transform.position.x > ranVec.x)
            {
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            transform.position = Vector2.MoveTowards(transform.position, ranVec, speedChill * Time.deltaTime);
        }
        else
        {
            PlusHp();
        }

        if (new Vector2(transform.position.x, transform.position.y) == ranVec)
        {
            timer = 0;
            RandomStep();
        }
    }

    protected virtual void Angry() // ����� ���� ����������, �� ������ ��� ��������� � ������� ������
    {
        if (transform.position.x >  player.position.x)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }

    protected void RandomStep() // ��������� ��������� ����
    {
        ranVec = fieldScript.GetRandomPoint();
    }

    public void SubHp(float dmg) // ��������� ����� �����
    {
        if (!damageResist)
        {
            hp -= dmg;
            damageResist = true;
            spriteRend.material.color = blink;
            if (hp <= 0)
            {
                Death();
            }

            Invoke("ResetMaterial", .2f);
            Invoke("DropResist", .2f);
        }
    }

    protected void DropResist() // ����� ������������� �� ��������� ����� �����
    {
        damageResist = false;
    }

    protected void ResetMaterial() // ���������� ���� ����� � ���������
    {
        spriteRend.material.color = Color.white;
    }

    protected void PlusHp() // ��� �����
    {
        if (hp < maxhp)
        {
            hp++;
        }
    }

    protected void Death() // ������ �����
    {
        gameObject.SetActive(false);
        explotion.transform.position = gameObject.transform.position;
        explotion.SetActive(true);

        playerMoney.currentMoney = playerMoney.currentMoney + new Money(money);
        textMesh.text = playerMoney.currentMoney.amount.ToString();

        Invoke("Respawn",5f);
    }

    protected void Respawn() // ����������� �����
    {
        hp = maxhp;
        gameObject.SetActive(true);
        explotion.SetActive(false);
        gameObject.transform.position = fieldPoint.position;
        gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
