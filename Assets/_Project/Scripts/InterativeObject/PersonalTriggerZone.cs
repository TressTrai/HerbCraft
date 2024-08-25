using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonalTriggerZone : MonoBehaviour
{
    public GameObject objectToDisappear;
    private bool ignoreTrigger = false;
    private bool playerDetection;
    private string triggeredName;

    public bool PlayerDetection
    {
        get { return playerDetection; }
    }
    public string TriggeredName
    {
        get { return triggeredName; }
    }
    public void SetIgnoreTrigger(bool shouldIgnore)
    {
        if (shouldIgnore) objectToDisappear.SetActive(false);
        ignoreTrigger = shouldIgnore;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (ignoreTrigger) return;

        triggeredName = collision.name;
        playerDetection = true;
        objectToDisappear.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (ignoreTrigger) return;

        playerDetection = false;
        objectToDisappear.SetActive(false);
    }

}
