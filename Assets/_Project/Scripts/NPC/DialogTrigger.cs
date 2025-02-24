using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    public GameObject dialogueFild;
    private Dialogue scriptDialogue;

    public GameObject objectToDisappear;

    public GameObject player;
    private Movement scriptMovement;

    private TriggerZone zone;

    private void Start()
    {
        scriptDialogue = dialogueFild.GetComponent<Dialogue>();
        scriptMovement = player.GetComponent<Movement>();
        zone = gameObject.GetComponent<TriggerZone>();
        dialogueFild.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.E) && scriptMovement.GetFreeze())
        {
            scriptDialogue.ScipText();
        }

        if (Input.GetKeyUp(KeyCode.E) && zone.GetDetection() && !scriptMovement.GetFreeze())
        {
            dialogueFild.SetActive(true);
            objectToDisappear.SetActive(false);
            scriptMovement.Freeze();
            Invoke("StartDialogue", 0.001f);
        }

        if (scriptMovement.GetFreeze() && !dialogueFild.activeSelf)
        {
            scriptMovement.UnFreeze();
            objectToDisappear.SetActive(true);
        }

    }

    private void StartDialogue()
    {
        scriptDialogue.StartDialogue();
    }

}
