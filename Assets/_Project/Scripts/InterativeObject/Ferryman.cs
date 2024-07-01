using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] GameObject taskField;
    private PersonalTriggerZone triggerZone;
    private Player player;
    private Inventory inv;
    private TextMeshProUGUI textMesh;

    private void Awake()
    {
        inv = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        player = GameObject.FindGameObjectWithTag("PlayerBody").GetComponent<Player>();
        triggerZone = gameObject.GetComponent<PersonalTriggerZone>();
        textMesh = GameObject.FindGameObjectWithTag("Money").GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && triggerZone.PlayerDetection && player != null && player.currentTask != null)
        {
            RemoveItemFromInventory();
        }
    }

    private void RemoveItemFromInventory()
    {
        int chosenItemId = inv.ChosenItem;
        Item chosenItem = inv.GetItemById(chosenItemId);
        if (chosenItem != null && chosenItem.CanUseInCraft && CanItemBeUsed(chosenItem.Name))
        {
            inv.RemoveItem(chosenItemId);
            GiveMoney();
            ResetTask();
        }
        else
        {
            print(chosenItem.Name);
            print(player.currentTask.objectName);
            print("can't use this item");
        }
    }

    private bool CanItemBeUsed(string itemName)
    {
        return (itemName == player.currentTask.objectName);
    }

    private void GiveMoney()
    {
        print("asd");
        print(textMesh.text);
        print(player.currentMoney.amount.ToString());
        player.currentMoney += new Money(player.currentTask.cost);
        textMesh.text = player.currentMoney.amount.ToString();
    }

    private void ResetTask()
    {
        print("reseting task");
        player.currentTask = null;
        taskField.SetActive(false);
    }
}
