using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ResourceManager : MonoBehaviour
{
    [SerializeField] private int startGasCount;
    [SerializeField] private int startMineralsCount;

    private int gasCount;
    private int gasCapacity;

    private int mineralsCount;
    private int mineralsCapacity;

    private void Awake()
    {
        Resources.InitResourceManager(this);

        gasCount = startGasCount;
        gasCapacity = startGasCount;

        mineralsCount = startMineralsCount;
        mineralsCapacity = startMineralsCount;

        UpdateGasText();
        UpdateMineralsText();
    }

    public bool IsCanBuy(ResourceUnit price)
    {
        return price.GasCount <= gasCount && price.MineralsCount <= mineralsCount;
    }

    public void SpendResources(ResourceUnit price)
    {
        gasCount -= price.GasCount;
        mineralsCount -= price.MineralsCount;

        if (price.GasCount > 0)
        {
            UpdateGasText();
        }

        if (price.MineralsCount > 0)
        {
            UpdateMineralsText();
        }

        if (gasCount < 0 || mineralsCount < 0)
        {
            throw new System.Exception();
        }
    }

    public void AddResource(ResourceUnit resource)
    {
        if (resource.GasCount > 0)
        {
            gasCount += resource.GasCount;
            if (gasCount > gasCapacity)
            {
                gasCount = gasCapacity;
            }

            UpdateGasText();
        }

        if (resource.MineralsCount > 0)
        {
            mineralsCount += resource.MineralsCount;
            if (mineralsCount > mineralsCapacity)
            {
                mineralsCount = mineralsCapacity;
            }

            UpdateMineralsText();
        }
    }

    public void AddGasCapacity(int extraStorage)
    {
        gasCapacity += extraStorage;
        UpdateGasText();
    }

    public void AddMineralsCapacity(int extraStorage)
    {
        mineralsCapacity += extraStorage;
        UpdateMineralsText();
    }

    private void UpdateGasText()
    {
        if (UIManager.Instance)
        {
            UIManager.Instance.UpdateGasText(gasCount, gasCapacity);
        }
        else
        {
            StartCoroutine(WaitForInitializationAndInvokeAction(() => UIManager.Instance.UpdateGasText(gasCount, gasCapacity)));
        }
    }

    private void UpdateMineralsText()
    {
        if (UIManager.Instance)
        {
            UIManager.Instance.UpdateMineralsText(mineralsCount, mineralsCapacity);
        }
        else
        {
            StartCoroutine(WaitForInitializationAndInvokeAction(() => UIManager.Instance.UpdateMineralsText(mineralsCount, mineralsCapacity)));
        }
    }

    private IEnumerator WaitForInitializationAndInvokeAction(UnityAction action)
    {
        while (UIManager.Instance == null)
        {
            yield return null;
        }

        action.Invoke();
    }
}
