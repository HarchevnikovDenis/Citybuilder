using UnityEngine;

public class PurchaseController : MonoBehaviour
{
    private Camera mainCamera;
    private BuildingInfo currentBuilding;
    private Cell selectedCell;

    private bool isSelectingCell => currentBuilding != null;

    public bool IsBuilding { get; private set; }

    private void Awake()
    {
        // Init camera
        mainCamera = Camera.main;
    }

    public void SetCurrentBuilding(BuildingInfo newBuildingBase)
    {
        currentBuilding = newBuildingBase;

        // Show green cells
        GameManager.Instance.GridController.ShowCellsAvailableForConstruction();
    }

    public void ResetCurrentBuilding()
    {
        if (isSelectingCell)
        {
            currentBuilding = null;

            // Hide green lighting
            GameManager.Instance.GridController.HideCellsAvailableForConstruction();
        }
    }

    private void Update()
    {
        if (!isSelectingCell) return;

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.GetComponent<Cell>() is Cell cell)
                {
                    selectedCell = cell;
                }
            }
            return;
        }

        if (Input.GetMouseButtonUp(0))
        {
            // Ñheck that we release the button on the same object
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.GetComponent<Cell>() is Cell cell)
                {
                    if (cell.Equals(selectedCell))
                    {
                        // Spend Resources
                        currentBuilding.SpendMoneyForBuilding();

                        // Build
                        selectedCell.Build(currentBuilding);
                        StartBuilding();

                        // Stop selecting cell for building process
                        ResetCurrentBuilding();   
                    }

                    selectedCell = null;
                }
            }
        }
    }

    private void StartBuilding()
    {
        IsBuilding = true;
    }

    public void StopBuilding()
    {
        IsBuilding = false;
    }
}
