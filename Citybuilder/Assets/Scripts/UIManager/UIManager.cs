using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private Button shopButton;
    [SerializeField] private Text gasText;
    [SerializeField] private Text mineralsText;

    [Header("SHOP BUTTONS")]
    [SerializeField] private List<BuildingPurchaseButtonInfo> shopButtons;

    public bool IsShopPanelOpened { get; private set; } = false;

    protected override void Awake()
    {
        base.Awake();
        InitButtons();
    }

    private void InitButtons()
    {
        shopButton.onClick.AddListener(() => ToggleShopPanelActivity());
    }

    public void ToggleShopPanelActivity()
    {
        IsShopPanelOpened = !IsShopPanelOpened;

        if (IsShopPanelOpened)
        {
            UpdateShopButtonsInteractivity();

            // Reset current selected building (if != null)
            GameManager.Instance.PurchaseController.ResetCurrentBuilding();
        }

        shopPanel.SetActive(IsShopPanelOpened);
    }

    public void UpdateShopButtonsInteractivity()
    {
        foreach (BuildingPurchaseButtonInfo button in shopButtons)
        {
            button.UpdateButtonInteractivity();
        }
    }

    public void UpdateGasText(int currentCasCount, int gasCapacity)
    {
        gasText.text = $"{currentCasCount}/{gasCapacity}";
    }

    public void UpdateMineralsText(int currentMineralsCount, int mineralsCapacity)
    {
        mineralsText.text = $"{currentMineralsCount}/{mineralsCapacity}";
    }


}
