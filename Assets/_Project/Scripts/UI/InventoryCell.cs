using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryCell : MonoBehaviour, IPointerClickHandler, IDropHandler
{
    public int index;

    [SerializeField] private GameObject slotItemObject;

    private GameObject inventoryObject;
    private Inventory inventory;


    private void Awake()
    {
        inventoryObject = GameObject.FindGameObjectWithTag("Inventory");
        inventory = inventoryObject.GetComponent<Inventory>();
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (index % 1 == 0)
        {
            inventory.SetChosenCell(index + 1);
        }
    }

    public void CreateItemIfSlotEmpty(Item item)
    {
        if (IsSlotHasChilds()) return;
        GameObject createdSlotItem = Instantiate(slotItemObject, Vector3.zero, Quaternion.identity, transform);
        createdSlotItem.transform.localPosition = Vector3.zero;
        createdSlotItem.GetComponent<Image>().sprite = item.Sprite;
        createdSlotItem.GetComponent<SlotItem>().item = item;
    }
    public void DeleteSlotItem()
    {
        if (transform.childCount == 0) return;
        Destroy(transform.GetChild(0).gameObject);
    }
    public bool IsSlotHasChilds()
    {
        return transform.childCount > 0;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (IsSlotHasChilds()) return;
        Transform otherItemTransform = eventData.pointerDrag.transform;
        otherItemTransform.SetParent(transform);
        otherItemTransform.localPosition = Vector3.zero;
    }
}
