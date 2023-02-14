using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    [SerializeField] long currencyAmount = 0;

    private void Start()
    {
        UpdateCurrency();
    }

    public long GetCurrency()
    {
        return currencyAmount;
    }

    public void AddCurrency(int amount)
    {
        currencyAmount += amount;
    }

    public void RemoveCurrency(int amount)
    {
        if (currencyAmount - amount <= 0)
        {
            currencyAmount = 0;
        }
        else
        {
            currencyAmount -= amount;
        }

        UpdateCurrency();
    }

    void UpdateCurrency()
    {
        // Update player data
        // Update currency UI
    }

    string CurrencyStringFormat()
    {
        string line = "";
        int roundDigits = 1;
        switch (currencyAmount)
        {
            case < 1000:
                line = currencyAmount.ToString();
                break;
            case < 1000000:
                line = $"{Math.Round((double)currencyAmount / 1000, roundDigits)} K";
                break;
            case < 1000000000:
                line = $"{Math.Round((double)currencyAmount / 1000000, roundDigits)} M";
                break;
            case >= 1000000000:
                line = $"{Math.Round((double)currencyAmount / 1000000000, roundDigits)} B";
                break;
        }

        return line;
    }
}
