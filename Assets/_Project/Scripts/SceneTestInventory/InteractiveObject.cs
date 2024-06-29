using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    [SerializeField] private Sprite itemSprite;
    private Item item;
    private GameObject inventory;
    private PersonalTriggerZone triggerZone;


    private void Awake()
    {
        inventory = GameObject.FindGameObjectWithTag("Inventory");
        triggerZone = gameObject.GetComponent<PersonalTriggerZone>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && triggerZone.PlayerDetection)
        {
            AddItemToInventory();
        }

    }

    private void AddItemToInventory()
    {
        if (itemSprite == null)
        {
            return;
        }
        item = new Item("ButtonE", itemSprite, true);
        Inventory inv = inventory.GetComponent<Inventory>();
        inv.AddItem(item);

        Destroy(transform.parent.gameObject);
    }
}
