using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class BuildingPurchaseButtonInfo : MonoBehaviour
{
    [SerializeField] private BuildingInfo buildingCell;
    [SerializeField] private Text buildingPriceText;

    private Button button;

    private void InitButton()
    {
        button = gameObject.GetComponent<Button>();
        buildingPriceText.text = buildingCell.GetPriceText();

        button.onClick.AddListener(() =>
        {
            // Hide Shop Panel
            UIManager.Instance.ToggleShopPanelActivity();

            // Init data in PurchaseController
            GameManager.Instance.PurchaseController.SetCurrentBuilding(buildingCell);
        });
    }

    public void UpdateButtonInteractivity()
    {
        if (!button)
        {
            InitButton();
        }

        if (!GameManager.Instance.PurchaseController.IsBuilding)
        {
            button.interactable = buildingCell.IsCanBuy();
        }
        else
        {
            button.interactable = false;

            if (buildingCell.IsCanBuy())
            {
                CheckStopBuildingAndUpdateTheButton();
            }
        }
    }

    private async void CheckStopBuildingAndUpdateTheButton()
    {
        while (true)
        {
            if (this)
            {
                // Shop closer
                if (!UIManager.Instance.IsShopPanelOpened)
                {
                    return;
                }

                if (!GameManager.Instance.PurchaseController.IsBuilding)
                {
                    button.interactable = true;
                }

                await Task.Yield();
            }
            else
            {
                return;
            }
        }
    }
}
