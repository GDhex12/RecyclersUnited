using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] ExperienceStats experienceManager;
    public Storage storage;
    public VehicleSystem vehicle;
    public static GameManager Instance { get; private set; }
    public Transform volunteerRoot;

    //temporary for volunteer walking
    public Queue<GameObject> trash = new Queue<GameObject>();

    [SerializeField]
    private GameObject[] trashobj;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            foreach (GameObject obj in trashobj)
            {
                trash.Enqueue(obj);
            }
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
        FindObjectOfType<SpawnObject>().SpawnCertainAmountOfVolunteers(volunteerRoot, PersistantData.Instance.playerData.VolunteerCount);
        storage.GetParamsFromSave();
        FindObjectOfType<VehicleSystem>().GetParamsFromSave();
    }
}
