using UnityEngine;
using UnityEngine.SceneManagement;

public class AppStartup : MonoBehaviour
{
    const int bootSceneNo = 0;
    public static bool veryFirstCallInApp = true;
    [SerializeField] private TimeManager refToTime;
    [SerializeField] private Storage refToStorage;

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

    void ProgramBegins()
    {
        float timeDifference = TimeManager.instance.TimeDiffBtwExitAndStart();
        int storageDifference = (int)timeDifference / 3;
        int storageLevelFormula = 50 + (PersistantData.Instance.sceneData.StorageCapacityCurrentLevel * 25);
        if (PersistantData.Instance.sceneData.StorageGarbageCount + storageDifference > storageLevelFormula)
        {
            refToStorage.AddGarbage(refToStorage.GetMaxGarbageCount() - refToStorage.GetGarbageCount());
        }
        else refToStorage.AddGarbage(storageDifference);
    }
}
