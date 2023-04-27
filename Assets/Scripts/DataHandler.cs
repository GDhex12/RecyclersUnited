using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class DataHandler : MonoBehaviour
{
    private void Start()
    {
        LoadSceneData();
        LoadPlayerData();
        LoadPlayerDataToGameScene();
    }

    private void LoadSceneData()
    {
        SceneData loadedData = SaveSystem.LoadSceneData();
        PersistantData.Instance.GetLoadedData(loadedData);
    }
    private void LoadPlayerData()
    {
        PlayerData playerData = SaveSystem.LoadPlayerData();
        PersistantData.Instance.GetLoadedData(playerData);

        CurrencyManager.instance.SetCurrency(playerData.Coins);
        GameManager.Instance.experienceManager.SetExperience(playerData.Level, playerData.Experience, playerData.ExpLimit);
    }
    private void LoadPlayerDataToGameScene()
    {
        //Loading Volunteers to scene

        if (GameManager.Instance.volunteerRoot || GameManager.Instance.storage != null)
        {
            GameManager.Instance.spawner.SpawnCertainAmountOfPickerVolunteers(PersistantData.Instance.sceneData.VolunteerPickerCount);
            GameManager.Instance.spawner.SpawnCertainAmountOfLoaderVolunteers(PersistantData.Instance.sceneData.VolunteerLoaderCount);
            GameManager.Instance.storage.GetParamsFromSave();
            GameManager.Instance.vehicle.GetParamsFromSave();
        }
    }
}
