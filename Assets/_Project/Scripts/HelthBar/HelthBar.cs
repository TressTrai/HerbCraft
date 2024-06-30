using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelthBar : MonoBehaviour
{
    Image healthBar;
    public float maxHealth = 100;
    public float Hp;
    private bool canDamage = true;

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

    public void SubHp(bool start, int damageAmount, float delay)
    {
        if (start)
        {
            if (canDamage)
            {
                StartCoroutine(DamageOverTime(damageAmount, delay));
            }
        }
        else
        {
            StopCoroutine(DamageOverTime(damageAmount, delay));
        }
    }

    private IEnumerator DamageOverTime(int damageAmount, float delay)
    {
        canDamage = false;
        ChangeHp(GetHp() - damageAmount);
        yield return new WaitForSeconds(delay);
        canDamage = true;
    }
}
