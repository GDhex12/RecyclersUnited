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
        GetParamsFromSave();
        
    }

    public long GetCurrency()
    {
        return currencyAmount;
    }

    public void AddCurrency(int amount)
    {
        AddCurrency((long)amount);
        UnlockCurrencyAchievements();
    }

    public void AddCurrency(long amount)
    {
        currencyAmount += amount;
        UpdateCurrency();
        UnlockCurrencyAchievements();
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
        return CurrencyStringFormat(currencyAmount);
    }

    public string CurrencyStringFormat(long amount)
    {
        string line = "";
        int roundDigits = 2;
        switch (amount)
        {
            case < 1000:
                line = amount.ToString();
                break;
            case < 1000000:
                line = $"{Math.Round((double)amount / 1000, roundDigits)} K";
                break;
            case < 1000000000:
                line = $"{Math.Round((double)amount / 1000000, roundDigits)} M";
                break;
            case >= 1000000000:
                line = $"{Math.Round((double)amount / 1000000000, roundDigits)} B";
                break;
        }

        return "$" + line;
    }

    void GetParamsFromSave()
    {
        //get parameters from savefile
        //currencyAmount
        //mapID

        currencyAmount = SaveSystem.LoadPlayerData().Coins;
        if (currencyUI != null)
        {
            currencyUI.text = CurrencyStringFormat();
        }
    }

    void SaveParams()
    {
        PersistantData.Instance.playerData.Coins = currencyAmount;
        SaveSystem.SavePlayerData(PersistantData.Instance.playerData);
    }

    private void UnlockCurrencyAchievements()
    {
#if UNITY_ANDROID
        if(currencyAmount > 10000)
        {
            GooglePlayLogin.Instance.UnlockAchievement(GPGSIds.achievement_money_hoarder_1);
            if (currencyAmount > 100000)
            {
                GooglePlayLogin.Instance.UnlockAchievement(GPGSIds.achievement_money_hoarder_2);
                if (currencyAmount > 1000000)
                {
                    GooglePlayLogin.Instance.UnlockAchievement(GPGSIds.achievement_money_hoarder_3);
                }
            }
        }
#endif
    }
}
