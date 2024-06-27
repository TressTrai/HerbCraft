using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    public GameObject objectToDisappear;
    public static bool playerDetection;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("»грок зашЄл в зону");
        playerDetection = true;
        objectToDisappear.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("»грок вышел из зоны");
        playerDetection = false;
        objectToDisappear.SetActive(false);
    }
}
