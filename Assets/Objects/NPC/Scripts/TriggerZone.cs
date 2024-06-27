using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    public GameObject objectToDisappear;
    public static bool playerDetection;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("����� ����� � ����");
        playerDetection = true;
        objectToDisappear.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("����� ����� �� ����");
        playerDetection = false;
        objectToDisappear.SetActive(false);
    }
}
