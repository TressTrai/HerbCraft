using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCell : MonoBehaviour
{
    private Image image;

    [SerializeField]
    private Sprite sprite;


    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Start()
    {
        image.sprite = sprite;
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
