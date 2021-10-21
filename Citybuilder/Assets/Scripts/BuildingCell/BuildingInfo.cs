using UnityEngine;

[CreateAssetMenu(fileName = "BuildingData", menuName = "BuildingData/Create New Building Data")]
public class BuildingInfo : ScriptableObject
{
    [SerializeField] private BuildingBase buildingPrefab;
    [SerializeField] private ResourceUnit buildingPrice;
    [SerializeField] private float buildingTimeInSec;

    public BuildingBase BuildingPrefab => buildingPrefab;
    public float BuildingTimeInSec => buildingTimeInSec;

    public void SpendMoneyForBuilding()
    {
        Resources.SpendResources(buildingPrice);
    }

    public bool IsCanBuy()
    {
        return Resources.IsCanBuy(buildingPrice);
    }

    public string GetPriceText()
    {
        return $"{buildingPrice.GasCount} g; {buildingPrice.MineralsCount} m";
    }
}

[System.Serializable]
public class ResourceUnit
{
    [SerializeField] private int gasCount;
    [SerializeField] private int mineralsCount;

    public int GasCount => gasCount;
    public int MineralsCount => mineralsCount;
}
