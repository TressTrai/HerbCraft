using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    public GameObject objectToDisappear;
    private bool playerDetection;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerDetection = true;
        objectToDisappear.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerDetection = false;
        objectToDisappear.SetActive(false);
    }

    public bool GetDetection()
    {
        return playerDetection;
    }
}
