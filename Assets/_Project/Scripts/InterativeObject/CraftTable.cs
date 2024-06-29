using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CraftTable : MonoBehaviour
{
    [SerializeField] private string[] craftIngridientsNames;
    [SerializeField] private int playerCraftDuration = 0;
    [SerializeField] private int stationCraftDuration = 0;

    private GameObject inventory;
    private PersonalTriggerZone triggerZone;

    private Item chosenItem;
    

    private void Awake()
    {
        inventory = GameObject.FindGameObjectWithTag("Inventory");
        triggerZone = gameObject.GetComponent<PersonalTriggerZone>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && triggerZone.PlayerDetection)
        {
            RemoveItemFromInventory();
            StartCraft();
        }
    }

    private void RemoveItemFromInventory()
    {
        Inventory inv = inventory.GetComponent<Inventory>();
        int chosenItemId = inv.ChosenItem;
        chosenItem = inv.GetItemById(chosenItemId);
        if (chosenItem != null && chosenItem.CanUseInCraft && craftIngridientsNames.Contains(chosenItem.Name))
        {
            inv.RemoveItem(chosenItemId);
        }
        else
        {
            print("can't use this item");
        }
    }

    private void StartCraft()
    {
        // TODO
    }
}
