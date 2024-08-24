using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChestCell : MonoBehaviour, IPointerClickHandler, IDropHandler
{
    public int index;

    private GameObject chestObject;
    private Chest chest;

    private void Awake()
    {
        chestObject = GameObject.FindGameObjectWithTag("Chest");
        chest = chestObject.GetComponent<Chest>();
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (index % 1 == 0)
        {
            chest.SetChosenCell(index + 1);
        }
    }
    public bool IsSlotHasChilds()
    {
        return transform.childCount > 0;
    }
    private void PutToCell(Transform otherItemTransform)
    {
        otherItemTransform.SetParent(transform);
        otherItemTransform.localPosition = Vector3.zero;
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (IsSlotHasChilds()) return;
        PutToCell(eventData.pointerDrag.transform);
    }
}
