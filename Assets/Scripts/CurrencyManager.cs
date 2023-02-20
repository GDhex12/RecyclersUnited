using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrencyManager : MonoBehaviour
{
    [SerializeField] long currencyAmount = 0;
    [SerializeField] TextMeshProUGUI currencyUI;

    public static CurrencyManager instance;

    private void Awake()
    {
        instance = this;
    }

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
        UpdateCurrency();
    }

    public void RemoveCurrency(int amount)
    {
        if (!IsAffordable(amount))
        {
            Debug.Log("Not enough money!");
        }
        else
        {
            currencyAmount -= amount;
        }

        UpdateCurrency();
    }

    public void RemoveAllCurrency()
    {
        currencyAmount = 0;
        UpdateCurrency();
    }

    void UpdateCurrency()
    {
        // Update player data
        if (currencyUI != null)
        {
            currencyUI.text = CurrencyStringFormat();
            // temp add for testing - Ernestas
            SaveSystem.SavePlayer(new PlayerData(currencyAmount));
        }
    }
    public void SetCurrency(long amount)
    {
        currencyAmount = amount;
        UpdateCurrency();
    }

    public bool IsAffordable(int price)
    {
        return currencyAmount >= price;
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

        return "$"+line;
    }
}
