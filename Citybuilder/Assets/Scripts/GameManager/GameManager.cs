using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private PurchaseController purchaseController;
    [SerializeField] private GridController gridController;

    public PurchaseController PurchaseController => purchaseController;
    public GridController GridController => gridController;
}
