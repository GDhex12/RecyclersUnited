using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardsManager : MonoBehaviour
{
    public void GiveMoney(int amount)
    {
        CurrencyManager.instance.AddCurrency(amount);
        Debug.Log($"Rewarded ${amount}");
    }
}
