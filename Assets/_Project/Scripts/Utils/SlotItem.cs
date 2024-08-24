using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public bool draggable = false;
    public Item item;

    private GameObject onDragObject;
    private Transform initialParent;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    public void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        onDragObject = GameObject.FindGameObjectWithTag("OnDragObject");
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!draggable) return;
        canvasGroup.blocksRaycasts = false;

        onDragObject.transform.position = eventData.pointerDrag.transform.parent.position;
        initialParent = eventData.pointerDrag.transform.parent;
        eventData.pointerDrag.transform.SetParent(onDragObject.transform);
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (!draggable) return;
        rectTransform.anchoredPosition += eventData.delta;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (!draggable) return;
        if (eventData.pointerDrag.transform.parent.name == "OnDragObject")
        {
            eventData.pointerDrag.transform.SetParent(initialParent);
        }
        transform.localPosition = Vector3.zero;
        canvasGroup.blocksRaycasts = true;
    }
}
