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

    void ProgramBegins()
    {
        float timeDifference = TimeManager.instance.TimeDiffBtwExitAndStart();
        AddStorageGarbage(timeDifference);
        AddVehicleGarbage(timeDifference);
    }

    void AddStorageGarbage(float timeDifference)
    {
        int volunteerCount = PersistantData.Instance.sceneData.VolunteerPickerCount;
        int storageDifference = (int)(timeDifference / storage_timeBtwAdditions) * volunteerCount;

        // ------------maksimumas jau yra paskaiciuotas kitur---------------------
        //int storageLevelFormula = 50 + (PersistantData.Instance.sceneData.StorageCapacityCurrentLevel * 25);

        int maxStorage = refToStorage.GetMaxGarbageCount(); // asikesnis vardas

        if (PersistantData.Instance.sceneData.StorageGarbageCount + storageDifference > maxStorage)
        {
            storageDifference = refToStorage.GetMaxGarbageCount() - refToStorage.GetGarbageCount();
        }


        refToStorage.AddGarbage(storageDifference);
        TrashController.Instance.DecreaseTotalTrashAmount(storageDifference);
        GameManager.Instance.experienceManager.experienceToIncrease += 2 * storageDifference;
    }

    void AddVehicleGarbage(float timeDifference)
    {
        int volunteerCount = PersistantData.Instance.sceneData.VolunteerLoaderCount;
        int vehicleDifference = (int)(timeDifference / vehicle_timeBtwAdditions) * volunteerCount;
        refToVehicle.AddGarbage(vehicleDifference);
    }
}
