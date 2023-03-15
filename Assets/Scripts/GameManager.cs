using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] CurrencyManager currencyManager;
    [SerializeField] ExperienceStats experienceManager;

    private void Start()
    {
        LoadPlayerData();
    }

    private void LoadPlayerData()
    {
        PlayerData loadedData = SaveSystem.LoadPlayer();
        PersistantData.Instance.GetLoadedData(loadedData);
        currencyManager.SetCurrency(loadedData.Coins);
        experienceManager.SetExperience(loadedData.Level, loadedData.Experience, loadedData.ExpLimit);
    }
}
