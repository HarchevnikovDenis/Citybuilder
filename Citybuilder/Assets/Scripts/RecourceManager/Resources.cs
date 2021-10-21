using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

// Facade
public static class Resources
{
    private static ResourceManager resourceManager;

    public static void InitResourceManager(ResourceManager resource)
    {
        resourceManager = resource;
    }

    private static void CheckResourceManager()
    {
        if (resourceManager == null)
        {
            throw new NullReferenceException();
        }
    }

    public static bool IsCanBuy(ResourceUnit price)
    {
        CheckResourceManager();
        return resourceManager.IsCanBuy(price);
    }

    public static void SpendResources(ResourceUnit price)
    {
        CheckResourceManager();
        resourceManager.SpendResources(price);
    }

    public static void AddRecources(ResourceUnit resource)
    {
        CheckResourceManager();
        resourceManager.AddResource(resource);

        if (UIManager.Instance.IsShopPanelOpened)
        {
            UIManager.Instance.UpdateShopButtonsInteractivity();
        }
    }

    public static void AddGasCapacity(int extraStorage)
    {
        CheckResourceManager();
        resourceManager.AddGasCapacity(extraStorage);
    }

    public static void AddMineralsCapacity(int extraStorage)
    {
        CheckResourceManager();
        resourceManager.AddMineralsCapacity(extraStorage);
    }
}
