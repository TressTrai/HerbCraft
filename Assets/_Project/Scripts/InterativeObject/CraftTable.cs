using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using UnityEngine;

public class CraftTable : MonoBehaviour
{
    [SerializeField] private string[] craftIngridientsNames;
    [SerializeField] private string[] craftOutputsNames;
    [SerializeField] private Sprite[] craftOutputsSprites;
    [SerializeField] private int playerCraftDuration = 0;
    [SerializeField] private int stationCraftDuration = 0;
    [SerializeField] private GameObject doneIcon;
    [SerializeField] private string craftTableName;

    public GameObject progressBarObject;

    private GameObject player;
    private GameObject inventory;
    private PersonalTriggerZone triggerZone;
    ProgressBar progressBarScript;

    private bool craftDone = false;
    private bool craftInProgress = false;

    Movement playerMovement;
    Inventory inv;
    private Item chosenItem;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("PlayerBody");
        playerMovement = player.GetComponentInChildren<Movement>();
        inventory = GameObject.FindGameObjectWithTag("Inventory");
        triggerZone = gameObject.GetComponent<PersonalTriggerZone>();
        inv = inventory.GetComponent<Inventory>();
        progressBarScript = progressBarObject.GetComponent<ProgressBar>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && triggerZone.PlayerDetection)
        {
            if (craftInProgress)
            {
                print("crafting, pease wait");
                return;
            }
            if (craftDone)
            {
                GiveCraftedItemToPlayer();
            }
            else
            {
                RemoveItemFromInventory();
            }
        }
    }

    private void RemoveItemFromInventory()
    {
        int chosenItemId = inv.ChosenItem;
        chosenItem = inv.GetItemById(chosenItemId);
        if (chosenItem != null && chosenItem.CanUseInCraft && CanItemBeUsed(chosenItem.Name))
        {
            print("chosen item");
            print(chosenItem.Name);
            inv.RemoveItem(chosenItemId);
            StartCraft();
        }
        else
        {
            print("can't use this item");
        }
    }

    private bool CanItemBeUsed(string itemName)
    {
        return craftIngridientsNames.Contains(itemName);
    }

    private void StartCraft()
    {
        triggerZone.SetIgnoreTrigger(true);

        if (playerCraftDuration != 0)
        {
            PlayerCraft();
        }
        else if (stationCraftDuration != 0)
        {
            StationCraft();
        }
        else
        {
            triggerZone.SetIgnoreTrigger(false);
        }
    }

    private void PlayerCraft()
    {
        void onCraftEnd()
        {
            playerMovement.UnFreeze();
            progressBarScript.HideProgressBar();

            if (stationCraftDuration == 0)
            {
                doneIcon.SetActive(true);
                craftDone = true;
                craftInProgress = false;
                triggerZone.SetIgnoreTrigger(false);
            }
            else
            {
                StationCraft();
            }
        }

        progressBarScript.Duration = playerCraftDuration;

        playerMovement.Freeze();
        progressBarScript.ShowProgressBar();
        craftInProgress = true;
        progressBarScript.Begin(onCraftEnd);
    }

    private void StationCraft()
    {
        print("station craft");
        void onCraftEnd()
        {
            progressBarScript.HideProgressBar();
            doneIcon.SetActive(true);
            craftDone = true;
            craftInProgress = false;
            triggerZone.SetIgnoreTrigger(false);
        }

        if (stationCraftDuration != 0)
        {
            progressBarScript.Duration = stationCraftDuration;
            progressBarScript.ShowProgressBar();
            craftInProgress = true;
            progressBarScript.Begin(onCraftEnd);
        }
    }

    private void GiveCraftedItemToPlayer()
    {
        if (chosenItem == null) return;

        Item craftedItem = GetCraftedItem();
        if (craftedItem.Name == null) return;

        bool itemAdded = inv.AddItem(craftedItem);

        if (!itemAdded) return;

        craftDone = false;
        doneIcon.SetActive(false);
    }

    private Item GetCraftedItem()
    {
        int indexOfCraftRecipe = Array.IndexOf(craftIngridientsNames, chosenItem.Name);
        string outputCraftName = craftOutputsNames[indexOfCraftRecipe];
        Sprite outputCraftSprite = craftOutputsSprites[indexOfCraftRecipe];
        Item outputCraftItem = new Item(outputCraftName, outputCraftSprite, true);

        return outputCraftItem;
    }
}

public delegate void OnCraftEnd();
