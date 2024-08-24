using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public struct CellsSizes
{
    public CellsSizes(float cellSize, float cellsGap)
    {
        CellSize = cellSize;
        CellsGap = cellsGap;
    }

    public float CellSize { get; }
    public float CellsGap { get; }
}

public class Chest : MonoBehaviour
{
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private int cellsAvailable = 10;
    [SerializeField] private int cellsGap = 18;
    [SerializeField] private int cellsInRow = 5;
    //private int cellSize = 72;

    CellsSizes cellsSizes;

    private Item[] items;
    private GameObject[] cellsObjects;

    private GameObject inventoryObject;
    private GameObject cellsGridPlane;
    private GameObject cellsLayoutPlane;
    private GridLayoutGroup cellsLayout;
    private GameObject player;
    private PersonalTriggerZone triggerZone;
    Movement playerMovement;

    private Inventory inventory;

    private int chosenItem;

    private void Awake()
    {
        cellsObjects = new GameObject[cellsAvailable];

        triggerZone = gameObject.GetComponent<PersonalTriggerZone>();

        player = GameObject.FindGameObjectWithTag("PlayerBody");
        playerMovement = player.GetComponentInChildren<Movement>();

        inventoryObject = GameObject.FindGameObjectWithTag("Inventory");
        inventory = inventoryObject.GetComponent<Inventory>();

        cellsGridPlane = GameObject.FindGameObjectWithTag("ChestGridPlane");
        cellsLayoutPlane = GameObject.FindGameObjectWithTag("ChestCellsGrid");
        cellsLayout = cellsLayoutPlane.GetComponent<GridLayoutGroup>();
        //cellsGridPlaneRect = cellsGridPlane.GetComponent<RectTransform>();

        DrawGrid();
        Close();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) &&
            triggerZone.PlayerDetection &&
            triggerZone.TriggeredName == "PlayerBody" &&
            cellsGridPlane.activeSelf == false)
        {
            Open();
        }
    }

    private void DrawGrid()
    {
        //cellsSizes = GetCellsSizes();
        //ChangeLayoutParams();
        //ChangeCellSize();

        for (int i = 0; i < cellsAvailable; i++)
        {
            GameObject cellObject = Instantiate(cellPrefab, cellsLayoutPlane.transform, false);
            cellObject.GetComponent<ChestCell>().index = i;
            cellsObjects[i] = cellObject;
        }
    }

    private void ChangeCellSize()
    {
        RectTransform cellRect = cellPrefab.GetComponent<RectTransform>();
        float initialSize = cellRect.sizeDelta.x;
        float scaleDelta = cellsSizes.CellSize / initialSize;

        cellPrefab.transform.localScale = new Vector2(scaleDelta, scaleDelta);
    }

    private void ChangeLayoutParams()
    {
        cellsLayout.spacing = new Vector2(
            cellsSizes.CellsGap,
            cellsSizes.CellsGap
        );

        cellsLayout.cellSize = new Vector2(
            cellsSizes.CellSize,
            cellsSizes.CellSize
        );
    }

    private CellsSizes GetCellsSizes()
    {
        Vector2 planeSizes = GetChestPlaneSizes();
        float minSide = Mathf.Min(planeSizes.x, planeSizes.y);
        return new CellsSizes(0.8f * minSide / 5f, 0.05f * minSide / 4f);
    }

    private Vector2 GetChestPlaneSizes()
    {
        RectTransform rectTransform = cellsGridPlane.GetComponent<RectTransform>();
        return new Vector2(
            Screen.width + rectTransform.sizeDelta.x,
            Screen.height + rectTransform.sizeDelta.y
        );
    }

    public void SetChosenCell(int cellId)
    {
        if (cellId > cellsObjects.Length) return;

        PaintChosenCell(cellId);
        chosenItem = cellId - 1;
    }

    public bool AddItem(Item newItem)
    {
        print(newItem.Name);
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
            {
                items[i] = newItem;
                //UpdateCells();
                return true;
            }
        }

        return false;
    }
    public Item[] GetItemsList()
    {
        return items;
    }
    public void ClearInventory()
    {
        for (int i = 0; i < items.Length; i++)
        {
            items[i] = null;
            ChestCell cell = (ChestCell)cellsObjects[i].gameObject.GetComponent(typeof(ChestCell));
            //cell.ChangeSprite();
        }
    }
    public bool RemoveItem(int itemId)
    {
        bool itemDeleted = false;

        for (int i = 0; i < items.Length; i++)
        {
            if (itemDeleted)
            {
                items[i - 1] = items[i];
                if (i == items.Length - 1) items[i] = null;
                continue;
            }
            if (itemId == i)
            {
                items[i] = null;
                itemDeleted = true;
            }
        }
        //UpdateCells();
        return itemDeleted;
    }
    //private void UpdateCells()
    //{
    //    for (int i = 0; i < items.Length; i++)
    //    {
    //        if (cellsObjects[i] == null) break;

    //        ChestCell cell = (ChestCell)cellsObjects[i].gameObject.GetComponent(typeof(ChestCell));
    //        if (items[i] != null)
    //        {
    //            cell.ChangeSprite(items[i].Sprite);
    //        }
    //        else
    //        {
    //            cell.ChangeSprite();
    //        }
    //    }
    //}
    private void PaintChosenCell(int cellId)
    {
        if (cellId > cellsObjects.Length) return;

        for (int i = 0; i < cellsObjects.Length; i++)
        {
            if (cellId - 1 == i)
            {
                //if (cellsObjects[i].transform.GetChild(0).GetComponent<Image>().sprite == null)
                //{
                cellsObjects[i].GetComponent<Image>().color = new Color(0.95f, 0.5f, 0.5f, 255);
                //}
                //else
                //{
                //    cellsObjects[i].GetComponent<Image>().color = new Color(0.95f, 0.5f, 0.5f, 255);
                //}
            }
            else
            {
                cellsObjects[i].GetComponent<Image>().color = new Color(1f, 1f, 1f, 255);
                //cellsObjects[i].transform.GetChild(0).GetComponent<Image>().color = new Color(1f, 1f, 1f, 255);
            }

        }
    }
    public Item GetItemById(int id)
    {
        return items[id];
    }
    private void CheckInventoryChosenCell()
    {
        int chosenItemId = inventory.ChosenItem;
        Debug.Log("chosen item id " + chosenItemId);
        Item chosenItem = inventory.GetItemById(chosenItemId);
        Debug.Log("chosen item name " + chosenItem.Name);
    }

    public void Open()
    {
        print("opening");
        cellsGridPlane.SetActive(true);
        playerMovement.Freeze();
    }

    public void Close()
    {
        print("closing");
        cellsGridPlane.SetActive(false);
        playerMovement.UnFreeze();
    }
}
