using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HelthBar : MonoBehaviour
{
    Image healthBar;
    public float maxHealth = 100;
    private float Hp;
    private GameObject player;
    private Inventory inventory;
    private Player money;
    private TextMeshProUGUI textMesh;

    private void Start()
    {
        healthBar = GetComponent<Image>();
        player = GameObject.FindGameObjectWithTag("PlayerBody");
        money = player.GetComponent<Player>();
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>() ;
        Hp = maxHealth;
        textMesh = GameObject.FindGameObjectWithTag("Money").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        healthBar.fillAmount = Hp / maxHealth;
    }

    public void ChangeHp(float newHp)
    {
        Hp = newHp;
    }

    public float GetHp()
    {
        return Hp;
    }

    public void SubHp(int damageAmount)
    {
        ChangeHp(GetHp() - damageAmount);
        if (Hp <= 0)
        {
            Respawn();
        }
    }

    private void Respawn()
    {
        Hp = maxHealth;
        player.transform.position = new Vector3(0.08f, 2.08f, 0);
        inventory.ClearInventory();
        money.currentMoney = new Money(0);
        textMesh.text = money.currentMoney.amount.ToString();
    }
}
