using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryCell : MonoBehaviour, IPointerClickHandler
{
    public int index;

    [SerializeField]
    private Sprite sprite;

    private Image image;
    private GameObject inventoryObject;
    private Inventory inventory;


    private void Awake()
    {
        image = transform.GetChild(0).GetComponent<Image>();
        inventoryObject = GameObject.FindGameObjectWithTag("Inventory");
        inventory = inventoryObject.GetComponent<Inventory>();
    }

    private void Start()
    {
        image.sprite = sprite;
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (index % 1 == 0)
        {
            inventory.SetChosenCell(index + 1);
        }
    }

    public void ChangeSprite()
    {
        image.sprite = null;
    }
    public void ChangeSprite(Sprite newSprite)
    {
        image.sprite = newSprite;
    }
    public void ChangeSprite(string newSpriteName)
    {
        image.sprite = Resources.Load<Sprite>(newSpriteName);
    }
}
