using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Item
{
    private string _name;
    private Sprite _sprite;
    private bool _canUseInCraft;

    public string Name 
    {
        get { return _name; }
        set { _name = value; }
    }
    public Sprite Sprite
    {
        get { return _sprite; }
        set { _sprite = value; }
    }
    public bool CanUseInCraft
    {
        get { return _canUseInCraft; }
        set { _canUseInCraft = value; }
    }

    public Item(string name, Sprite sprite)
    {
        _name = name;
        _sprite = sprite;
        _canUseInCraft = false;
    }
    public Item(string name, Sprite sprite, bool canUseInCraft)
    {
        _name = name;
        _sprite = sprite;
        _canUseInCraft = canUseInCraft;
    }
}

public class Inventory : MonoBehaviour
{
    [SerializeField] private int maxItemsAmount = 3;
    public GameObject inventoryCell;

    private Item[] items;
    private Image[] cellsObjects;

    private int chosenItem;

    private readonly int padding = 4;
    private readonly int cellWidth = 70;

    public int ChosenItem
    {
        get { return chosenItem; }
        set { chosenItem = value; }
    }


    private void Awake()
    {
        items = new Item[maxItemsAmount];
        cellsObjects = new Image[maxItemsAmount];

        int width = maxItemsAmount * cellWidth + (maxItemsAmount + 2) * padding;
        int height = cellWidth + 2 * padding;
        gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
        gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);

        for (int i = 0; i < maxItemsAmount; i++)
        {
            GameObject cellObject = Instantiate(inventoryCell, new Vector3(0, 0, 0), Quaternion.identity, transform);
            cellsObjects[i] = cellObject.GetComponent<Image>();
        }
    }

    private void Update()
    {
        for (int i = 49; i <= 57; i++)
        {
            KeyCode keyCode = (KeyCode)i;
            if (Input.GetKeyDown(keyCode))
            {
                SetChosenCell(i-48);
                PaintChosenCell(i-48);
            }
        }
    }

    private void SetChosenCell(int cellId)
    {
        if (cellId > cellsObjects.Length) return;

        chosenItem = cellId-1;
    }

    public bool AddItem(Item newItem)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
            {
                items[i] = newItem;
                UpdateCells();
                return true;
            }
        }

        return false;
    }
    public Item[] GetItemsList()
    {
        return items;
    }
    public bool RemoveItem(int itemId)
    {
        bool itemDeleted = false;

        for (int i = 0; i < items.Length; i++)
        {
            if (itemDeleted)
            {
                items[i - 1] = items[i];
                if (i == items.Length-1) items[i] = null;
                continue;
            }
            if (itemId == i)
            {
                items[i] = null;
                itemDeleted = true;
            }
        }
        UpdateCells();
        return itemDeleted;
    }
    private void UpdateCells()
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (cellsObjects[i] == null) break;

            InventoryCell cell = (InventoryCell)cellsObjects[i].gameObject.GetComponent(typeof(InventoryCell));
            if (items[i] != null)
            {
                cell.ChangeSprite(items[i].Sprite);
            }
            else
            {
                cell.ChangeSprite();
            }
        }
    }
    private void PaintChosenCell(int cellId)
    {
        if (cellId > cellsObjects.Length) return;

        for (int i = 0; i < cellsObjects.Length; i++)
        {
            if (cellId-1 == i)
            {
                cellsObjects[i].gameObject.GetComponent<Image>().color = Color.magenta;
            }
            else
                cellsObjects[i].gameObject.GetComponent<Image>().color = Color.white;
        }
    }
    public Item GetItemById(int id)
    {
        return items[id];
    }
}
