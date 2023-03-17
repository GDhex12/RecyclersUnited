using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrencyManager : MonoBehaviour
{
    int mapID;
    [SerializeField] long currencyAmount = 0;
    [SerializeField] TextMeshProUGUI currencyUI;

    public static CurrencyManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        GetParamsFromSave();
        UpdateCurrency();
    }

    public long GetCurrency()
    {
        return currencyAmount;
    }

    public void AddCurrency(int amount)
    {
        AddCurrency((long)amount);
    }

    public void AddCurrency(long amount)
    {
        currencyAmount += amount;
        UpdateCurrency();
    }

    public void RemoveCurrency(int amount)
    {
        RemoveCurrency((long)amount);
    }

    public void RemoveCurrency(long amount)
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
        if (currencyUI != null)
        {
            currencyUI.text = CurrencyStringFormat();
        }
        // Update player data
        SaveParams();
    }
    public void SetCurrency(long amount)
    {
        currencyAmount = amount;
        UpdateCurrency();
    }

    public bool IsAffordable(long price)
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

    void GetParamsFromSave()
    {
        //get parameters from savefile
        //currencyAmount
        //mapID

        currencyAmount = SaveSystem.LoadPlayer().Coins;
    }

    void SaveParams()
    {
        PersistantData.Instance.playerData.Coins = currencyAmount;
        SaveSystem.SavePlayer(PersistantData.Instance.playerData);
    }
}
