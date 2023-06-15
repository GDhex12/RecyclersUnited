using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class TrashController : MonoBehaviour
{
    public static TrashController Instance;
    public NavMeshAgent navMesh;
    [SerializeField] List<TrashPile> trashPiles;

    [Header("Total location trash amount")]
    [SerializeField] int maxTotalTrashAmount = 1000;
    [SerializeField] int currentTotalTrashAmount = 0;
    [SerializeField] int completionPercentage = 100;
    [SerializeField] Slider meterUI;
    [SerializeField] UnityEvent onLocationComplete;

    bool _isCompleted = false;

    public bool IsCompleted() 
    { 
        return _isCompleted;
    }

    private void Awake()
    {
        Instance = this;

        //if (!_isCompleted && currentTotalTrashAmount == 0)
        //{
        //    currentTotalTrashAmount = maxTotalTrashAmount;
        //    SaveTotalGarbageCount();
        //    UpdateMeterUI();
        //}
    }

    public int GetCount()
    {
        return trashPiles.Count;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!_isCompleted && currentTotalTrashAmount == 0)
        {
            currentTotalTrashAmount = maxTotalTrashAmount;
            SaveTotalGarbageCount();
        }

        UpdateMeterUI();

        trashPiles = FindObjectsOfType<TrashPile>().ToList();


        // total amount check on load
        if (_isCompleted)
        {
            DestroyAllPiles();
            OnLocationCompletion();
        }
        else
        {
            if (currentTotalTrashAmount - (trashPiles[0].GetTrashAmount() * trashPiles.Count) < 0)
            {
                int fullPilesNeeded = currentTotalTrashAmount / trashPiles[0].GetTrashAmount();
                int trashAmountInNotFullPile = currentTotalTrashAmount % trashPiles[0].GetTrashAmount();

                int pilesToDestroy = trashPiles.Count - (fullPilesNeeded + (trashAmountInNotFullPile>0 ? 1 : 0));
                //Debug.Log($"{fullPilesNeeded}   {trashAmountInNotFullPile}   {pilesToDestroy}");
                if (pilesToDestroy != 0)
                {
                    for(int i = 0;i< pilesToDestroy;i++)
                        DestryTrashPile(trashPiles[0]);
                    //trashPiles.RemoveRange(0, pilesToDestroy);
                    if (trashAmountInNotFullPile > 0)
                    {
                        int i = Random.Range(0, trashPiles.Count);
                        trashPiles[i].RemoveTrash_TotalAmount(trashPiles[i].GetTrashAmount() - trashAmountInNotFullPile);
                    }
                }
            }
        }
        
    }

    //for volunteers
    public TrashPile GetRandomPile()
    {
        int index = Random.Range(0, trashPiles.Count);
        return trashPiles[index];
    }

    //for spawning
    public void AddTrashPile(TrashPile pile)
    {
        int currTrashAmountInPiles = GetCurrTrashAmountInPiles();
        int trashAmountInOnePile = pile.GetTrashAmount();
        if (currentTotalTrashAmount - currTrashAmountInPiles < trashAmountInOnePile)
        {
            pile.RemoveTrash_TotalAmount(trashAmountInOnePile - (currentTotalTrashAmount - currTrashAmountInPiles));
        }

        trashPiles.Add(pile);
        
    }
    
    //for collected piles
    public void RemoveTrashPile(TrashPile pile)
    {
        trashPiles.Remove(pile);
    }

    //if new items are added on field
    public void RegeneratePickupPoints()
    {
        navMesh.isStopped = true;
        /*
         * TO-DO: clear all NavMesh data and regenerate new paths based on newly spawned trash here
         */
    }

    void DestryTrashPile(TrashPile pile)
    {
        RemoveTrashPile(pile);
        Destroy(pile.gameObject);
    }
    
    void DestroyAllPiles()
    {
        foreach(TrashPile pile in trashPiles)
            Destroy(pile.gameObject);
        trashPiles.Clear();
    }

    //----------------- total location trash amount ----------------

    public void DecreaseTotalTrashAmount(int amount)
    {
        if (!_isCompleted)
        {
            currentTotalTrashAmount -= amount;
            completionPercentage = GetCompletionPercentage();
            if (currentTotalTrashAmount <= 0)
            {
                _isCompleted = true;
                UnlockTrashCompletionAchievements();
            }
            SaveTotalGarbageCount();
            UpdateMeterUI();
        }
    }

    void UpdateMeterUI()
    {
        float max = maxTotalTrashAmount;
        float curr = currentTotalTrashAmount;
        float percentige = 1-(curr / max);
        meterUI.value = percentige;
    }

    private void UnlockTrashCompletionAchievements()
    {
#if UNITY_ANDROID
        switch(SceneManager.GetActiveScene().name)
        {
            case "BeachScene_01":
                GooglePlayLogin.Instance.UnlockAchievement(GPGSIds.achievement_sparkling_sweep_1);
                break;
            case "ValleyScene_02":
                GooglePlayLogin.Instance.UnlockAchievement(GPGSIds.achievement_sparkling_sweep_2);
                break;
            case "ArcticScene_03":
                GooglePlayLogin.Instance.UnlockAchievement(GPGSIds.achievement_sparkling_sweep_3);
                break;
            default:
                break;
        }
#endif
    }

    public void DecreaseTotalTrashAmount()
    {
        DecreaseTotalTrashAmount(1);
    }

    public int GetCompletionPercentage()
    {
        float max = maxTotalTrashAmount;
        float curr = currentTotalTrashAmount;
        int percentige = (int)Mathf.Round((curr / max) * 100);
        return percentige < 0 ? 0 : percentige;
    }

    public bool NeedsToSpawn()
    {
        int sum = 0;
        foreach (TrashPile pile in trashPiles)
            sum += pile.GetTrashAmount();

        return currentTotalTrashAmount - sum > 0;
    }

    int GetCurrTrashAmountInPiles()
    {
        int sum = 0;
        foreach (TrashPile pile in trashPiles)
            sum += pile.GetTrashAmount();
        return sum;
    }

    public void LoadTotalGarbageCount()
    {
        currentTotalTrashAmount = PersistantData.Instance.sceneData.TotalCollectedGarbageCount;
        _isCompleted = PersistantData.Instance.sceneData.IsLocationCompleted;
    }

    public void SaveTotalGarbageCount()
    {
        PersistantData.Instance.sceneData.TotalCollectedGarbageCount = currentTotalTrashAmount;
        PersistantData.Instance.sceneData.IsLocationCompleted = _isCompleted;
        SaveSystem.SaveSceneData(PersistantData.Instance.sceneData);
    }

    public void ResetTotalGarbageCount()
    {
        currentTotalTrashAmount = maxTotalTrashAmount;
        completionPercentage = GetCompletionPercentage();
        _isCompleted = false;
        SaveTotalGarbageCount();
        UpdateMeterUI();
    }

    public void OnLocationCompletion()
    {
        onLocationComplete.Invoke();
    }
}
