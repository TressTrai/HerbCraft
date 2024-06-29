using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonalTriggerZone : MonoBehaviour
{
    public GameObject objectToDisappear;
    private bool playerDetection;

    public bool PlayerDetection
    {
        get { return playerDetection; }
    }

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

}
