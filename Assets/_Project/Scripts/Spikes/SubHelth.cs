using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubHelth : MonoBehaviour
{
    public GameObject helthBar;
    private bool inside;
    private HelthBar script;
    private bool canDamage = true;

    private void Start()
    {
        script = helthBar.GetComponent<HelthBar>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Зашёл в шипы");
        inside = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Вышел из шипов");
        inside = false;
    }

    private void LateUpdate()
    {
        if (inside && canDamage)
        {
            StartCoroutine(DamageOverTime(5, 0.5f));
        }
    }

    private IEnumerator DamageOverTime(int damageAmount, float delay)
    {
        canDamage = false;
        script.ChangeHp(script.GetHp() - damageAmount);
        yield return new WaitForSeconds(delay);
        canDamage = true;
    }
}
