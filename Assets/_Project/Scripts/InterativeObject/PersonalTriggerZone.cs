using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonalTriggerZone : MonoBehaviour
{
    public GameObject objectToDisappear;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        triggeredName = collision.name;
        playerDetection = true;
        objectToDisappear.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerDetection = false;
        objectToDisappear.SetActive(false);
    }

}
