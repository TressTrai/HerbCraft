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
    private MusicPlayer musicPlayer;

    private Field fieldScript;


    private void Awake()
    {
        fieldScript = transform.parent.Find("Field").GetComponent<Field>();

        inventory = GameObject.FindGameObjectWithTag("Inventory");
        triggerZone = gameObject.GetComponent<PersonalTriggerZone>();
        musicPlayer = GameObject.FindGameObjectWithTag("MusicPlayer").GetComponent<MusicPlayer>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && triggerZone.PlayerDetection && triggerZone.TriggeredName == "PlayerBody")
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

        musicPlayer.PlayMusic(8);
        item = new Item(itemName, itemSprite, canUseInCraft);
        Inventory inv = inventory.GetComponent<Inventory>();
        bool itemAdded = inv.AddItem(item);
        if (!itemAdded) return;
 
        transform.parent.gameObject.SetActive(false); 

        Invoke("Respawn",1f);
    }

    
    private void Respawn()
    {
        transform.parent.gameObject.SetActive(true);
        gameObject.transform.position = fieldScript.GetRandomPoint();
    }
}
