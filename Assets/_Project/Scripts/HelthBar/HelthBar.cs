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

    private SpriteRenderer spriteRendPlayer;
    private SpriteRenderer spriteRendStick;

    //Моргание и взрыв
    private Color blink = new Color(240f / 255f, 189f / 255f, 201f / 255f);
    private Color blinkHeal = new Color(204f / 255f, 240f / 255f, 153f / 255f);
    public GameObject explotion;

    //Сопротивление урону
    private bool damageResist;

    //Индикатор слома палки
    private StickIndicator stickIndicator;

    private void Start()
    {
        healthBar = GetComponent<Image>();
        player = GameObject.FindGameObjectWithTag("PlayerBody");
        money = player.GetComponent<Player>();
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>() ;
        Hp = maxHealth;
        textMesh = GameObject.FindGameObjectWithTag("Money").GetComponent<TextMeshProUGUI>();
        spriteRendPlayer = GameObject.FindGameObjectWithTag("PlayerBody").GetComponent<SpriteRenderer>();
        spriteRendStick = GameObject.FindGameObjectWithTag("Stick").GetComponent<SpriteRenderer>();
        stickIndicator = GameObject.FindGameObjectWithTag("StickIndicator").GetComponent<StickIndicator>();
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
        if (!damageResist)
        {
            ChangeHp(GetHp() - damageAmount);
            damageResist = true;
            spriteRendPlayer.material.color = blink;
            spriteRendStick.material.color = blink;
            if (Hp <= 0)
            {
               Respawn(); //Данная строчка должна быть убрана из окончательного билда, для корректной работы игры!!!!!!!!!!!
                #if !UNITY_EDITOR
                                Yandex.PlayRewardedAdd();
                #endif
            }
            else
            {
                Invoke("ResetMaterialPlayer", .2f);
            }

            Invoke("DropResist", .2f);
        }
    }

    private void DropResist()
    {
        damageResist = false;
    }

    public void AddHp(int healAmount)
    {
        if(GetHp() + healAmount > maxHealth)
        {
            ChangeHp(maxHealth);
        }
        else
        {
            ChangeHp(GetHp() + healAmount);
        }
        spriteRendPlayer.material.color = blinkHeal;
        spriteRendStick.material.color = blinkHeal;

        explotion.transform.position = player.transform.position;
        explotion.SetActive(true);

        Invoke("ResetMaterialPlayer", .2f);
    }

    private void ResetMaterialPlayer()
    {
        spriteRendPlayer.material.color = Color.white;
        spriteRendStick.material.color = Color.white;
        explotion.SetActive(false);
    }

    private void Respawn()
    {
#if !UNITY_EDITOR
        Yandex.SendToLeaderBord(money.currentMoney.amount);
        Yandex.RateGamePopup();
#endif
        Hp = maxHealth;
        ResetMaterialPlayer();
        player.transform.position = new Vector3(0.08f, 2.08f, 0);
        inventory.ClearInventory();
        money.currentMoney = new Money(0);
        textMesh.text = money.currentMoney.amount.ToString();
        stickIndicator.Set(3);
    }

    private void RespawnWithAward()
    {
        Hp = maxHealth;
        ResetMaterialPlayer();
        player.transform.position = new Vector3(0.08f, 2.08f, 0);
        stickIndicator.Set(3);
        spriteRendPlayer.material.color = Color.black;
    }
}
