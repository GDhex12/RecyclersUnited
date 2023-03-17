using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] CurrencyManager currencyManager;


    private void Start()
    {
        LoadPlayerData();
        LoadPlayerDataToScene();
    }

    private void LoadPlayerData()
    {
        PlayerData loadedData = SaveSystem.LoadPlayer();

        PersistantData.Instance.GetLoadedData(loadedData);

        currencyManager.SetCurrency(loadedData.Coins);

    }
    private void LoadPlayerDataToScene()
    {
        //Loading Volunteers to scene
        FindObjectOfType<SpawnObject>().SpawnCertainAmountOfVolunteers(PersistantData.Instance.playerData.VolunteerCount);
        FindObjectOfType<Storage>().GetParamsFromSave();
    }
}
