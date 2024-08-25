using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bin : MonoBehaviour
{
    private GameObject cellsGridPlane;
    private GameObject player;
    private PersonalTriggerZone triggerZone;
    Movement playerMovement;

    void Awake()
    {
        triggerZone = gameObject.GetComponent<PersonalTriggerZone>();
        cellsGridPlane = GameObject.FindGameObjectWithTag("BinPlane");
        player = GameObject.FindGameObjectWithTag("PlayerBody");
        playerMovement = player.GetComponentInChildren<Movement>();

        Close();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) &&
            triggerZone.PlayerDetection &&
            triggerZone.TriggeredName == "PlayerBody" &&
            cellsGridPlane.activeSelf == false)
        {
            Open();
        }
    }

    public void Open()
    {
        cellsGridPlane.SetActive(true);
        playerMovement.Freeze();
    }
    public void Close()
    {
        cellsGridPlane.SetActive(false);
        playerMovement.UnFreeze();
    }
}
