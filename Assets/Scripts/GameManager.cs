using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] ExperienceStats experienceManager;
    public Storage storage;
    public VehicleSystem vehicle;
    public VehicleCooldown vehicleCooldown;
    public SpawnObject spawner;
    public static GameManager Instance { get; private set; }
    public Transform volunteerRoot;

    //temporary for volunteer walking
    public List<GameObject> trash = new();


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }


    }
    private void Start()
    {
        LoadPlayerData();
        LoadPlayerDataToScene();
        // Add the game objects to the queue
        
    }

    private void LoadPlayerData()
    {
        PlayerData loadedData = SaveSystem.LoadPlayer();
        PersistantData.Instance.GetLoadedData(loadedData);

        CurrencyManager.instance.SetCurrency(loadedData.Coins);
        experienceManager.SetExperience(loadedData.Level, loadedData.Experience, loadedData.ExpLimit);
    }
    private void LoadPlayerDataToScene()
    {
        //Loading Volunteers to scene

        if (volunteerRoot || storage != null)
        {
        spawner.SpawnCertainAmountOfPickerVolunteers(PersistantData.Instance.playerData.VolunteerPickerCount);
        spawner.SpawnCertainAmountOfLoaderVolunteers(PersistantData.Instance.playerData.VolunteerLoaderCount);
        storage.GetParamsFromSave();
        vehicle.GetParamsFromSave();
        }

    }
}
