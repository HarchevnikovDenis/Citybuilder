using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    [SerializeField] private Cell buildingCellPrefab;
    [SerializeField] private Vector2 gridSize;
    [SerializeField] private float cellSize;

    private List<Cell> cells;

    private void Awake()
    {
        InitGrid();
    }

    private void InitGrid()
    {
        cells = new List<Cell>();

        for (int i = 0; i < gridSize.y; i++)
        {
            for (int j = 0; j < gridSize.x; j++)
            {
                Vector3 spawnPosition = Vector3.zero;
                spawnPosition.x += j * cellSize;
                spawnPosition.z += i * cellSize;

                Cell cell = Instantiate(buildingCellPrefab, spawnPosition, Quaternion.identity);
                cell.transform.SetParent(transform);
                cell.gameObject.name = $"Cell_{i + 1}_{j + 1}";
                cells.Add(cell);
            }
        }
    }

    public void ShowCellsAvailableForConstruction()
    {
        foreach (Cell cell in cells)
        {
            cell.ShowActiveForBuilding();
        }
    }

    public void HideCellsAvailableForConstruction()
    {
        foreach (Cell cell in cells)
        {
            cell.HideActiveForBuilding();
        }
    }
}
