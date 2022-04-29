using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    public GameObject cellPrefab;

    [HideInInspector]
    public Cell[,] allCells = new Cell[19, 19];

    public void Create()
    {
        for (int y = 0; y < 19; y++)
        {
            for (int x = 0; x < 19; x++)
            {
                GameObject newCell = Instantiate(cellPrefab, transform);

                RectTransform rectTransform = newCell.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = new Vector2((x * 100) + 50, (y * 100) + 50);

                allCells[x, y] = newCell.GetComponent<Cell>();
                allCells[x, y].SetUp(new Vector2Int(x, y), this);
            }
        }

        for (int x = 0; x < 18; x += 2)
        {
            for (int y = 0; y < 19; y++)
            {
                int offset = (y % 2 != 0) ? 0 : 1;
                int finalX = x + offset;

                allCells[finalX, y].GetComponent<Image>().color = new Color32(230, 220, 187, 255);
            }
        }
    }
}
