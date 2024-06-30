using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    private int duration = 60;
    private int timeRemaining;
    private bool isCountingDown = false;
    private float timeLeft = 60f;

    public Countdown(int newDuration)
    {
        duration = newDuration;
        timeLeft = duration;
    }

    public void Begin()
    {
        if (!isCountingDown)
        {
            isCountingDown = true;
            timeRemaining = duration;
        }
    }

    private void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
        }
    }
}