using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] ExperienceStats experienceManager;
    public static GameManager Instance;




    private void Start()
    {
        LoadPlayerData();
    }

    private void LoadPlayerData()
    {
        PlayerData loadedData = SaveSystem.LoadPlayer();
        PersistantData.Instance.GetLoadedData(loadedData);

        CurrencyManager.instance.SetCurrency(loadedData.Coins);
        experienceManager.SetExperience(loadedData.Level, loadedData.Experience, loadedData.ExpLimit);
    }


}
