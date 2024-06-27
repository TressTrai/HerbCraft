using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && TriggerZone.playerDetection)
        {
            Debug.Log("Диалог");
        }
    }
}
