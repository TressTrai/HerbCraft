using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestObj : MonoBehaviour
{
    [SerializeField] private GameObject chestUI;

    private PersonalTriggerZone triggerZone;


    private void Awake()
    {
        triggerZone = gameObject.GetComponent<PersonalTriggerZone>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && triggerZone.PlayerDetection)
        {
            print("taptaptap");
            chestUI.SetActive(true);
        }

    }
}
