using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelthBar : MonoBehaviour
{
    Image healthBar;
    public float maxHealth = 100;
    public float Hp;

    private void Start()
    {
        healthBar = GetComponent<Image>();
        Hp = maxHealth;
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
}
