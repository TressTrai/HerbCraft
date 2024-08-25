using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BinCell : MonoBehaviour, IDropHandler
{
    public bool IsSlotHasChilds()
    {
        return transform.childCount > 0;
    }
    // Removing object from bin
    private void ClearSlot()
    {
        Destroy(transform.GetChild(0).gameObject);
    }
    private void PutToCell(Transform otherItemTransform)
    {
        otherItemTransform.SetParent(transform);
        otherItemTransform.localPosition = Vector3.zero;
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (IsSlotHasChilds()) ClearSlot();
        PutToCell(eventData.pointerDrag.transform);
    }
}
