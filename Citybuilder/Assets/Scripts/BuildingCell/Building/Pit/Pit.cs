using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Pit : BuildingBase
{
    [SerializeField] private Image sliderImage;
    [SerializeField] private float workTimeInSec;

    private bool isWorking;
    private bool isEarned;

    private void OnMouseDown()
    {
        if (!isWorking)
        {
            if (isEarned)
            {
                // Get Reward
                GetRewardForTheWork();
            }

            StartWorking();
        }
    }

    private void GetRewardForTheWork()
    {
        Resources.AddRecources(workRemuneration);
    }

    private async void StartWorking()
    {
        isWorking = true;
        float t = 0.0f;

        while (t < 1.0f)
        {
            if (!this)
            {
                return;
            }

            t += (1.0f / workTimeInSec) * Time.deltaTime;
            t = Mathf.Clamp01(t);

            // Update Slider
            sliderImage.fillAmount = t;

            await Task.Yield();
        }

        if (!this)
        {
            return;
        }

        if (!isEarned)
        {
            isEarned = true;
        }

        isWorking = false;
    }
}
