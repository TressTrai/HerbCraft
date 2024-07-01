using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    [SerializeField] private string itemName;
    [SerializeField] private Sprite itemSprite;
    [SerializeField] private bool canUseInCraft;
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

        
        item = new Item(itemName, itemSprite, canUseInCraft);
        Inventory inv = inventory.GetComponent<Inventory>();
        bool itemAdded = inv.AddItem(item);
        if (!itemAdded) return;
 
        transform.parent.gameObject.SetActive(false); 

        Invoke("Respawn",100f);
    }

    
    private void Respawn(){
            transform.parent.gameObject.SetActive(true);
    }
}
