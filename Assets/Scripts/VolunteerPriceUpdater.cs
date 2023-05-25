using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolunteerPriceUpdater : MonoBehaviour
{
    public static event Action OnPriceChange;

    public void UpdateUI()
    {
        OnPriceChange?.Invoke();
    }
}
