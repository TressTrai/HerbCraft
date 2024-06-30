using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Money
{
    public int amount = 0;

    public Money(int amount)
    {
        this.amount = amount;
    }

    public static Money operator +(Money money1, Money money2)
    {
        return new Money(money1.amount + money2.amount);
    }
}

public class Player : MonoBehaviour
{
    public Task currentTask;
    public Money currentMoney = new Money(0);
}
