using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repository : BuildingBase
{
    private void Start()
    {
        IncreaseStorage();
    }

    protected void IncreaseStorage()
    {
        if (workRemuneration.GasCount > 0)
        {
            Resources.AddGasCapacity(workRemuneration.GasCount);
        }

        if (workRemuneration.MineralsCount > 0)
        {
            Resources.AddMineralsCapacity(workRemuneration.MineralsCount);
        }
    }
}
