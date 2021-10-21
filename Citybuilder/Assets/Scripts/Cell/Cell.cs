using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    [SerializeField] private Image sliderImage;
    [SerializeField] private MeshRenderer meshRenderer;

    public bool IsTheCellFreeForBuild { get; private set; } = true;

    public void ShowActiveForBuilding()
    {
        // Green Coloring
        if (IsTheCellFreeForBuild)
        {
            meshRenderer.material.color = Color.green;
        }
    }

    public void HideActiveForBuilding()
    {
        // White Coloring
        if (IsTheCellFreeForBuild)
        {
            meshRenderer.material.color = Color.white;
        }
    }

    public async void Build(BuildingInfo buildingInfo)
    {
        sliderImage.gameObject.SetActive(true);
        HideActiveForBuilding();

        IsTheCellFreeForBuild = false;
        float t = 0.0f;

        while (t < 1.0f)
        {
            // The application was closed when the asynchronous method was executed
            if (!this)
            {
                return;
            }

            t += (1.0f / buildingInfo.BuildingTimeInSec) * Time.deltaTime;
            t = Mathf.Clamp01(t);

            // Update UI Slider
            sliderImage.fillAmount = t;

            await Task.Yield();
        }

        // Instantiate Building
        BuildingBase newBuilding = Instantiate(buildingInfo.BuildingPrefab, transform);
        GameManager.Instance.PurchaseController.StopBuilding();
        sliderImage.gameObject.SetActive(false);
    }
}
