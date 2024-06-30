using System.Collections;
using UnityEngine;

public class TimerCountdown
{
    private static float currCountdownValue;
    public static IEnumerator StartCountdown(TimerCallback callback, float countdownValue = 10)
    {
        currCountdownValue = countdownValue;
        while (currCountdownValue > 0)
        {
            yield return new WaitForSeconds(1.0f);
            currCountdownValue--;
        }
        callback();
    }
}