using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private int cellsAvailable;

    private Item[] items;
    private GameObject[] cellsObjects;

    private GameObject cellsGridPlane;

    private void Awake()
    {
        cellsGridPlane = GameObject.FindGameObjectWithTag("ChestGridPlane");
        for (int i = 0; i < cellsAvailable; i++)
        {
            GameObject cellObject = Instantiate(cellPrefab, new Vector3(0, 0, 0), Quaternion.identity, cellsGridPlane.transform);
        }
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


    public void Close()
    {
        print("clicked");
        gameObject.SetActive(false);
    }
}
