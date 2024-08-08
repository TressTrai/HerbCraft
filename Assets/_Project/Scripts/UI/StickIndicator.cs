using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StickIndicator : MonoBehaviour
{
    private Color good = new Color(164f / 255f, 231f / 255f, 110f / 255f);
    private Color medium = new Color(236f / 255f, 235f / 255f, 115f / 255f);
    private Color bad = new Color(222f / 255f, 59f / 255f, 49f / 255f);

    private int lvl = 3;
    private Image image;

    public GameObject objectAttackArea;
    private AttackArea attackArea;

    private void Start()
    {
        image = gameObject.GetComponent<Image>();
        attackArea = objectAttackArea.GetComponent<AttackArea>();
    }

    public void StatusCheck()
    {
        switch (lvl)
        {
            case 1:
                image.color = bad;
                break;
            case 2:
                image.color = medium;
                break;
            case 3:
                image.color = good;
                break;
        }
    }

    public void Sub()
    {
        if (lvl != 1)
        {
            lvl -= 1;
            attackArea.damage -= 2;
        }
        StatusCheck();
    }

    public void Add()
    {
        if (lvl != 3)
        {
            lvl += 1;
            attackArea.damage += 2;
            attackArea.counter = 6;
        }
        StatusCheck();
    }
}
