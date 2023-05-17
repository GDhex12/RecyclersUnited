using UnityEngine;
using UnityEngine.SceneManagement;

public class AppStartup : MonoBehaviour
{
    [Header("General")]
    const int bootSceneNo = 0;
    public static bool veryFirstCallInApp = true;
    [SerializeField] private TimeManager refToTime;

    [Header("Storage")]
    Storage refToStorage;
    [SerializeField] float storage_timeBtwAdditions = 5;

    [Header("Vehicle")]
    VehicleSystem refToVehicle;
    [SerializeField] float vehicle_timeBtwAdditions = 4;

    [Header("Testing")]
    [SerializeField] bool isCountingOffScene = true;

    void Start()
    {
        refToStorage = GameManager.Instance.storage;
        refToVehicle = GameManager.Instance.vehicle;

        if (veryFirstCallInApp)
        {
            ProgramBegins();
            if (SceneManager.GetActiveScene().buildIndex != bootSceneNo)
            {
                return;
            }
        }
        veryFirstCallInApp = false;
        Destroy(gameObject);
    }

    private void ProgramBegins()
    {
        if (PersistantData.Instance.playerData.NewGameTutorial)
        {
            TutorialManager.Instance.Initialize();
        }

        if (!TrashController.Instance.IsCompleted() && isCountingOffScene)
        {
            float timeDifference = TimeManager.instance.TimeDiffBtwExitAndStart();
            AddStorageGarbage(timeDifference);
            AddVehicleGarbage(timeDifference);
        }
    }

    void AddStorageGarbage(float timeDifference)
    {
        int volunteerCount = PersistantData.Instance.sceneData.VolunteerPickerCount;
        int storageDifference = (int)(timeDifference / storage_timeBtwAdditions) * volunteerCount;

        int maxStorage = refToStorage.GetMaxGarbageCount();

        if (PersistantData.Instance.sceneData.StorageGarbageCount + storageDifference > maxStorage)
        {
            storageDifference = refToStorage.GetMaxGarbageCount() - refToStorage.GetGarbageCount();
        }


        refToStorage.AddGarbage(storageDifference);
        TrashController.Instance.DecreaseTotalTrashAmount(storageDifference);
        AddXpBasedOnGarbage(storageDifference);
    }

    void AddVehicleGarbage(float timeDifference)
    {
        int volunteerCount = PersistantData.Instance.sceneData.VolunteerLoaderCount;
        int vehicleDifference = (int)(timeDifference / vehicle_timeBtwAdditions) * volunteerCount;
        refToVehicle.AddGarbage(vehicleDifference);
    }
    
    private void AddXpBasedOnGarbage(int garbageAmount)
    {
        GameManager.Instance.experienceManager.experienceToIncrease += garbageAmount;
    }
}
