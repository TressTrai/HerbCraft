using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// , IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
public class DragDrop : EventTrigger
{
    [SerializeField] private Canvas canvas;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Image image;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public override void OnBeginDrag(PointerEventData eventData)
    {
        print("begin drag");
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
        return;
    }
    public override void OnDrag(PointerEventData eventData)
    {
        print("drag");
        rectTransform.anchoredPosition += eventData.delta;
    }
    public override void OnEndDrag(PointerEventData eventData)
    {
        print("end drag");
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        return;
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        print("poinnter down");
        return;
    }
    public override void OnDrop(PointerEventData eventData)
    {
        print("dropped");
        return;
    }
}
