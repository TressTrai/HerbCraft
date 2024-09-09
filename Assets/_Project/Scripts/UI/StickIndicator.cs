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

    //Звуки
    private MusicPlayer musicPlayer;

    private void Start()
    {
        image = gameObject.GetComponent<Image>();
        attackArea = objectAttackArea.GetComponent<AttackArea>();
        musicPlayer = GameObject.FindGameObjectWithTag("MusicPlayer").GetComponent<MusicPlayer>();
    }

    public void StatusCheck()
    {
        switch (lvl)
        {
            case 1:
                image.color = bad;
                attackArea.damage = 1;
                break;
            case 2:
                image.color = medium;
                attackArea.damage = 3;
                break;
            case 3:
                image.color = good;
                attackArea.damage = 5;
                break;
        }
    }

    public void Sub()
    {
        if (lvl != 1)
        {
            lvl -= 1;
            musicPlayer.PlayMusic(7);
        }
        StatusCheck();
    }

    public void Add()
    {
        if (lvl != 3)
        {
            lvl += 1;
        }
        attackArea.counter = 6;
        StatusCheck();
    }

    public void Set(int setLvl)
    {
        lvl = setLvl;
        StatusCheck();
    }
}
