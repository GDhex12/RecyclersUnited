using UnityEngine;
using UnityEngine.SceneManagement;

public class AppStartup : MonoBehaviour
{
    const int bootSceneNo = 0;
    public static bool veryFirstCallInApp = true;
    [SerializeField] private TimeManager refToTime;

    void Start()
    {
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
        float timeDifference = TimeManager.instance.TimeDiffBtwExitAndStart();
        int storageDifference = (int)timeDifference / 3;
        int storageLevelFormula = 50 + (PersistantData.Instance.sceneData.StorageCapacityCurrentLevel * 25);
        int garbageToAdd;
        if (PersistantData.Instance.sceneData.StorageGarbageCount + storageDifference > storageLevelFormula)
        {        
            garbageToAdd = GameManager.Instance.storage.GetMaxGarbageCount() -
                GameManager.Instance.storage.GetGarbageCount();
            GameManager.Instance.storage.AddGarbage(garbageToAdd);
        }
        else
        {
            garbageToAdd = storageDifference;
            GameManager.Instance.storage.AddGarbage(garbageToAdd);
        }
        AddXpBasedOnGarbage(garbageToAdd);
    }
    private void AddXpBasedOnGarbage(int garbageAmount)
    {
        GameManager.Instance.experienceManager.experienceToIncrease += garbageAmount;
    }
}
